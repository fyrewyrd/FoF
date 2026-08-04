using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // H E A L  T A R G E T
    [Server]
    public virtual void ApplyHealTo(CharacterSheet patient, int amount)
    {
        // A P P L Y  H E A L
        Heal(patient, amount);

        // S H O W  H E A L  P O P U P
        patient.RpcShowHealPopup(amount);
        //patient.RpcShowHealPopup(amount);// dealsDamageType, dealsElementalDamageType);

        //AGGRO
        // D R A W  A G G R O  F R O M  T A R G E T S  E N E M Y
        if (patient.target != null && patient.target.ai != null)
        {
            //GAIN HATE
            patient.target.ai.OnOpponentHealed(this, amount);
            //((Monster)patient.target).GenerateAggro(this, amount);
            //((Monster)patient.target).OnDamaged(this, amount);
        }

        // H A N D L E  P O S T  H E A L  E V E N T S
        ActiveOffensiveState = OffensiveState.HealingTarget;
        patient.ActiveDefensiveState = DefensiveState.BeingHealed;

        // H A N D L E  P O S T  C O M B A T  E V E N T S
        HandlePassiveTriggers(patient, ActiveOffensiveState, patient.ActiveDefensiveState);
    }
}
