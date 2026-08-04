using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    [Server]
    public void Revive(float recoveryPercentage = 1.0f)
    {
        character.Revive(recoveryPercentage);
    }
}

public partial class CharacterSheet : NetworkBehaviour
{
    [Server] public void Revive(float recoveryPercentage = 1.0f)
    {
#if ummorpg
        //HEALTH
        GetComponent<Entity>().health = Mathf.RoundToInt(LIFEMAX * recoveryPercentage);
        //MANA
        GetComponent<Entity>().mana = Mathf.RoundToInt(MANAMAX * recoveryPercentage);
#endif
        //LIFE
        LIFE = Mathf.RoundToInt(LIFEMAX * recoveryPercentage);
        //MANA
        MANA = Mathf.RoundToInt(MANAMAX * recoveryPercentage);
        //BLOOD
        BLOOD = Mathf.RoundToInt(BLOODMAX * recoveryPercentage);
        //SPIRIT
        SPIRIT = Mathf.RoundToInt(SPIRITMAX * recoveryPercentage);
        //SHIELD
        SHIELD = Mathf.RoundToInt(SHIELDMAX * recoveryPercentage);
		//BARRIER
        BARRIER = Mathf.RoundToInt(BARRIERMAX * recoveryPercentage);
        //FURY
        //fury = Mathf.RoundToInt(furyMax * recoveryPercentage);
        //STAMINA
        STAMINA = Mathf.RoundToInt(STAMINAMAX * recoveryPercentage);

        if (CanResurrect) _resurrectionLevel = 0; //RESET RESURRECTION LEVEL
        if (state == ActiveState.DEAD) state = ActiveState.IDLE; //RESET DEAD STATE //TODO: There should be a REVIVING state.
    }
}
