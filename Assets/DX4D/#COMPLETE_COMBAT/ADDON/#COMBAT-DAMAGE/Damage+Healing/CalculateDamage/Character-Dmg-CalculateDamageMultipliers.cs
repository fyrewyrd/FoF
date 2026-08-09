using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - -
    // A L L  D A M A G E  M O D I F I E R S
    /// <summary>
    /// Calculate Damage Multipliers of the attacker and Defensive Multipliers of the defender
    /// </summary>
    [Server] int CalculateDamageMultipliers(int amount, CharacterSheet defender, MethodOfDamage method, Element element)
    {
        int damageModifier = GetDamageMethodMultipliers(amount, defender, method);
        damageModifier += GetElementalDamageMultipliers(amount, defender, element);
        //Debug.Log("<GETALLDAMAGEMODIFIERS.RESULT>" + damageModifier.ToString());
        return damageModifier;
    }

    // - - - - - - - - - - - - - - - - - - - - - - - -
    // E L E M E N T A L  D A M A G E  M O D I F I E R
    [Server]
    public int GetElementalDamageMultipliers(int amount, CharacterSheet defender, Element e)
    {
        // ELEMENTAL DAMAGE TYPE MULTIPLIERS
        switch (e)
        {
            case Element.Neutral:
                {
                    return 0;
                }
            case Element.Fire:
                {
                    //return (int)(amount * fireDamageMultiplier * defender.fireVulnerability) - amount; //DEPRECIATED
                    return (int)(amount * combat.elementalDamageMultipliers.fire.Get(level) * defender.combat.elementalDefenseMultipliers.fire.Get(level)) - amount;
                }
            case Element.Ice:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.ice.Get(level) * defender.combat.elementalDefenseMultipliers.ice.Get(level)) - amount;
                }
            case Element.Lightning:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.lightning.Get(level) * defender.combat.elementalDefenseMultipliers.lightning.Get(level)) - amount;
                }
            case Element.Water:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.water.Get(level) * defender.combat.elementalDefenseMultipliers.water.Get(level)) - amount;
                }
            case Element.Wind:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.wind.Get(level) * defender.combat.elementalDefenseMultipliers.wind.Get(level)) - amount;
                }
            case Element.Earth:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.earth.Get(level) * defender.combat.elementalDefenseMultipliers.earth.Get(level)) - amount;
                }
            case Element.Arcane:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.arcane.Get(level) * defender.combat.elementalDefenseMultipliers.arcane.Get(level)) - amount;
                }
            case Element.Holy:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.holy.Get(level) * defender.combat.elementalDefenseMultipliers.holy.Get(level)) - amount;
                }
			case Element.Ancient:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.ancient.Get(level) * defender.combat.elementalDefenseMultipliers.ancient.Get(level)) - amount;
                }
			case Element.Spirit:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.spirit.Get(level) * defender.combat.elementalDefenseMultipliers.spirit.Get(level)) - amount;
                }
			case Element.Runic:
                {
                    return (int)(amount * combat.elementalDamageMultipliers.runic.Get(level) * defender.combat.elementalDefenseMultipliers.runic.Get(level)) - amount;
                }
            default:
                {
                    return 0;
                }
        }
    }

    // - - - - - - - - - - - - - - - - - - - - -
    // D A M A G E  M E T H O D  M O D I F I E R
    [Server] int GetDamageMethodMultipliers(int amount, CharacterSheet defender, MethodOfDamage method)
    {
        switch (method)
        {
            case MethodOfDamage.NoDamage:
                {
                    return 0;
                }
            case MethodOfDamage.Physical:
                {
                    // P H Y S I C A L  D A M A G E
                    //return (int)(amount * physicalDamageMultiplier * defender.physicalDefenseMultiplier) - amount; //DEPRECIATED
                    return (int)(amount * combat.damageMultipliers.physical.Get(level) * defender.combat.defenseMultipliers.physical.Get(defender.level)) - amount;
                }
            case MethodOfDamage.Magic:
                {
                    // M A G I C  D A M A G E
                    return (int)(amount * combat.damageMultipliers.spell.Get(level) * defender.combat.defenseMultipliers.spell.Get(defender.level)) - amount;
                }
            case MethodOfDamage.Blood:
                {
                    // B L O O D  D A M A G E
                    return (int)(amount * combat.damageMultipliers.blood.Get(level) * defender.combat.defenseMultipliers.blood.Get(defender.level)) - amount;
                }
            case MethodOfDamage.Spirit:
                {
                    // S P I R I T  D A M A G E
                    return (int)(amount * combat.damageMultipliers.spirit.Get(level) * defender.combat.defenseMultipliers.spirit.Get(defender.level)) - amount;
                }
            case MethodOfDamage.Poison:
                {
                    // P O I S O N  D A M A G E
                    return (int)(amount * combat.damageMultipliers.poison.Get(level) * defender.combat.defenseMultipliers.poison.Get(defender.level)) - amount;
                }
            case MethodOfDamage.Mana:
                {
                    // M A N A  D A M A G E
                    return (int)(amount * combat.damageMultipliers.mana.Get(level) * defender.combat.defenseMultipliers.mana.Get(defender.level)) - amount; ;
                }
            case MethodOfDamage.Fury:
                {
                    // F U R Y  D A M A G E
                    return (int)(amount * combat.damageMultipliers.fury.Get(level) * defender.combat.defenseMultipliers.fury.Get(defender.level)) - amount; ;
                }
            case MethodOfDamage.Stamina:
                {
                    // S T A M I N A  D A M A G E
                    return (int)(amount * combat.damageMultipliers.stamina.Get(level) * defender.combat.defenseMultipliers.stamina.Get(defender.level)) - amount; ;
                }
            default:
                {
                    return 0;
                }
        }
    }
}
