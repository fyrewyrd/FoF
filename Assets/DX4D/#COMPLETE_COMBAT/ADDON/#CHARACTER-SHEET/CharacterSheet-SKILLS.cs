#define automatic_components
//#define RPG2D //ENABLE FOR 2D MODE...or import the 2D_MODE unitypackage from the zip file
using Mirror;
using UnityEngine;
//using System.Collections.Generic;

public partial class CharacterSheet : NetworkBehaviour
{
    // skill finished event & pending actions //////////////////////////////////
    // pending actions while casting. to be applied after cast.
    [SyncVar, HideInInspector] public int currentSkill = -1;
    int pendingSkill = -1;
    // helper variable to remember which skill to use when we walked close enough
    public int useSkillWhenCloser = -1;

    //PLAYER (MOVEMENT)
    Vector3 pendingDestination = Vector3.zero;
    bool pendingDestinationValid = false;
    Vector3 pendingVelocity = Vector3.zero;
    bool pendingVelocityValid = false;

    //WARP
    public virtual void Warp(Vector3 destination)
    {
        // warp on server / client
        agent.Warp(destination);

#if !RPG2D
        // notify all the clients. this is the only 100% reliable way.
        // (see Entity.Warp comments)
        // => only on server. this might be called on clients too.
        if (isServer)
        {
            if (player) player.rubberbanding.RpcWarp(destination);

            if (GetComponent<Pet>() || GetComponent<Npc>() || GetComponent<Monster>())
                networkNavMeshAgent.RpcWarp(destination);

            // || GetComponent<Mount>()
        }
#endif
    }

    //RESET MOVEMENT
    //[Server]
    public virtual void ResetMovement()
    {
        // reset on server / client
        agent.ResetMovement();

        // notify all the clients. this is the only 100% reliable way.
        // => only on server. this might be called on clients too.
        if (isServer)
        {
            if (player != null) player.rubberbanding.ResetMovement();
        }
    }

    // client event when skill cast finished on server
    // -> useful for follow up attacks etc.
    //    (doing those on server won't really work because the target might have
    //     moved, in which case we need to follow, which we need to do on the
    //     client)
    [Client]
    void OnSkillCastFinished(Skill skill)
    {
        if (!isLocalPlayer) return;

        // tried to click move somewhere?
        if (pendingDestinationValid)
        {
            agent.stoppingDistance = 0;
            agent.destination = pendingDestination;
        }
        // tried to wasd move somewhere?
        else if (pendingVelocityValid)
        {
            agent.velocity = pendingVelocity;
        }
        // user pressed another skill button?
        else if (pendingSkill != -1)
        {
            TryUseSkill(pendingSkill, true);
        }
        // otherwise do follow up attack if no interruptions happened
        else if (skill.followupDefaultAttack)
        {
            StartCoroutine(player.FollowupAttack(0, skill.cooldown));
        }

        // clear pending actions in any case
        pendingSkill = -1;
        pendingDestinationValid = false;
        pendingVelocityValid = false;
    }

    [Command]
    public void CmdUseSkill(int skillIndex)
    {
        // validate
        if ((state == ActiveState.IDLE || state == ActiveState.MOVING || state == ActiveState.CASTING) &&
            0 <= skillIndex && skillIndex < SKILLS.Count)
        {
            // skill learned and can be casted?
            if (SKILLS[skillIndex].level > 0 && SKILLS[skillIndex].IsReady())
            {
                currentSkill = skillIndex;
            }
        }
    }

    // helper function: try to use a skill and walk into range if necessary
    [Client]
    public void TryUseSkill(int skillIndex, bool ignoreState = false)
    {
        // only if not casting already
        // (might need to ignore that when coming from pending skill where
        //  CASTING is still true)
        if (state != ActiveState.CASTING || ignoreState)
        {
            Skill skill = SKILLS[skillIndex];
            if (CastCheckSelf(skill) && CastCheckTarget(skill))
            {
                // check distance between self and target
#if RPG2D
                Vector2 destination;
#else
                Vector3 destination;
#endif
                if (CastCheckDistance(skill, out destination))
                {
                    // cast
                    CmdUseSkill(skillIndex);
                }
                else
                {
                    // move to the target first
                    // (use collider point(s) to also work with big entities)
                    agent.stoppingDistance = skill.castRange * ai.optimalRange;
                    agent.destination = destination;

                    // use skill when there
                    useSkillWhenCloser = skillIndex;
                }
            }
        }
        else
        {
            pendingSkill = skillIndex;
        }
    }

    // skill system ////////////////////////////////////////////////////////////
    // helper function to find a skill index
    public int GetSkillIndexByName(string skillName)
    {
        // (avoid FindIndex to minimize allocations)
        for (int i = 0; i < SKILLS.Count; ++i)
            if (SKILLS[i].name == skillName)
                return i;
        return -1;
    }

    // helper function to find a buff index
    public int GetBuffIndexByName(string buffName)
    {
        // (avoid FindIndex to minimize allocations)
        for (int i = 0; i < BUFFS.Count; ++i)
            if (BUFFS[i].name == buffName)
                return i;
        return -1;
    }
    // the first check validates the caster
    // (the skill won't be ready if we check self while casting it. so the
    //  checkSkillReady variable can be used to ignore that if needed)
    // has a weapon (important for projectiles etc.), no cooldown, hp, mp?
    public bool CastCheckSelf(Skill skill, bool checkSkillReady = true) =>
        skill.CheckSelf(this.GetComponent<Entity>(), checkSkillReady);

    // the second check validates the target and corrects it for the skill if
    // necessary (e.g. when trying to heal an npc, it sets target to self first)
    // (skill shots that don't need a target will just return true if the user
    //  wants to cast them at a valid position)
    public bool CastCheckTarget(Skill skill) =>
        skill.CheckTarget(this.GetComponent<Entity>());

    // the third check validates the distance between the caster and the target
    // (target entity or target position in case of skill shots)
    // note: castchecktarget already corrected the target (if any), so we don't
    //       have to worry about that anymore here
#if !RPG2D
    public bool CastCheckDistance(Skill skill, out Vector3 destination) =>
#else
    public bool CastCheckDistance(Skill skill, out Vector2 destination) =>
#endif
        skill.CheckDistance(this.GetComponent<Entity>(), out destination);

    // starts casting
    public void StartCastSkill(Skill skill)
    {
        // start casting and set the casting end time
        skill.castTimeEnd = NetworkTime.time + skill.castTime;

        // save modifications
        SKILLS[currentSkill] = skill;

        // rpc for client sided effects
        // -> pass that skill because skillIndex might be reset in the mean
        //    time, we never know
        RpcSkillCastStarted(skill);
    }

    // cancel a skill cast properly
    [Server]
    public void CancelCastSkill()
    {
        // reset cast time, otherwise if a buff has a 10s cast time and we
        // cancel the cast after 1s, then we would have to wait 9 more seconds
        // before we can attempt to cast it again.
        // -> we cancel it in any case. players will have to wait for 'casttime'
        //    when attempting another cast anyway.
        if (currentSkill != -1)
        {
            Skill skill = combat.skills[currentSkill];
            skill.castTimeEnd = NetworkTime.time - skill.castTime;
            combat.skills[currentSkill] = skill;

            // reset current skill
            currentSkill = -1;
        }
    }

    // finishes casting. casting and waiting has to be done in the state machine
    public void FinishCastSkill(Skill skill)
    {
        // * check if we can currently cast a skill (enough mana etc.)
        // * check if we can cast THAT skill on THAT target
        // note: we don't check the distance again. the skill will be cast even
        //   if the target walked a bit while we casted it (it's simply better
        //   gameplay and less frustrating)
        if (CastCheckSelf(skill, false) && CastCheckTarget(skill))
        {
            // let the skill template handle the action
            skill.Apply(this.GetComponent<Entity>());

            // rpc for client sided effects
            // -> pass that skill because skillIndex might be reset in the mean
            //    time, we never know
            RpcSkillCastFinished(skill);

            // decrease mana in any case
            SHIELD -= skill.costs.me.shield; BARRIER -= skill.costs.me.barrier; LIFE -= skill.costs.me.life; BLOOD -= skill.costs.me.blood; SPIRIT -= skill.costs.me.spirit; MANA -= skill.costs.me.mana; FURY -= skill.costs.me.fury; STAMINA -= skill.costs.me.stamina;


            // start the cooldown (and save it in the struct)
            skill.cooldownEnd = NetworkTime.time + skill.cooldown;

            // save any skill modifications in any case
            SKILLS[currentSkill] = skill;
        }
        else
        {
            // not all requirements met. no need to cast the same skill again
            //currentSkill = -1;
            CancelCastSkill();
        }
    }

    // helper function to add or refresh a buff
    public void AddOrRefreshBuff(Buff buff)
    {
        // reset if already in buffs list, otherwise add
        int index = GetBuffIndexByName(buff.name);
        if (index != -1) BUFFS[index] = buff;
        else BUFFS.Add(buff);
    }

    // helper function to remove all buffs that ended
    void CleanupBuffs()
    {
        for (int i = 0; i < BUFFS.Count; ++i)
        {
            if (BUFFS[i].BuffTimeRemaining() == 0)
            {
                BUFFS.RemoveAt(i);
                --i;
            }
        }
    }

    // skill cast started rpc for client sided effects
    // note: no need to pass skillIndex, currentSkill is synced anyway
    [ClientRpc]
    public void RpcSkillCastStarted(Skill skill)
    {
        // validate: still alive?
        if (!IsDead)
        {
            // call scriptableskill event
            skill.data.OnCastStarted(this);
        }
    }

    // skill cast finished rpc for client sided effects
    // note: no need to pass skillIndex, currentSkill is synced anyway
    [ClientRpc]
    public void RpcSkillCastFinished(Skill skill)
    {
        if (!IsDead)
        {
            // call scriptableskill event
            skill.data.OnCastFinished(this);

            // maybe some other component needs to know about it too
            SendMessage("OnSkillCastFinished", skill, SendMessageOptions.DontRequireReceiver);
        }
    }
}