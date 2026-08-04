using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    [Server] int CalculateStateBasedDamageBonuses(CharacterSheet defender, DamageInfo.DamageValues damage, DamageInfo.DamageProperties properties)//, StatusEffectList status)
    {
        int amount = damage.total;

        switch (ActiveOffensiveState)
        {
            case OffensiveState.Idle:
                {
                    return 0; //no bonus
                }
            case OffensiveState.HealingTarget:
                {
                    ApplyHealTo(defender, amount);
                    return -amount; //negate damage
                }


            // -- N O R M A L  D A M A G E --
            case OffensiveState.DealingDamage:
                {
                    return 0; //no bonus
                }
            case OffensiveState.DealingSpellDamage:
                {
                    return 0; //no bonus
                }


            // -- C R I T I C A L  D A M A G E --
            // C R I T I C A L
            case OffensiveState.DealingCriticalDamage:
                {
                    if (properties.directDamage) return 0;
                    return (int)(amount * combat.critDamageMultiplier) - amount;
                }
            case OffensiveState.DealingCriticalSpellDamage:
                {
                    if (properties.directDamage) return 0;
                    return (int)(amount * combat.spellCritDamageMultiplier) - amount;
                }


            // -- D A M A G E  R E F L E C T --
            case OffensiveState.DamageReflected:
                {
                    if (properties.penetrateReflect) return 0; //no bonus
                    if (defender != null) defender.DealFlatDamage(this, damage);//, status); //We use DealPenetratingDamage here to prevent a reflect loop
                    return -amount; //negate damage
                }


            // -- D A M A G E  A B S O R B --
            case OffensiveState.DamageAbsorbed:
                {
                    if (!properties.nonAbsorbable) return 0; //no bonus
                    ApplyHealTo(defender, amount);
                    return -amount; //negate damage
                }

            // -- D A M A G E  R E S I S T A N C E --
            // R E S I S T
            case OffensiveState.DamageResisted:
                {
                    if (properties.nonResistable) return 0; //no bonus
                    return (int)((amount * ResistConfig.resistanceMultiplier) - amount);
                }
            case OffensiveState.DamagingWeakness:
                {
                    if (properties.nonResistable) return 0; //no bonus
                    return (int)(amount * ResistConfig.weakToMultiplier) - amount;
                }
            case OffensiveState.DamagingAchillesHeel:
                {
                    if (properties.nonResistable) return 0; //no bonus
                    return (int)(amount * ResistConfig.veryWeakToMultiplier) - amount;
                }
            // I G N O R E
            case OffensiveState.DamageIgnored:
                {
                    if (properties.nonResistable) return 0; //no bonus
                    return -amount; //negate damage
                }

            // -- D A M A G E  A V O I D A N C E --
            // D O D G E D
            case OffensiveState.AttackDodged:
                {
                    if (properties.unavoidable) return 0; //no bonus
                    return (int)(amount * defender.combat.dodgePhysicalDamageMultiplier) - amount;
                }
            case OffensiveState.SpellDodged:
                {
                    if (properties.unavoidable) return 0; //no bonus
                    return (int)(amount * defender.combat.dodgeSpellDamageMultiplier) - amount;
                }
            // B L O C K E D
            case OffensiveState.AttackBlocked:
                {
                    if (properties.unblockable) return 0; //no bonus
                    return (int)(amount * defender.combat.blockPhysicalDamageMultiplier) - amount;
                }
            case OffensiveState.SpellBlocked:
                {
                    if (properties.unblockable) return 0; //no bonus
                    return (int)(amount * defender.combat.blockSpellDamageMultiplier) - amount;
                }


            // -- D I R E C T I O N A L  D A M A G E --
            // B A C K S T A B
            case OffensiveState.DealingBackstabDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.backstabDamageMultiplier) - amount;
                }
            case OffensiveState.DealingCriticalBackstabDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.critDamageMultiplier * combat.backstabDamageMultiplier) - amount;
                }
            // F L A N K
            case OffensiveState.DealingFlankDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.flankDamageMultiplier) - amount;
                }
            case OffensiveState.DealingCriticalFlankDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.critDamageMultiplier * combat.flankDamageMultiplier) - amount;
                }
				 // O V E R W H E L M
            case OffensiveState.DealingOverwhelmDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.overwhelmDamageMultiplier) - amount;
                }
            case OffensiveState.DealingCriticalOverwhelmDamage:
                {
                    if (properties.nonSneakAttack) return 0; //no bonus
                    return (int)(amount * combat.critDamageMultiplier * combat.overwhelmDamageMultiplier) - amount;
                }
        }

        return 0; //no bonus
    }
}
