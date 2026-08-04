using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class PlayerCharacter : CharacterSheet
{
    [Header("EQUIPPED ARMORS")]
    [SerializeField] public List<CombatArmor> equippedArmors = new List<CombatArmor>();

    [Server] public void SyncArmorToCharacter()
    {
        //ResetResists(); //DEPRECIATED - Needs to come before both Armor and Accessories

        if (equippedArmors.Count < 1) return; //No armor to deal with

        //foreach (EnchantedArmor armor in equippedEnchantedArmors)
        foreach (CombatArmor armor in equippedArmors)
        {
                // A S S I G N  A R M O R  D A M A G E  M E T H O D  R E S I S T S
                if (resists.reflectDamage == MethodOfDamage.NoDamage) resists.reflectDamage = armor.resists.reflectDamage;
                if (resists.absorbDamage == MethodOfDamage.NoDamage) resists.absorbDamage = armor.resists.absorbDamage;
                if (resists.negateDamage == MethodOfDamage.NoDamage) resists.negateDamage = armor.resists.negateDamage;
                if (resists.resistDamage == MethodOfDamage.NoDamage) resists.resistDamage = armor.resists.resistDamage;
                
                // A S S I G N  A R M O R  E L E M E N T  R E S I S T S
                if (resists.reflectElement == Element.Neutral) resists.reflectElement = armor.resists.reflectElement;
                if (resists.absorbElement == Element.Neutral) resists.absorbElement = armor.resists.absorbElement;
                if (resists.negateElement == Element.Neutral) resists.negateElement = armor.resists.negateElement;
                if (resists.resistElement == Element.Neutral) resists.resistElement = armor.resists.resistElement;

                //WEAKNESSES
                if (weakness.weakToDamage == MethodOfDamage.NoDamage) weakness.weakToDamage = armor.weakness.weakToDamage;
                if (weakness.veryWeakToDamage == MethodOfDamage.NoDamage) weakness.veryWeakToDamage = armor.weakness.veryWeakToDamage;
                if (weakness.weakToElement == Element.Neutral) weakness.weakToElement = armor.weakness.weakToElement;
                if (weakness.veryWeakToElement == Element.Neutral) weakness.veryWeakToElement = armor.weakness.veryWeakToElement;

                Debug.Log(name.ToUpper() + " ARMOR RESISTS LOADED: \n" + armor.name); //DEBUG
        }
    }
    // I S  W E A R I N G  E N C H A N T E D  A R M O R
    //public bool IsWearingEnchantedArmor { get { return (equippedArmors.Count > 0); } }

    // I S  N O T  W E A R I N G  E N C H A N T E D  A R M O R
    //public bool IsNotWearingEnchantedArmor { get { return (!IsWearingEnchantedArmor); } }

        /*
    // R E F R E S H  E N C H A N T E D  A R M O R
    [Server] public void RefreshEnchantedArmor()
    {
        // R E S E T  R E S I S T S
        ResetResists();

        //if (IsCastingSkill) return; // Make sure not to overwrite skill damage

        // C A L C U L A T E  A R M O R  R E S I S T S
        if (IsWearingEnchantedArmor)
        {
            foreach (ElementalArmor armor in equippedArmors)
            {
                // A S S I G N  A R M O R  E L E M E N T  A N D  D A M A G E  M E T H O D  R E S I S T S
                //reflect
                //if (armor.reflectsDamageType != MethodOfDamage.NoDamage) reflectsDamageType = armor.reflectsDamageType;
                //absorb
                //if (armor.absorbsDamageType != MethodOfDamage.NoDamage) absorbsDamageType = armor.absorbsDamageType;
                //negate
                //if (armor.negatesDamageType != MethodOfDamage.NoDamage) negatesDamageType = armor.negatesDamageType;
                //resist
                //if (armor.resistsDamageType != MethodOfDamage.NoDamage) resistsDamageType = armor.resistsDamageType;
                //weakTo
                //if (armor.weakToDamageType != MethodOfDamage.NoDamage) weakToDamageType = armor.weakToDamageType;
                //veryWeakTo
                //if (armor.veryWeakToDamageType != MethodOfDamage.NoDamage) veryWeakToDamageType = armor.veryWeakToDamageType;

                if (armor.reflectsElement != Element.Neutral) reflectsElement = armor.reflectsElement;
                if (armor.absorbsElement != Element.Neutral) absorbsElement = armor.absorbsElement;
                if (armor.negatesElement != Element.Neutral) negatesElement = armor.negatesElement;
                if (armor.resistsElement != Element.Neutral) resistsElement = armor.resistsElement;
                if (armor.weakToElement != Element.Neutral) weakToElement = armor.weakToElement;
                if (armor.veryWeakToElement != Element.Neutral) veryWeakToElement = armor.veryWeakToElement;
            }

            // D E B U G G I N G
            Debug.Log(name.ToUpper() + " ARMOR RESISTS APPLIED: \n" + equippedArmors.ToString()); //DEBUG
        }
    }
        */
}
