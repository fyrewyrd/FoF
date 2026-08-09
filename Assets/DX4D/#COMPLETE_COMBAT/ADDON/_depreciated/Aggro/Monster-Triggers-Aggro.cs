/* //DEPRECIATED
using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class Monster : Entity
{
    [Header("TRIGGERED SPEECH")]
    [SerializeField, TextArea(1, 3)] public string sayOnAggro;
    [SerializeField, TextArea(1, 3)] public string sayOnSwitchTarget;

    [Header("TRIGGERED SKILL CAST")]
    [SerializeField] public bool canTriggerSkills = false;
    [SerializeField] public ScriptableSkill onAggroCastSkill;
    [SerializeField] public ScriptableSkill onSwitchTargetCastSkill;

    [Header("TRIGGERED AUDIO")]
    [Tooltip("This can only be turned off if On Aggro Sound is set to none/null")]
    [SerializeField]
    public bool canTriggerSound = false;
    [SerializeField] public AudioClip onAggroSound;
    [SerializeField] public AudioClip onSwitchTargetSound;
    [Header("TRIGGERED ANIMATIONS")]
    //TODO: I have not animated anything in Unity yet, so hopefully this is how it works
    [Tooltip("This can only be turned off if On Aggro Animation is set to none/null")]
    [SerializeField] public bool canTriggerAnimation = false;
    [SerializeField] public string onAggroClipName;
    [SerializeField] public string onSwitchTargetClipName;

    [Header("TRIGGERED SPAWNING")]
    [SerializeField]
    public bool canSpawnMinions = false;
    [SerializeField] public int maxMinions = 1;
    [SerializeField] public GameObject[] onAggroSpawn;
    [SerializeField] public GameObject[] onSwitchTargetSpawn;

    [SerializeField] public int spawnDistance = 3;
    [SerializeField] public bool minionsDieWithMe = true;
    [SerializeField] public bool noMinionCorpses = false;
    [SerializeField] List<Entity> spawnedMinions = new List<Entity>();

    [Server] protected void OnDeath_ResetSpawnedAllies()
    {
        if (minionsDieWithMe)
        {
            foreach (Entity minion in spawnedMinions)
            {
                if (noMinionCorpses) { Destroy(minion.gameObject); }
                else { minion.health = 0; } //TODO: Make sure this works in Server mode
            }
        }
        spawnedMinions.Clear();  //TODO: Leaving this here could lead to ungodly amounts of summoned allies if nobody cleans house...
                                 //  Think about adding AI that makes enemies fight when they get bored. ;)
                                 //  A DestroyAfter Component set to around 300 (5 mins) is recommended for all minions to solve this.
    }

    private void OnValidate()
    {
        if (!canTriggerSkills && (onAggroCastSkill != null || onSwitchTargetCastSkill != null))
        {
            canTriggerSkills = true;
        }
        if (!canTriggerAnimation && (onAggroClipName != string.Empty || onSwitchTargetClipName != string.Empty))
        {
            canTriggerAnimation = true;
        }
        if (!canTriggerSound && (onAggroSound != null || onSwitchTargetSound != null))
        {
            canTriggerSound = true;
        }
        if (!canSpawnMinions && ((onAggroSpawn != null && onAggroSpawn.Length > 0) || (onSwitchTargetSpawn != null && onSwitchTargetSpawn.Length > 0)))
        {
            canSpawnMinions = true;
        }
    }

    [Server] public void LaunchTriggeredActions(Entity triggeredBy, string say, AudioClip sound, string animationName, GameObject[] spawnMinions, ScriptableSkill skill)
    {
        //TRIGGERED ACTIONS
        //SPEAK
        if (say != string.Empty) RpcShowTextPopup(say);

        //PLAY SOUND
        if (canTriggerSound && sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
        }

        //PLAY ANIMATION
        if (canTriggerAnimation && animationName != string.Empty)
        {
            animator.Play(animationName);
        }

        //SUMMON ALLIES
        if (canSpawnMinions && spawnMinions != null && spawnMinions.Length > 0 && spawnedMinions.Count < maxMinions)
        {
            for (int i = 0; i < spawnMinions.Length; i++)
            {
                if (spawnMinions[i] != null && spawnMinions[i].gameObject != null)
                {
                    //GameObject go = Instantiate(spawnMinions[i].gameObject, (transform.position + DX4D.Tools.GetRandomVector(-spawnDistance, spawnDistance)), Quaternion.identity);
                    GameObject go = Instantiate(spawnMinions[i].gameObject, (transform.position + DX4D.Tools.GetRandomVector(-spawnDistance, spawnDistance)), Quaternion.identity);
                    go.name = spawnMinions[i].name; //TODO: This does not work in Server mode for some reason...we might need a ClientRpc or ServerCallback. Normally this would get rid of (Clone)

                    Entity summoned = go.GetComponent<Entity>();
                    if (summoned != null)
                    {
                        summoned.name = spawnMinions[i].name; //TODO: Added for redundancy...still needs to be tested in server mode. One of these can be removed once it works across the network.
                        spawnedMinions.Add(summoned); //Only Entities will count toward your maxAllies quota
                        summoned.OnAggro(triggeredBy); //Aggro our attacker with the newly summoned entity...if it's an Entity
                    }

                    NetworkServer.Spawn(go);
                }
            }
        }

        //CAST SKILL
        if (canTriggerSkills && skill != null)
        {
            currentSkill = GetSkillIndexByName(skill.name);
        }
    }
}
*/
