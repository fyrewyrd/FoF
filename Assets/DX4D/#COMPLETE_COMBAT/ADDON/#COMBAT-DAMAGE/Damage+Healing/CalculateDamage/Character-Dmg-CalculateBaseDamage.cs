using Mirror;
using System.Linq;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - - - -
    // D A M A G E  B O N U S E S
    [Server] //TODO: DefenseInfo
    int CalculateBaseDamageBonus(CharacterSheet defender, MethodOfDamage method, Element element)
    {
        int damageBonus = 0;
        damageBonus += GetBaseDamageBonus(defender, method);
        damageBonus += GetBaseElementalDamageBonus(defender, element);
        //Debug.Log("<BASEDAMAGE.RESULT>" + damageBonus); //DEBUG
        return damageBonus;
    }
    
    // - - - - - - - - - - - - - - - - - - - - - - -
    // E L E M E N T A L  D A M A G E  B O N U S E S
    [Server] //TODO: ElementalDefense ElementalDamage
    int GetBaseElementalDamageBonus(CharacterSheet defender, Element element)
    {
        // ELEMENTAL DAMAGE BONUSES
        switch (element)
        {
            case Element.Neutral:
                {
                    return 0;
                }
            case Element.Fire:
                {
                    return 0;
                }
            case Element.Ice:
                {
                    return 0;
                }
            case Element.Lightning:
                {
                    return 0;
                }
            case Element.Water:
                {
                    return 0;
                }
            case Element.Wind:
                {
                    return 0;
                }
            case Element.Earth:
                {
                    return 0;
                }
            case Element.Arcane:
                {
                    return 0;
                }
            case Element.Holy:
                {
                    return 0;
                }
			case Element.Ancient:
                {
                    return 0;
                }
			case Element.Spirit:
                {
                    return 0;
                }
			case Element.Runic:
                {
                    return 0;
                }
            default:
                {
                    return 0;
                }
        }
    }
    // - - - - - - - - - - - - - - - - - - - -
    // D A M A G E  M E T H O D  B O N U S E S
    [Server] int GetBaseDamageBonus(CharacterSheet defender, MethodOfDamage method)
    {
        switch (method)
        {
            case MethodOfDamage.Physical:
                {
                    // P H Y S I C A L  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.armor;

                    return calculatedDamage - calculatedDefense;
                    /* //NOTE: uMMORPG already does these calculations...they are included here for reference to make sure all damage is accounted for
                    //WEAPON AND ARMOR CALCULATIONS FOR PLAYER CHARACTERS
                    if (this is Player)
                    {
                        Player attackingPlayer = (this as Player);

                        //PASSIVE SKILL DAMAGE BONUS
                        calculatedDamage += (from skill in skills where skill.level > 0 && skill.data is PassiveSkill
                                            select ((PassiveSkill)skill.data).bonusDamage.Get(skill.level)).Sum();

                        //BUFF DAMAGE BONUS
                        calculatedDamage += buffs.Sum(buff => buff.bonusDamage);

                        //GEAR DAMAGE BONUS
                        calculatedDamage += (from slot in equipment where slot.amount > 0
                                              select ((EquipmentItem)slot.item.data).damageBonus).Sum();
                    }
                    if (defender is Player)
                    {
                        Player defendingPlayer = (defender as Player);

                        //PASSIVE SKILL DEFENSE BONUS
                        calculatedDefense += (from skill in skills where skill.level > 0 && skill.data is PassiveSkill
                                            select ((PassiveSkill)skill.data).bonusDefense.Get(skill.level)).Sum();

                        //BUFF DEFENSE BONUS
                        calculatedDefense += buffs.Sum(buff => buff.bonusDefense);

                        //GEAR DEFENSE BONUS
                        calculatedDefense += (from slot in defendingPlayer.equipment where slot.amount > 0
                                              select ((EquipmentItem)slot.item.data).defenseBonus).Sum();
                    }*/
                }
            case MethodOfDamage.Magic:
                {
                    // M A G I C  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Blood:
                {
                    // B L O O D  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Spirit:
                {
                    // S P I R I T  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Poison:
                {
                    // P O I S O N  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Mana:
                {
                    // M A N A  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Fury:
                {
                    // F U R Y  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            case MethodOfDamage.Stamina:
                {
                    // S T A M I N A  D A M A G E
                    int calculatedDamage = combat.attack;
                    int calculatedDefense = defender.combat.willpower;

                    return calculatedDamage - calculatedDefense;
                    //return 0;
                }
            default:
                {
                    return 0;
                }
        }
    }
}
