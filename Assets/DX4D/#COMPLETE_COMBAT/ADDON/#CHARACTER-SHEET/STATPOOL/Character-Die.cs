//#define ummorpg
using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - -
    // H E L P E R  M E T H O D S
    // - - - - - - - - - - - - - -

    // I S D E A D
    public virtual bool Die()
    {
        //if (!enabled) { return false; } //TODO
        if (combat.invincible) { return false; }

        SHIELD = BARRIER = LIFE = BLOOD = SPIRIT = MANA = FURY = STAMINA = 0;

        return IsDead;
    }
}