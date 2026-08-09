using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - -   - - - - - - - -
    // H A N D L E   T R I G G E R S
    // - - - - - -   - - - - - - - -
    [Server] public void HandleTrigger(PassiveTrigger trigger)
    {
        if (IsDead) return; //DON'T REGEN WHILE DEAD

        if (!ShieldIsBroken()) AdjustShield(trigger);	
		if (!BarrierIsBroken()) AdjustBarrier(trigger);

        AdjustLife(trigger);    AdjustBlood(trigger);   AdjustSpirit(trigger);
        AdjustMana(trigger);    AdjustFury(trigger);    AdjustStamina(trigger);

        AdjustGold(trigger);    AdjustGems(trigger);
        //AdjustCopper(trigger);    AdjustSilver(trigger);    AdjustPlatinum(trigger); //TODO

        AdjustExperience(trigger);
    }

    // S H I E L D
    [Server]
    public void AdjustShield(PassiveTrigger trigger)
    {
        if (TriggerIsActive(stats.shieldGainTriggers, trigger)) { SHIELD += stats.shieldGainRate; }
        if (TriggerIsActive(stats.shieldLossTriggers, trigger)) { SHIELD -= stats.shieldLossRate; }
    }
	
	// B A R R I E R
	[Server]
    public void AdjustBarrier(PassiveTrigger trigger)
    {
        if (TriggerIsActive(stats.barrierGainTriggers, trigger)) { BARRIER += stats.barrierGainRate; }
        if (TriggerIsActive(stats.barrierLossTriggers, trigger)) { BARRIER -= stats.barrierLossRate; }
    }

    // L I F E
    [Server]
    public void AdjustLife(PassiveTrigger trigger)
    {
        if (TriggerIsActive(stats.lifeGainTriggers, trigger)) { LIFE += stats.lifeGainRate; }
        if (TriggerIsActive(stats.lifeLossTriggers, trigger)) { LIFE -= stats.lifeLossRate; }
    }
    // B L O O D
    [Server]
    public void AdjustBlood(PassiveTrigger trigger)
    {
        if (TriggerIsActive(stats.bloodGainTriggers, trigger)) { BLOOD += stats.bloodGainRate; }
        if (TriggerIsActive(stats.bloodLossTriggers, trigger)) { BLOOD -= stats.bloodLossRate; }
    }
    // S P I R I T
    [Server]
    public void AdjustSpirit(PassiveTrigger trigger)
    {
        if (TriggerIsActive(stats.spiritGainTriggers, trigger)) { SPIRIT += stats.spiritGainRate; }
        if (TriggerIsActive(stats.spiritLossTriggers, trigger)) { SPIRIT -= stats.spiritLossRate; }
    }



    // M A N A
    [Server] public void AdjustMana(PassiveTrigger trigger) {
        if (TriggerIsActive(stats.manaGainTriggers, trigger)) { MANA += stats.manaGainRate; }
        if (TriggerIsActive(stats.manaLossTriggers, trigger)) { MANA -= stats.manaLossRate; }
    }
    // F U R Y
    [Server] public void AdjustFury(PassiveTrigger trigger) {
        if (TriggerIsActive(stats.furyGainTriggers, trigger)) { FURY += stats.furyGainRate; }
        if (TriggerIsActive(stats.furyLossTriggers, trigger)) { FURY -= stats.furyLossRate; }
    }
    // S T A M I N A
    [Server] public void AdjustStamina(PassiveTrigger trigger) {
        if (TriggerIsActive(stats.staminaGainTriggers, trigger)) { STAMINA += stats.staminaGainRate; }
        if (TriggerIsActive(stats.staminaLossTriggers, trigger)) { STAMINA -= stats.staminaLossRate; }
    }


    // G O L D
    [Server]
    public void AdjustGold(PassiveTrigger trigger)
    {
        if (TriggerIsActive(wealth.goldGainTriggers, trigger)) { GOLD += wealth.goldGainRate; }
        if (TriggerIsActive(wealth.goldLossTriggers, trigger)) { GOLD -= wealth.goldLossRate; }
    }
    // G E M S
    [Server]
    public void AdjustGems(PassiveTrigger trigger)
    {
        if (TriggerIsActive(wealth.gemsGainTriggers, trigger)) { GEMS += wealth.gemsGainRate; }
        if (TriggerIsActive(wealth.gemsLossTriggers, trigger)) { GEMS -= wealth.gemsLossRate; }
    }
    // E X P E R I E N C E
    [Server] public void AdjustExperience(PassiveTrigger trigger)
    {
        if (TriggerIsActive(wealth.expGainTriggers, trigger)) { EXP += wealth.expGainRate; }
        if (TriggerIsActive(wealth.expLossTriggers, trigger)) { EXP -= wealth.expLossRate; }
    }

    /*
    // S P E N D  F U R Y
    [Server] public bool SpendFury(int amount) {
        if (fury >= amount) { DepleteFury(amount); return true; } else { return false; }
    }
    // S P E N D  S T A M I N A
    [Server] public bool SpendStamina(int amount) {
        if (stamina >= amount) { DepleteStamina(amount); return true; } else { return false; }
    }
    // S P E N D  S H I E L D
    [Server] public bool SpendShield(int amount) {
        if (shield >= amount) { DepleteShield(amount); return true; } else { return false; }
    }
    */
}

    //Debug.Log("Stamina Gained " + target.stamina); //DEBUG 
    /*
    [ClientRpc] public void RpcGainStamina(int amount) { CmdGainStamina(amount); }
    [Command] public void CmdGainStamina(int amount) { GainStamina(amount); }
    */
    //GAIN TRIGGERED STAMINA
    //[ClientRpc] void RpcGainTriggeredStamina(PassiveTrigger trigger, int amount) { CmdGainTriggeredStamina(trigger, amount); }
    //[Command] void CmdGainTriggeredStamina(PassiveTrigger trigger, int amount) { GainTriggeredStamina(trigger, amount); }
    /// <summary> Uses Stamina if you have enough in your pool...otherwise returns false </summary>
    /// <returns>Is there enough in the Pool?</returns>
    // D E P L E T E  S T A M I N A
    /// <summary>
    /// Reduces the Pool no matter what...can drop it to zero
    /// </summary>
    //[ClientRpc] public void RpcDepleteStamina(int amount) { CmdDepleteStamina(amount); }
    //[Command] public void CmdDepleteStamina(int amount) { DepleteStamina(amount); }


    // - - - -
    // F U R Y

    //[ClientRpc] public void RpcGainTriggeredFury(PassiveTrigger trigger, int amount) { CmdGainTriggeredFury(trigger, amount); }
    //[Command] public void CmdGainTriggeredFury(PassiveTrigger trigger, int amount) { GainTriggeredFury(trigger, amount); }
        //Debug.Log("Fury Gained " + fury); //DEBUG
    /*
    [ClientRpc] public void RpcGainFury(int amount) { CmdGainFury(amount); }
    [Command] public void CmdGainFury(int amount) { GainFury(amount); }
    */
    /// <summary> Uses Fury if you have enough in your pool...otherwise returns false </summary>
    /// <returns>Is there enough in the Pool?</returns>
    // D E P L E T E  F U R Y
    /// <summary> Reduces the Pool no matter what...can drop it to zero </summary>
    ///
    /*
    [ClientRpc] public void RpcDepleteFury(int amount) { CmdDepleteFury(amount); }
    [Command] public void CmdDepleteFury(int amount) { DepleteFury(amount); }
    */


    // - - - - - -
    // S H I E L D


    /*
    [ClientRpc] public void RpcGainShield(int amount) { CmdGainShield(amount); }
    [Command] public void CmdGainShield(int amount) { GainShield(amount); }
    */
    //GAIN TRIGGERED SHIELD
        //Debug.Log("Shield Gained " + target.shield); //DEBUG
    /*
    [ClientRpc] void RpcGainTriggeredShield(PassiveTrigger trigger, int amount) { CmdGainTriggeredShield(trigger, amount); }
    [Command] void CmdGainTriggeredShield(PassiveTrigger trigger, int amount) { GainTriggeredShield(trigger, amount); }
    */
    /// <summary> Uses Shield if you have enough in your pool...otherwise returns false </summary>
    /// <returns>Is there enough in the Pool?</returns>
    // D E P L E T E  S H I E L D
    /// <summary>
    /// Reduces the Pool no matter what...can drop it to zero
    /// </summary>
    //[ClientRpc] public void RpcDepleteShield(int amount) { CmdDepleteShield(amount); }
    //[Command] public void CmdDepleteShield(int amount) { DepleteShield(amount); }
