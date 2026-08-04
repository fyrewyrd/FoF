using Mirror;
using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    // - - - - - - - - - - - - - -
    // R E F R E S H  S K I L L S
    [Server] public void SyncCurrentSkillToCharacter()
    {
        // R E F R E S H  S K I L L  D A M A G E  T Y P E S
        if (IsNotCastingASkill) return; // || currentSkill < 1) return;

        if (!(SKILLS[currentSkill].data is DamageSkill)) return;

        if (IsCastingSiegeWeaponSkill && HasSiegeWeapon)// && siegeWeapon.weaponHand != WeaponHand.Unarmed) //SIEGE WEAPON
        {
            SetDamage(siegeWeapon.damage.method, siegeWeapon.damage.element);
        }
        else if (IsCastingMainWeaponSkill && HasMainWeapon)// && mainWeapon.weaponHand != WeaponHand.Unarmed) //MAIN WEAPON
        {
            SetDamage(mainWeapon.damage.method, mainWeapon.damage.element);

            //SYNC WEAPON
            //dealsStatusEffectType = mainWeapon.dealsStatusEffect; //DEPRECIATED
            //onHitDamageEffect = mainWeapon.onHitDamageEffect;

            //SYNC SKILL
            //skills[0].data.castRange = new LinearFloat() { baseValue = mainWeapon.attackRange, bonusPerLevel = skills[0].data.castRange.bonusPerLevel };
        }
        else if (IsCastingOffhandWeaponSkill && HasOffhandWeapon) //OFFHAND
        {
            SetDamage(offhandWeapon.damage.method, offhandWeapon.damage.element);
        }
        else if (IsCastingUnarmedSkill && IsUnarmed && unarmedWeapon != null) //UNARMED
        {
            SetDamage(unarmedWeapon.damage.method, unarmedWeapon.damage.element);
            //dealsStatusEffectType = unarmedWeapon.dealsStatusEffect; //DEPRECIATED
        }
        else if (IsNotCastingASkill) //FALLBACK FOR NON DX4D WEAPONS
        {
            ResetDamage();
            //SetDamage(innateMethodOfDamage, innateDamageElement);
#if UNITY_EDITOR
            Debug.Log(">>>" + name + "<<<" + " NO COMBAT WEAPON EQUIPPED AND NO UNARMED ATTACK " + ">>>" + "\n(see the DX4D complete combat manual for info on how to set up your player class with an unarmed attack)"); //DEBUG
#endif
        }
        else
        {
            DamageSkill s = ((DamageSkill)SKILLS[currentSkill].data);
            if (s != null)
            {
                //Debug.Log(name.ToUpper() + " INVOKING DAMAGE EVENTS ON " + s.name.ToUpper()); //DEBUG

                SetDamage(s.skillDamage.method, s.skillDamage.element);
                //dealsStatusEffectType = s.initialDamage.damageStatusEffect; //DEPRECIATED
                //Debug.Log(s.name.ToUpper() + " CAST BY " + name.ToUpper()); //DEBUG
                //s.initialDamage.ProcessScriptedDamage();
                //Invoke("ProcessAddedDamage", s.addedDamageDelay);
            }
        }
    }
}
