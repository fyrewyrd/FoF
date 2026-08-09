/* //DEPRECIATED
using System.Collections;
using UnityEngine;

public partial class Player : Entity
{
    public IEnumerator DealWeaponDamage(Entity defender, CombatWeapon weapon, bool delayed)//, int amount, float delay)
    {
        if (delayed) yield return new WaitForSeconds(weapon.attackDelay);

        if (defender != null)
        {
            //AMMUNITION
            if (weapon.requiresAmmo)
            {
                if (!weapon.HasAmmo)
                {
                    RpcShowOopsPopup(weapon.name + " [out of ammo]");
                }
                else
                {
                    FireWeapon(weapon);
                }
            }
            else
            {
                // A P P L Y  D A M A G E
                DealDamageAt(defender, new DamageInfo(weapon.damage.total, weapon.damage.method, weapon.damage.element, damage, weapon.damage.multiplier), new StatusInfo(weapon.status.probability.stun, weapon.status.duration.stun));

                if (weapon.damage.addedDamage != null && weapon.damage.addedDamage.Length > 0)
                {
                    for (int i = 0; i < weapon.damage.addedDamage.Length; i++)
                    {
                        StartCoroutine(ApplyScriptedDamage(defender, weapon.damage.addedDamage[i], 0, 0.0f));
                    }
                }
            }

            //LOWER DURABILITY
            if (weapon.durabilityNow > 0)
            {
                //weapon.durability -= weapon.breakability;
                weapon.DamageDurability();

                if (weapon.durability <= weapon.maxDurability * 0.01f) RpcShowActivationPopup(weapon.name + " is almost broken");
                else if (weapon.durability <= weapon.maxDurability * 0.03f) RpcShowActivationPopup(weapon.name + " is heavily damaged");
            }
            else //BREAK IF NO DURABILITY
            {
                RpcShowActivationPopup((weapon.name + " broke"));
                EquipmentRemove(weapon, 1);
            }
        }
    }

//    public IEnumerator ApplyScriptedDamage(Entity defender, ScriptedDamage scriptedDamage, float delay)
//    {
//        yield return new WaitForSeconds(delay);
//
//        ProcessScriptedDamage(defender, scriptedDamage);
//    }
}
*/

    /*
    [Server] void ApplyDamageMethodModsToTarget(ScriptedDamage dmg)
    {
        #region PHYSICAL
        target.physicalDamageDoneMultiplier *= dmg.physicalStats.damageMultiplier;
        target.physicalVulnerability *= dmg.physicalStats.vulnerability;
        target.physicalDamageDoneBonus += dmg.physicalStats.damageBonus;
        target.physicalDamageReduction += dmg.physicalStats.damageReduction;
        #endregion
        #region MAGIC
        target.magicDamageMultiplier *= dmg.magicStats.damageMultiplier;
        target.magicVulnerability *= dmg.magicStats.vulnerability;
        target.magicDamageBonus += dmg.magicStats.damageBonus;
        target.magicDamageReduction += dmg.magicStats.damageReduction;
        #endregion
        #region BLOOD
        target.bloodDamageMultiplier *= dmg.bloodStats.damageMultiplier;
        target.bloodVulnerability *= dmg.bloodStats.vulnerability;
        target.bloodDamageBonus += dmg.bloodStats.damageBonus;
        target.bloodDamageReduction += dmg.bloodStats.damageReduction;
        #endregion
        #region SPIRIT
        target.spiritDamageMultiplier *= dmg.spiritStats.damageMultiplier;
        target.spiritVulnerability *= dmg.spiritStats.vulnerability;
        target.spiritDamageBonus += dmg.spiritStats.damageBonus;
        target.spiritDamageReduction += dmg.spiritStats.damageReduction;
        #endregion
        #region POISON
        target.poisonDamageMultiplier *= dmg.poisonStats.damageMultiplier;
        target.poisonVulnerability *= dmg.poisonStats.vulnerability;
        target.poisonDamageBonus += dmg.poisonStats.damageBonus;
        target.poisonDamageReduction += dmg.poisonStats.damageReduction;
        #endregion
        // FURY
        // STAMINA
        // MANA
        //(TEMPLATE)
        
        //target.DamageMultiplier = Stats.damageMultiplier;
        //target.DamageBonus = Stats.damageBonus;
        //target.Vulnerability = Stats.vulnerability;
        //target.DamageReduction = Stats.damageReduction;
        
    }

    [Server] void RemoveDamageMethodModsFromTarget(ScriptedDamage dmg)
    {
        #region PHYSICAL
        target.physicalDamageDoneMultiplier /= dmg.physicalStats.damageMultiplier;
        target.physicalVulnerability /= dmg.physicalStats.vulnerability;
        target.physicalDamageDoneBonus -= dmg.physicalStats.damageBonus;
        target.physicalDamageReduction -= dmg.physicalStats.damageReduction;
        #endregion
        #region MAGIC
        target.magicDamageMultiplier /= dmg.magicStats.damageMultiplier;
        target.magicVulnerability /= dmg.magicStats.vulnerability;
        target.magicDamageBonus -= dmg.magicStats.damageBonus;
        target.magicDamageReduction -= dmg.magicStats.damageReduction;
        #endregion
        #region BLOOD
        target.bloodDamageMultiplier /= dmg.bloodStats.damageMultiplier;
        target.bloodVulnerability /= dmg.bloodStats.vulnerability;
        target.bloodDamageBonus -= dmg.bloodStats.damageBonus;
        target.bloodDamageReduction -= dmg.bloodStats.damageReduction;
        #endregion
        #region SPIRIT
        target.spiritDamageMultiplier /= dmg.spiritStats.damageMultiplier;
        target.spiritVulnerability /= dmg.spiritStats.vulnerability;
        target.spiritDamageBonus -= dmg.spiritStats.damageBonus;
        target.spiritDamageReduction -= dmg.spiritStats.damageReduction;
        #endregion
        #region POISON
        target.poisonDamageMultiplier /= dmg.poisonStats.damageMultiplier;
        target.poisonVulnerability /= dmg.poisonStats.vulnerability;
        target.poisonDamageBonus -= dmg.poisonStats.damageBonus;
        target.poisonDamageReduction -= dmg.poisonStats.damageReduction;
        #endregion
        // FURY
        // STAMINA
        // MANA
    }

    // E L E M E N T A L
    [Server] void ApplyElementalModsToTarget(ScriptedDamage dmg)
    {
        #region BASE ELEMENTAL
        target.baseElementalDamageMultiplier *= dmg.elementalStats.damageMultiplier;
        target.baseElementalVulnerability *= dmg.elementalStats.vulnerability;
        target.baseElementalDamageBonus += dmg.elementalStats.damageBonus;
        target.baseElementalDamageReduction += dmg.elementalStats.damageReduction;
        #endregion
        #region FIRE
        target.fireDamageMultiplier *= dmg.fireStats.damageMultiplier;
        target.fireVulnerability *= dmg.fireStats.vulnerability;
        target.fireDamageBonus += dmg.fireStats.damageBonus;
        target.fireDamageReduction += dmg.fireStats.damageReduction;
        #endregion
        #region ICE
        target.iceDamageMultiplier *= dmg.iceStats.damageMultiplier;
        target.iceVulnerability *= dmg.iceStats.vulnerability;
        target.iceDamageBonus += dmg.iceStats.damageBonus;
        target.iceDamageReduction += dmg.iceStats.damageReduction;
        #endregion
        #region LIGHTNING
        target.lightningDamageMultiplier *= dmg.lightningStats.damageMultiplier;
        target.lightningVulnerability *= dmg.lightningStats.vulnerability;
        target.lightningDamageBonus += dmg.lightningStats.damageBonus;
        target.lightningDamageReduction += dmg.lightningStats.damageReduction;
        #endregion
        #region WATER
        target.waterDamageMultiplier *= dmg.waterStats.damageMultiplier;
        target.waterVulnerability *= dmg.waterStats.vulnerability;
        target.waterDamageBonus += dmg.waterStats.damageBonus;
        target.waterDamageReduction += dmg.waterStats.damageReduction;
        #endregion
        #region AIR
        target.airDamageMultiplier *= dmg.airStats.damageMultiplier;
        target.airVulnerability *= dmg.airStats.vulnerability;
        target.airDamageBonus += dmg.airStats.damageBonus;
        target.airDamageReduction += dmg.airStats.damageReduction;
        #endregion
        #region EARTH
        target.earthDamageMultiplier *= dmg.earthStats.damageMultiplier;
        target.earthVulnerability *= dmg.earthStats.vulnerability;
        target.earthDamageBonus += dmg.earthStats.damageBonus;
        target.earthDamageReduction += dmg.earthStats.damageReduction;
        #endregion
        #region DARK
        target.darkDamageMultiplier *= dmg.darkStats.damageMultiplier;
        target.darkVulnerability *= dmg.darkStats.vulnerability;
        target.darkDamageBonus += dmg.darkStats.damageBonus;
        target.darkDamageReduction += dmg.darkStats.damageReduction;
        #endregion
        #region HOLY
        target.holyDamageMultiplier *= dmg.holyStats.damageMultiplier;
        target.holyVulnerability *= dmg.holyStats.vulnerability;
        target.holyDamageBonus += dmg.holyStats.damageBonus;
        target.holyDamageReduction += dmg.holyStats.damageReduction;
        #endregion
        // (TEMPLATE)
        //target.DamageMultiplier = Stats.damageMultiplier;
        //target.DamageBonus = Stats.damageBonus;
        //target.Vulnerability = Stats.vulnerability;
        //target.DamageReduction = Stats.damageReduction;
    }

    [Server] void RemoveElementalModsFromTarget(ScriptedDamage dmg)
    {
        #region BASE ELEMENTAL
        target.baseElementalDamageMultiplier /= dmg.elementalStats.damageMultiplier;
        target.baseElementalVulnerability /= dmg.elementalStats.vulnerability;
        target.baseElementalDamageBonus -= dmg.elementalStats.damageBonus;
        target.baseElementalDamageReduction -= dmg.elementalStats.damageReduction;
        #endregion
        #region FIRE
        target.fireDamageMultiplier /= dmg.fireStats.damageMultiplier;
        target.fireVulnerability /= dmg.fireStats.vulnerability;
        target.fireDamageBonus -= dmg.fireStats.damageBonus;
        target.fireDamageReduction -= dmg.fireStats.damageReduction;
        #endregion
        #region ICE
        target.iceDamageMultiplier /= dmg.iceStats.damageMultiplier;
        target.iceVulnerability /= dmg.iceStats.vulnerability;
        target.iceDamageBonus -= dmg.iceStats.damageBonus;
        target.iceDamageReduction -= dmg.iceStats.damageReduction;
        #endregion
        #region LIGHTNING
        target.lightningDamageMultiplier /= dmg.lightningStats.damageMultiplier;
        target.lightningVulnerability /= dmg.lightningStats.vulnerability;
        target.lightningDamageBonus -= dmg.lightningStats.damageBonus;
        target.lightningDamageReduction -= dmg.lightningStats.damageReduction;
        #endregion
        #region WATER
        target.waterDamageMultiplier /= dmg.waterStats.damageMultiplier;
        target.waterVulnerability /= dmg.waterStats.vulnerability;
        target.waterDamageBonus -= dmg.waterStats.damageBonus;
        target.waterDamageReduction -= dmg.waterStats.damageReduction;
        #endregion
        #region AIR
        target.airDamageMultiplier /= dmg.airStats.damageMultiplier;
        target.airVulnerability /= dmg.airStats.vulnerability;
        target.airDamageBonus -= dmg.airStats.damageBonus;
        target.airDamageReduction -= dmg.airStats.damageReduction;
        #endregion
        #region EARTH
        target.earthDamageMultiplier /= dmg.earthStats.damageMultiplier;
        target.earthVulnerability /= dmg.earthStats.vulnerability;
        target.earthDamageBonus -= dmg.earthStats.damageBonus;
        target.earthDamageReduction -= dmg.earthStats.damageReduction;
        #endregion
        #region DARK
        target.darkDamageMultiplier /= dmg.darkStats.damageMultiplier;
        target.darkVulnerability /= dmg.darkStats.vulnerability;
        target.darkDamageBonus -= dmg.darkStats.damageBonus;
        target.darkDamageReduction -= dmg.darkStats.damageReduction;
        #endregion
        #region HOLY
        target.holyDamageMultiplier /= dmg.holyStats.damageMultiplier;
        target.holyVulnerability /= dmg.holyStats.vulnerability;
        target.holyDamageBonus -= dmg.holyStats.damageBonus;
        target.holyDamageReduction -= dmg.holyStats.damageReduction;
        #endregion
    }
*/
