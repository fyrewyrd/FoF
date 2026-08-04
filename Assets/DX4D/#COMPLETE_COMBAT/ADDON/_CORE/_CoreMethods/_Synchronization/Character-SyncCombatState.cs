using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - - -
    // R E F R E S H  C O M B A T  S T A T E
    // - - - - - - - - - - - - - - - - - - -
    [Server]
    void SyncCombatState(CharacterSheet attacker, CharacterSheet defender, DamageInfo.DamageValues damage, DamageInfo.DamageProperties properties)
    {
        // D I R E C T  D A M A G E
        if (properties.directDamage)
        {
            attacker.ActiveOffensiveState = OffensiveState.DealingDamage;
            defender.ActiveDefensiveState = DefensiveState.TakingDamage;
            return;
        }

        switch (damage.method)
        {
            case MethodOfDamage.NoDamage:
                {
                    attacker.ActiveOffensiveState = OffensiveState.Idle;
                    defender.ActiveDefensiveState = DefensiveState.Idle;
                    break;
                }
            case MethodOfDamage.Physical:
                {
                    //activeOffensiveState = OffensiveState.DealingDamage;
                    //defender.activeDefensiveState = DefensiveState.TakingDamage;

                    // C R I T I C A L  H I T
                    if (Random.Range(1, 100) <= attacker.combat.critChance) // C R I T - (Crits always hit)
                    {
                        // B A C K S T A B
                        if (!properties.nonSneakAttack && attacker.IsBehind(defender))
                        {
                            attacker.ActiveOffensiveState = OffensiveState.DealingCriticalBackstabDamage;
                            defender.ActiveDefensiveState = DefensiveState.TakingCriticalBackstabDamage;
                        }
                        // F L A N K I N G
                        else if (!properties.nonSneakAttack && attacker.IsFlanking(defender))
                        {
                            attacker.ActiveOffensiveState = OffensiveState.DealingCriticalFlankDamage;
                            defender.ActiveDefensiveState = DefensiveState.TakingCriticalFlankDamage;
                        }
						// O V E R W H E L M
                        else if (!properties.nonSneakAttack && attacker.IsOverwhelming(defender))
                        {
                            attacker.ActiveOffensiveState = OffensiveState.DealingCriticalOverwhelmDamage;
                            defender.ActiveDefensiveState = DefensiveState.TakingCriticalOverwhelmDamage;
                        }
                        // N O R M A L  C R I T  D A M A G E
                        else
                        {
                            attacker.ActiveOffensiveState = OffensiveState.DealingCriticalDamage;
                            defender.ActiveDefensiveState = DefensiveState.TakingCriticalDamage;
                        }
                    }
                    // B A C K S T A B
                    else if (!properties.nonSneakAttack && attacker.IsBehind(defender))
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingBackstabDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingBackstabDamage;
                    }
                    // F L A N K I N G
                    else if (!properties.nonSneakAttack && attacker.IsFlanking(defender))
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingFlankDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingFlankDamage;
                    }
					// O V E R W H E L M I N G
                    else if (!properties.nonSneakAttack && attacker.IsOverwhelming(defender))
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingOverwhelmDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingOverwhelmDamage;
                    }
                    // D O D G E  P H Y S I C A L
                    else if (!properties.unavoidable && Random.Range(1, 100) > attacker.combat.hitChance - defender.combat.dodgePhysicalChance) // D O D G E - (we use < not <= so that block rate 0 never blocks) (Dodge is preferred over Block)
                    {
                        attacker.ActiveOffensiveState = OffensiveState.AttackDodged;
                        defender.ActiveDefensiveState = DefensiveState.DodgingAttack;
                    }
                    // B L O C K  P H Y S I C A L
                    else if (!properties.unblockable && Random.Range(1, 100) <= defender.combat.blockPhysicalChance) // B L O C K (we use < not <= so that block rate 0 never blocks)
                    {
                        attacker.ActiveOffensiveState = OffensiveState.AttackBlocked;
                        defender.ActiveDefensiveState = DefensiveState.BlockingAttack;
                    }
                    // N O R M A L  D A M A G E
                    else
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingDamage;
                    }
                    break;
                }
            case MethodOfDamage.Magic:
                {
                    //activeOffensiveState = OffensiveState.DealingSpellDamage;
                    //defender.activeDefensiveState = DefensiveState.TakingSpellDamage;

                    // C R I T I C A L  S P E L L  D A M A G E
                    if (!properties.directDamage && Random.Range(1, 100) <= attacker.combat.spellCritChance) //S P E L L  C R I T I C A L
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingCriticalSpellDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingCriticalSpellDamage;
                    }
                    // D O D G E  S P E L L  D A M A G E
                    else if (!properties.unavoidable && Random.Range(1, 100) > attacker.combat.spellHitChance - defender.combat.dodgeSpellChance) //S P E L L  D O D G E
                    {
                        attacker.ActiveOffensiveState = OffensiveState.SpellDodged;
                        defender.ActiveDefensiveState = DefensiveState.DodgingSpell;
                    }
                    // B L O C K  S P E L L  D A M A G E
                    else if (!properties.unblockable && Random.Range(1, 100) <= defender.combat.blockSpellChance) //S P E L L  B L O C K
                    {
                        attacker.ActiveOffensiveState = OffensiveState.SpellBlocked;
                        defender.ActiveDefensiveState = DefensiveState.BlockingSpell;
                    }
                    else
                    {
                        attacker.ActiveOffensiveState = OffensiveState.DealingSpellDamage;
                        defender.ActiveDefensiveState = DefensiveState.TakingSpellDamage;
                    }
                    break;
                }
            case MethodOfDamage.Blood://TODO
                break;
            case MethodOfDamage.Spirit://TODO
                break;
            case MethodOfDamage.Mana://TODO
                break;
            case MethodOfDamage.Fury://TODO
                break;
            case MethodOfDamage.Stamina://TODO
                break;
            case MethodOfDamage.Poison://TODO
                break;
            default:
                break;
        }

        // E L E M E N T A L  A N D  D A M A G E  M E T H O D  R E S I S T S
        // R E F L E C T
        if (!properties.penetrateReflect && defender.resists.reflectDamage != MethodOfDamage.NoDamage && defender.Reflects(damage.method))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageReflected;
            defender.ActiveDefensiveState = DefensiveState.ReflectingDamage;
        }
        else if (!properties.penetrateReflect && defender.resists.reflectElement != Element.Neutral && defender.Reflects(damage.element))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageReflected;
            defender.ActiveDefensiveState = DefensiveState.ReflectingDamage;
        }
        // A B S O R B
        else if (!properties.nonAbsorbable && defender.resists.absorbDamage != MethodOfDamage.NoDamage && defender.Absorbs(damage.method))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageAbsorbed;
            defender.ActiveDefensiveState = DefensiveState.AbsorbingDamage;
        }
        else if (!properties.nonAbsorbable && defender.resists.absorbElement != Element.Neutral && defender.Absorbs(damage.element))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageAbsorbed;
            defender.ActiveDefensiveState = DefensiveState.AbsorbingDamage;
        }

        // V U L N E R A B I L I T I E S
        //ACHILLES
        if (defender.weakness.veryWeakToDamage != MethodOfDamage.NoDamage && defender.weakness.veryWeakToDamage == damage.method)
        {
            attacker.ActiveOffensiveState = OffensiveState.DamagingAchillesHeel;
            defender.ActiveDefensiveState = DefensiveState.TakingAchillesHeelDamage;
        }
        else if (defender.weakness.veryWeakToElement != Element.Neutral && defender.weakness.veryWeakToElement == damage.element)
        {
            attacker.ActiveOffensiveState = OffensiveState.DamagingAchillesHeel;
            defender.ActiveDefensiveState = DefensiveState.TakingAchillesHeelDamage;
        }
        //WEAKNESS
        else if (defender.weakness.weakToDamage != MethodOfDamage.NoDamage && defender.weakness.weakToDamage == damage.method)
        {
            attacker.ActiveOffensiveState = OffensiveState.DamagingWeakness;
            defender.ActiveDefensiveState = DefensiveState.TakingWeakToDamage;
        }
        else if (defender.weakness.weakToElement != Element.Neutral && defender.weakness.weakToElement == damage.element)
        {
            attacker.ActiveOffensiveState = OffensiveState.DamagingWeakness;
            defender.ActiveDefensiveState = DefensiveState.TakingWeakToDamage;
        }

        // R E S I S T A N C E S
        //IGNORE
        else if (!properties.nonResistable && defender.resists.negateDamage != MethodOfDamage.NoDamage && defender.Negates(damage.method))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageIgnored;
            defender.ActiveDefensiveState = DefensiveState.IgnoringDamage;
        }
        else if (!properties.nonResistable && defender.resists.negateElement != Element.Neutral && defender.Negates(damage.element))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageIgnored;
            defender.ActiveDefensiveState = DefensiveState.IgnoringDamage;
        }
        //RESIST
        else if (!properties.nonResistable && defender.resists.resistDamage != MethodOfDamage.NoDamage && defender.Resists(damage.method))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageResisted;
            defender.ActiveDefensiveState = DefensiveState.ResistingDamage;
        }
        else if (!properties.nonResistable && defender.resists.resistElement != Element.Neutral && defender.Resists(damage.element))
        {
            attacker.ActiveOffensiveState = OffensiveState.DamageResisted;
            defender.ActiveDefensiveState = DefensiveState.ResistingDamage;
        }
    }
}
