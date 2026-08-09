using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - - - -
    // D A M A G E  B O N U S E S
    [Server] //TODO: DefenseInfo
    int CalculateDamageModifiers(CharacterSheet defender, MethodOfDamage method, Element element)
    {
        int damageBonus = 0;
        damageBonus += GetElementalBonuses(defender, element);
        damageBonus += GetDamageMethodBonuses(defender, method);
        //Debug.Log("<GETALLDAMAGEBONUSES.RESULT>" + damageBonus); //DEBUG
        return damageBonus;
    }
    
    // - - - - - - - - - - - - - - - - - - - - - - -
    // E L E M E N T A L  D A M A G E  B O N U S E S
    [Server] //TODO: ElementalDefense ElementalDamage
    int GetElementalBonuses(CharacterSheet defender, Element element)
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
                    //return (fireDamageBonus - defender.fireDamageReduction); //DEPRECIATED
                    return (combat.elementalDamageBonuses.fire.Get(level) - defender.combat.elementalDefenseBonuses.fire.Get(level));
                }
            case Element.Ice:
                {
                    return (combat.elementalDamageBonuses.ice.Get(level) - defender.combat.elementalDefenseBonuses.ice.Get(level));
                }
            case Element.Lightning:
                {
                    return (combat.elementalDamageBonuses.lightning.Get(level) - defender.combat.elementalDefenseBonuses.lightning.Get(level));
                }
            case Element.Water:
                {
                    return (combat.elementalDamageBonuses.water.Get(level) - defender.combat.elementalDefenseBonuses.water.Get(level));
                }
            case Element.Wind:
                {
                    return (combat.elementalDamageBonuses.wind.Get(level) - defender.combat.elementalDefenseBonuses.wind.Get(level));
                }
            case Element.Earth:
                {
                    return (combat.elementalDamageBonuses.earth.Get(level) - defender.combat.elementalDefenseBonuses.earth.Get(level));
                }
            case Element.Arcane:
                {
                    return (combat.elementalDamageBonuses.arcane.Get(level) - defender.combat.elementalDefenseBonuses.arcane.Get(level));
                }
            case Element.Holy:
                {
                    return (combat.elementalDamageBonuses.holy.Get(level) - defender.combat.elementalDefenseBonuses.holy.Get(level));
                }
			case Element.Ancient:
                {
                    return (combat.elementalDamageBonuses.ancient.Get(level) - defender.combat.elementalDefenseBonuses.ancient.Get(level));
                }
			case Element.Spirit:
                {
                    return (combat.elementalDamageBonuses.spirit.Get(level) - defender.combat.elementalDefenseBonuses.spirit.Get(level));
                }
			case Element.Runic:
                {
                    return (combat.elementalDamageBonuses.runic.Get(level) - defender.combat.elementalDefenseBonuses.runic.Get(level));
                }
            default:
                {
                    return 0;
                }
        }
    }
    // - - - - - - - - - - - - - - - - - - - -
    // D A M A G E  M E T H O D  B O N U S E S
    [Server] int GetDamageMethodBonuses(CharacterSheet defender, MethodOfDamage method)
    {
        switch (method)
        {
            case MethodOfDamage.Physical:
                {
                    // P H Y S I C A L  D A M A G E
                    return combat.damageBonuses.physical.Get(level) - defender.combat.defenseBonuses.physical.Get(defender.level); // - defender.defense;
                }
            case MethodOfDamage.Magic:
                {
                    // M A G I C  D A M A G E
                    //return magicDamageBonus - defender.spellDefense; //DEPRECIATED
                    return combat.damageBonuses.spell.Get(level) - defender.combat.defenseBonuses.spell.Get(defender.level);
                }
            case MethodOfDamage.Blood:
                {
                    // B L O O D  D A M A G E
                    return combat.damageBonuses.blood.Get(level) - defender.combat.defenseBonuses.blood.Get(defender.level);
                }
            case MethodOfDamage.Spirit:
                {
                    // S P I R I T  D A M A G E
                    return combat.damageBonuses.spirit.Get(level) - defender.combat.defenseBonuses.spirit.Get(defender.level);
                }
            case MethodOfDamage.Poison:
                {
                    // P O I S O N  D A M A G E
                    return combat.damageBonuses.poison.Get(level) - defender.combat.defenseBonuses.poison.Get(defender.level);
                }
            case MethodOfDamage.Mana:
                {
                    // M A N A  D A M A G E
                    return combat.damageBonuses.mana.Get(level) - defender.combat.defenseBonuses.mana.Get(defender.level);
                }
            case MethodOfDamage.Fury:
                {
                    // F U R Y  D A M A G E
                    return combat.damageBonuses.fury.Get(level) - defender.combat.defenseBonuses.fury.Get(defender.level);
                }
            case MethodOfDamage.Stamina:
                {
                    // S T A M I N A  D A M A G E
                    return combat.damageBonuses.stamina.Get(level) - defender.combat.defenseBonuses.stamina.Get(defender.level);
                }
            default:
                {
                    return 0;
                }
        }
    }
}
