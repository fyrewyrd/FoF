using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - -
    // H A N D L E  T R I G G E R S
    // O N  C O M B A T  C O N D I T I O N S
    //TODO: We passed in playerOffensiveState to ensure that our states match the defender's...we should do the same with defender's DefensiveState as well
    [Server] void HandlePassiveTriggers(CharacterSheet defender, OffensiveState playerOffensiveState, DefensiveState opponentDefensiveState)
    {
        /* //REMOVED FOR NOW, COULD BE USEFUL LATER - NOW IN Character-Recover.cs
        // A T T A C K E R  I N  C O M B A T
        if (combat.IsInCombat) { HandleTrigger(PassiveTrigger.InCombat); }
        else { HandleTrigger(PassiveTrigger.NotInCombat); }

        // D E F E N D E R  I N  C O M B A T
        if (defender.combat.IsInCombat) { defender.HandleTrigger(PassiveTrigger.InCombat); }
        else { defender.HandleTrigger(PassiveTrigger.NotInCombat); }
        */

        // A T T A C K E R  O F F E N S I V E  S T A T E
        switch (playerOffensiveState)
        {
            case OffensiveState.Idle:
                {
                    HandleTrigger(PassiveTrigger.NotMoving);
                    break;
                }
            case OffensiveState.HealingTarget:
                {
                    HandleTrigger(PassiveTrigger.HealTarget);
                    break;
                }
            case OffensiveState.DealingDamage:
                {
                    HandleTrigger(PassiveTrigger.DamageDealt);
                    break;
                }
            case OffensiveState.DealingSpellDamage:
                {
                    HandleTrigger(PassiveTrigger.SpellDMGDealt);
                    break;
                }
            case OffensiveState.AttackDodged:
                {
                    HandleTrigger(PassiveTrigger.MyAttackDodged);
                    break;
                }
            case OffensiveState.SpellDodged:
                {
                    HandleTrigger(PassiveTrigger.MySpellDodged);
                    break;
                }
            case OffensiveState.AttackBlocked:
                {
                    HandleTrigger(PassiveTrigger.MyAttackBlocked);
                    break;
                }
            case OffensiveState.SpellBlocked:
                {
                    HandleTrigger(PassiveTrigger.MySpellBlocked);
                    break;
                }
            case OffensiveState.DealingCriticalDamage:
                {
                    HandleTrigger(PassiveTrigger.CriticalHit);
                    break;
                }
            case OffensiveState.DealingCriticalSpellDamage:
                {
                    HandleTrigger(PassiveTrigger.SpellCrit);
                    break;
                }
            case OffensiveState.DealingBackstabDamage:
                {
                    HandleTrigger(PassiveTrigger.Backstab);
                    break;
                }
            case OffensiveState.DealingCriticalBackstabDamage:
                {
                    HandleTrigger(PassiveTrigger.BackstabCrit);
                    break;
                }
        }

        // D E F E N D E R  D E F E N S I V E  S T A T E
        switch (opponentDefensiveState)
        {
            case DefensiveState.Idle:
                {
                    defender.HandleTrigger(PassiveTrigger.NotMoving);
                    break;
                }
            case DefensiveState.BeingHealed:
                {
                    defender.HandleTrigger(PassiveTrigger.BeingHealed);
                    break;
                }
            case DefensiveState.TakingDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.DamageTaken);
                    break;
                }
            case DefensiveState.TakingSpellDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.SpellDMGTaken);
                    break;
                }
            case DefensiveState.DodgingAttack:
                {
                    defender.HandleTrigger(PassiveTrigger.DodgeAttack);
                    break;
                }
            case DefensiveState.DodgingSpell:
                {
                    defender.HandleTrigger(PassiveTrigger.DodgeSpell);
                    break;
                }
            case DefensiveState.BlockingAttack:
                {
                    defender.HandleTrigger(PassiveTrigger.BlockAttack);
                    break;
                }
            case DefensiveState.BlockingSpell:
                {
                    defender.HandleTrigger(PassiveTrigger.BlockSpell);
                    break;
                }
            case DefensiveState.TakingCriticalDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.CritTaken);
                    break;
                }
            case DefensiveState.TakingCriticalSpellDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.SpellCritTaken);
                    break;
                }
            case DefensiveState.TakingBackstabDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.BackstabTaken);
                    break;
                }
            case DefensiveState.TakingCriticalBackstabDamage:
                {
                    defender.HandleTrigger(PassiveTrigger.BackstabCritTaken);
                    break;
                }
        }
    }
}
