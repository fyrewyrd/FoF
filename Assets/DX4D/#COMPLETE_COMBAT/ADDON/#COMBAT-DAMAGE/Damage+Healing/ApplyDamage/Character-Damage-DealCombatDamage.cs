//#define ummorpg //NOTE: Enable this to turn on ummorpg addon hooks
using Mirror;
//using System.Collections.Generic;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - -
    // D E A L  C O M B A T  D A M A G E
    /// <summary>
    /// Use this function to deal damage modified by the attacker and defender's stats/gear/etc
    /// Refreshes the character's combat state, gear, skills, etc, then calls <see cref="ApplyDamageTo(CharacterSheet, DamageInfo)"/>
    /// </summary>
    [Server] public virtual void DealCombatDamage(CharacterSheet defender, DamageInfo initialDamage)//, StatusEffectList statusEffects)
    {
        // V A L I D A T E  C O M B A T A N T S
        if (IsDead || !defender || defender.combat.invincible || defender.IsDead) return;


        // S Y N C  G E A R
        if (this is PlayerCharacter) (this as PlayerCharacter).SyncMyEquipment();
        if (defender is PlayerCharacter) (defender as PlayerCharacter).SyncMyEquipment();
        
        //bool zeroDamage = (initialDamage.max <= 1);

        // I N I T I A L  D A M A G E
        DamageInfo damage = initialDamage.copy();
        
        // I M B U E  D A M A G E
        if (combat.imbueAttacks)
        {
            //innate damage method
            if (damage.method == MethodOfDamage.Physical && combat.innateMethodOfDamage != MethodOfDamage.Physical)
            { damage.method = combat.innateMethodOfDamage; }//innateMethodOfDamage != MethodOfDamage.NoDamage && //NonDamage is now a valid method for innate damage

            //innate damage element
            if (damage.element == Element.Neutral && combat.innateDamageElement != Element.Neutral)
            { damage.element = combat.innateDamageElement; }
        }



        // S Y N C  C O M B A T  S T A T E
        SyncCombatState(this, defender, damage.damage, damage.properties);

        // C A L C U L A T E  D A M A G E
        damage = CalculateDamage(defender, damage);



        // A P P L Y  D A M A G E  &  D A M A G E  S H I E L D  A B S O R B
        ApplyDamage(defender, damage.total, damage.method);
        /*
        #region DEBUG
#if UNITY_EDITOR
        if (zeroDamage)
        {
            UnityEngine.Debug.Log("bypassing damage (max < 1)");
        }
        else
        {

            UnityEngine.Debug.Log("!!!!!!!!!DAMAGE!!!!!!!!!!!!");
        }
#endif
        #endregion
        */


        // A P P L Y  D A M A G E  O V E R  T I M E
        if (damage.damageOverTime != null && damage.damageOverTime.Count > 0)// && damage.damageOverTime.Count > 0)
        {
            for (int i = 0; i < damage.damageOverTime.Count; i++) //We start with the second member of the added damage list because the first one is our initial damage
            {
                StartCoroutine(ApplyScriptedDamage(defender, damage.damageOverTime[i], i, damage.damageTickInterval,  damage.bonus));
            }
            //StartCoroutine(ApplyDamageOverTime(defender, damage.damageOverTime.ToArray(), damage.damageTickInterval, /*modifiedSkillDamage.total*/ 0)); //TODO: Debug
            damage.damageOverTime.Clear();
        }

        // A P P L Y  T A C T I C S
        if (damage.tactics != null) damage.tactics.Apply(this, defender);// ApplyTactics(defender, damage);

        // A P P L Y  S T A T U S  E F F E C T S
        ApplyStatusEffects(defender, damage.status);

        // A P P L Y  D A M A G E  L E E C H
        if (damage.properties.lifeLeech) { ApplyHealTo(this, damage.total); }



        //TODO
        // A P P L Y  O N  H I T  E F F E C T S
        //if (!zeroDamage) ApplyOnHitEffects(defender);
        //player.RpcShowVisualEffect()


        // S E T  L A S T  C O M B A T  T I M E
        combat.timeOfLastCombat = NetworkTime.time;
        defender.combat.timeOfLastCombat = NetworkTime.time;



        // H A N D L E  P O S T  A T T A C K  E V E N T S
        HandleDamageTriggers(defender, damage.method, damage.element);
        HandlePassiveTriggers(defender, ActiveOffensiveState, defender.ActiveDefensiveState);



        // S H O W  D A M A G E  P O P U P
        //if (!zeroDamage) 
        defender.RpcShowDamagePopup(damage.total, defender.ActiveDefensiveState, damage.method, damage.element);
        //defender.RpcOnDamageReceived(damage.total, defender.ActiveDefensiveState, damage.method, damage.element);// dealsDamageType, dealsElementalDamageType);

        // A G G R O
        //defender.ai.OnDamagedByOpponent(this, damage.total); //FALLBACK DAMAGE TRIGGER //NOTE: Already called by hooks?
        defender.OnAggro(this); //FALLBACK AGGRO

        // [CALL HOOKS]
        if (player)
        {
            Utils.InvokeMany(typeof(PlayerCharacter), player, "DealCombatDamage_", defender, damage);
        }
        else
        {
            Utils.InvokeMany(typeof(CharacterSheet), this, "DealCombatDamage_", defender, damage);
        }

//#if ummorpg
        // [UMMORPG HOOKS]
        //Entity attackingEntity = gameObject.GetComponent<Entity>();
        //Entity defendingEntity = defender.gameObject.GetComponent<Entity>();
        //if (attackingEntity != null && defendingEntity != null)
        //{
            //attackingEntity.DealDamageAt(defendingEntity, damage.total, 1.0f, 1.0f);
            //Utils.InvokeMany(typeof(Entity), attackingEntity, "DealDamageAt_", , damage.total, 0, 0);
        //}
//#endif
    }
}
