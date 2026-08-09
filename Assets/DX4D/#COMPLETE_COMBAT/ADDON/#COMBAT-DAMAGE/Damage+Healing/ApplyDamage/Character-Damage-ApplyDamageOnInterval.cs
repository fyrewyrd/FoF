using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - - -
    // P R O C E S S  A D D E D  D A M A G E
    [Server] public IEnumerator ApplyDamageOverTime(CharacterSheet defender, ScriptedDamage[] scriptedDamageList, float delay, int bonusDamage)
    {
        //yield return new WaitForSeconds(startDelay);
        //skill.initialDamage.ProcessScriptedDamage(); //We process the initial skill damage first

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < scriptedDamageList.Length; i++) //We start with the second member of the added damage list because the first one is our initial damage
        {
            StartCoroutine(ApplyScriptedDamage(defender, scriptedDamageList[i], i, delay, bonusDamage));
            //TODO TODO TODO: EVAL THIS
            //for (int z = 0; z < scriptedDamageList[i].damage.damageOverTime.Count; z++)
            //{
            //    StartCoroutine(ApplyScriptedDamage(defender, scriptedDamageList[i].damage.damageOverTime[z], i + z, delay, bonusDamage));
            //}
        }
        //foreach (ScriptedDamage addedDamage in skill.addedDamage) { }
    }

    [Server] public IEnumerator ApplyScriptedDamage(CharacterSheet defender, ScriptedDamage scriptedDamage, int slotNumber, float delay, int bonusDamage)
    {
        yield return new WaitForSeconds(delay * slotNumber);

        ProcessScriptedDamage(defender, scriptedDamage, bonusDamage);
    }

    [Server] public void ProcessScriptedDamage(CharacterSheet defender, ScriptedDamage dmg, int bonusDamage)
    {
        // S E T  T H E  T A R G E T
        if (dmg.useOnSelf) { defender = this; }
        if (defender == null) { return; }

        //VISUAL FX
        if (vfx != null && dmg.onHitVisualEffect != null)
        {
            vfx.TriggerVisualEffect(defender.transform, dmg.onHitVisualEffect);
        }

        //DAMAGE MODS
        //ApplyDamageMethodModsToTarget(dmg);
        //ApplyElementalModsToTarget(dmg);

        //DEAL DAMAGE TO TARGET
        //if (dmg.damageAmount > 0)
        DamageInfo dmgInfo = dmg.damage.copy();

        dmgInfo.bonus = bonusDamage;

        DealCombatDamage(defender, dmgInfo);//, dmg.statusEffects);//new DamageInfo(dmg.damage.total, dmg.damage.method, dmg.damage.element, damage, 1.0f), new StatusInfo(dmg.status.probability.stun, dmg.status.duration.stun));

        /* //DEPRECIATED
        for (int i = 0; i < dmg.damage.damageOverTime.Count; i++)
        {
#if UNITY_EDITOR
            Debug.Log("SCRIPTED DAMAGE: " + dmg.damage.damageOverTime[i].damage.total); //DEBUG
#endif
            ProcessScriptedDamage(defender, dmg.damage.damageOverTime[i], bonusDamage);
        }*/
        //HEAL TARGET
        //if (dmg.healAmount > 0) ApplyHealTo(defender, dmg.healAmount);
    }
}
