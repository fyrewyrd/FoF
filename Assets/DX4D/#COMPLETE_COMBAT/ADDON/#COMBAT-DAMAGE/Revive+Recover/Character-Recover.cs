//#define ummorpg //NOTE: This define is depreciated
using Mirror;

public abstract partial class Entity// : NetworkBehaviour
{
    [Server] public void Recover()
    {
        character.Recover();
    }
}
public partial class CharacterSheet : NetworkBehaviour
{
    [Server] public void Recover()
    {
        if (enabled)// && !IsDead)//health > 0)
        {
            //if (IsDead) { return; } //TODO: Death Triggers

            //MOVEMENT TRIGGERS
            if (state == ActiveState.IDLE) //IDLE
            {
                HandleTrigger(PassiveTrigger.NotMoving);
            }
            else if (state == ActiveState.MOVING) //MOVING
            {
                HandleTrigger(PassiveTrigger.Moving);
            }

            //ACTION TRIGGERS
            if (state == ActiveState.CASTING) //CASTING
            {
                HandleTrigger(PassiveTrigger.Casting);
            }

            //COMBAT TRIGGERS
            if (combat.IsInCombat) //COMBAT
            {
                HandleTrigger(PassiveTrigger.InCombat);
            }
            else //NON COMBAT
            {
                HandleTrigger(PassiveTrigger.NonCombat);
            }


//#if ummorpg //DEPRECIATED
            //HEALTH
//            if (healthRecovery) health += healthRecoveryRate;
            //MANA
//            if (manaRecovery) mana += manaRecoveryRate;
//#endif

            //LIFE
            //if (healthRecovery) health += healthRecoveryRate;
            //MANA
            //if (manaRecovery) mana += manaRecoveryRate;
            //BLOOD
            //if (bloodRegenerates) blood += bloodRegenRate;
            //SPIRIT
            //if (spiritRegenerates) spirit += spiritRegenRate;
            //SHIELD
            //if (shieldRegenerates) shield += shieldRegenRate;
            //if (shieldDepletes) shield -= shieldLossRate;
            //FURY
            //if (furyRegenerates) fury += furyRegenRate;
            //if (furyDepletes) fury -= furyLossRate;
            //STAMINA
            //if (staminaRegenerates) stamina += staminaRegenRate;
            //if (staminaDepletes) stamina -= staminaLossRate;
        }
    }
}
