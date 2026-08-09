using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - -
    // H A N D L E  T R I G G E R S
    // O N  C O M B A T  C O N D I T I O N S
    //TODO: We passed in playerOffensiveState to ensure that our states match the defender's...we should do the same with defender's DefensiveState as well
    [Server] void HandleDamageTriggers(CharacterSheet defender, MethodOfDamage method, Element element)
    {
        // A T T A C K E R  D A M A G E  M E T H O D
        switch (method)
        {
            case MethodOfDamage.NoDamage: { break; }
            case MethodOfDamage.Physical:
                {
                    // P H Y S I C A L  D A M A G E
                    HandleTrigger(PassiveTrigger.DamageDealt);
                    defender.HandleTrigger(PassiveTrigger.DamageTaken);
                    break;
                }
            case MethodOfDamage.Magic:
                {
                    // M A G I C  D A M A G E
                    HandleTrigger(PassiveTrigger.MagicDMGDealt);
                    defender.HandleTrigger(PassiveTrigger.MagicDMGTaken);
                    break;
                }
            case MethodOfDamage.Blood:
                {
                    // B L O O D  D A M A G E
                    break;
                }
            case MethodOfDamage.Spirit:
                {
                    // S P I R I T  D A M A G E
                    break;
                }
            case MethodOfDamage.Poison:
                {
                    // P O I S O N  D A M A G E
                    break;
                }
            case MethodOfDamage.Mana:
                {
                    // M A N A  D A M A G E
                    break;
                }
            case MethodOfDamage.Fury:
                {
                    // F U R Y  D A M A G E
                    break;
                }
            case MethodOfDamage.Stamina:
                {
                    // S T A M I N A  D A M A G E
                    break;
                }
        }
        // A T T A C K E R  B A S E  E L E M E N T A L  D A M A G E
        if (element != Element.Neutral)
        {
        }

        // A T T A C K E R  E L E M E N T A L  D A M A G E
        switch (element)
        {
            case Element.Neutral:
                {
                    break;
                }
            case Element.Fire:
                {
                    break;
                }
            case Element.Ice:
                {
                    break;
                }
            case Element.Lightning:
                {
                    break;
                }
            case Element.Water:
                {
                    break;
                }
            case Element.Wind:
                {
                    break;
                }
            case Element.Earth:
                {
                    break;
                }
            case Element.Arcane:
                {
                    break;
                }
            case Element.Holy:
                {
                    break;
                }
			case Element.Ancient:
                {
                    break;
                }
			case Element.Spirit:
                {
                    break;
                }
			case Element.Runic:
                {
                    break;
                }
        }
    }
}

/*RANDOM IDEA
 * 

    dual switch (group1, group2) //groups must be different types
    {
        case (group1.value) ...
        case (group2.value) ...
        dual case (group1.value, group2.value) ...
    }
     */
