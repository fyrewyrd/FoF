/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    private void Update_HandleRegeneration()
    {
        //SHIELD
        if (shieldRegenerates) CmdGainShield(shieldRegenRate);
        if (shieldDepletes) CmdDepleteShield(shieldLossRate);
        //FURY
        if (furyRegenerates) CmdGainFury(furyRegenRate);
        if (furyDepletes) CmdDepleteFury(furyLossRate);
        //STAMINA
        if (staminaRegenerates) CmdGainStamina(staminaRegenRate);
        if (staminaDepletes) CmdDepleteStamina(staminaLossRate);
        //BLOOD
        if (bloodRegenerates) blood += bloodRegenRate;
        //SPIRIT
        if (spiritRegenerates) spirit += spiritRegenRate;
    }

    [Server] void HandleRegeneration()
    {
        //SHIELD
        if (shieldRegenerates) shield += shieldRegenRate;
        if (shieldDepletes) shield -= shieldLossRate;
        //FURY
        if (furyRegenerates) fury += furyRegenRate;
        if (furyDepletes) fury -= furyLossRate;
        //STAMINA
        if (staminaRegenerates) stamina += staminaRegenRate;
        if (staminaDepletes) stamina -= staminaLossRate;

        //SHIELD
        //if (shieldRegenerates) RpcGainShield(shieldRegenRate);
        //if (shieldDepletes) RpcDepleteShield(shieldLossRate);
        //FURY
        //if (furyRegenerates) RpcGainFury(furyRegenRate);
        //if (furyDepletes) RpcDepleteFury(furyLossRate);
        //STAMINA
        //if (staminaRegenerates) RpcGainStamina(staminaRegenRate);
        //if (staminaDepletes) RpcDepleteStamina(staminaLossRate);
        //BLOOD
        if (bloodRegenerates) blood += bloodRegenRate;
        //SPIRIT
        if (spiritRegenerates) spirit += spiritRegenRate;
    }
}
*/
