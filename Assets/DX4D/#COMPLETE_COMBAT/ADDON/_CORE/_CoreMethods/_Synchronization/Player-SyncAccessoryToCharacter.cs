using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class PlayerCharacter : CharacterSheet
{
    [Header("EQUIPPED ACCESSORIES")]
    [SerializeField] public List<CombatAccessory> equippedAccessories = new List<CombatAccessory>();

    [Server] public void SyncAccessoriesToCharacter()
    {
        //ResetResists(); //DEPRECIATED - Needs to come before both Armor and Accessories

        if (equippedAccessories.Count < 1) return; //No armor to deal with
        
        foreach (CombatAccessory accessory in equippedAccessories)
        {
                // A S S I G N  A R M O R  D A M A G E  M E T H O D  R E S I S T S
                if (resists.reflectDamage == MethodOfDamage.NoDamage) resists.reflectDamage = accessory.resists.reflectDamage;
                if (resists.absorbDamage == MethodOfDamage.NoDamage) resists.absorbDamage = accessory.resists.absorbDamage;
                if (resists.negateDamage == MethodOfDamage.NoDamage) resists.negateDamage = accessory.resists.negateDamage;
                if (resists.resistDamage == MethodOfDamage.NoDamage) resists.resistDamage = accessory.resists.resistDamage;
                
                // A S S I G N  A R M O R  E L E M E N T  R E S I S T S
                if (resists.reflectElement == Element.Neutral) resists.reflectElement = accessory.resists.reflectElement;
                if (resists.absorbElement == Element.Neutral) resists.absorbElement = accessory.resists.absorbElement;
                if (resists.negateElement == Element.Neutral) resists.negateElement = accessory.resists.negateElement;
                if (resists.resistElement == Element.Neutral) resists.resistElement = accessory.resists.resistElement;


                if (weakness.weakToDamage == MethodOfDamage.NoDamage) weakness.weakToDamage = accessory.weakness.weakToDamage;
                if (weakness.veryWeakToDamage == MethodOfDamage.NoDamage) weakness.veryWeakToDamage = accessory.weakness.veryWeakToDamage;
                if (weakness.weakToElement == Element.Neutral) weakness.weakToElement = accessory.weakness.weakToElement;
                if (weakness.veryWeakToElement == Element.Neutral) weakness.veryWeakToElement = accessory.weakness.veryWeakToElement;

                //Debug.Log(name.ToUpper() + " ACCESSORY RESISTS LOADED: \n" + accessory.name); //DEBUG
        }
    }
}
