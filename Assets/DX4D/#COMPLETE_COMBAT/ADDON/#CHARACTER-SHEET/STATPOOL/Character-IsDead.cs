//#define ummorpg
using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
// - - - - - - - - - - - - - -
// H E L P E R  M E T H O D S
// - - - - - - - - - - - - - -

    // I S D E A D
    public virtual bool IsDead
    {
        get
        {
            if (!enabled) { return true; } //If this is not enabled it is not alive
            if (combat.invincible) { return false; } //If you are invincible you can't be dead

            if (//TODO: use a loop here once stat pools become scriptables
                (stats.shieldLossKillsMe && SHIELDMAX > 0 && SHIELD < 1) ||
				(stats.barrierLossKillsMe && BARRIERMAX > 0 && BARRIER < 1) ||
                (stats.lifeLossKillsMe && LIFEMAX > 0 && LIFE < 1) ||
                (stats.manaLossKillsMe && MANAMAX > 0 && MANA < 1) ||
                (stats.bloodLossKillsMe && BLOODMAX > 0 && BLOOD < 1) ||
                (stats.spiritLossKillsMe && SPIRITMAX > 0 && SPIRIT < 1) ||
                (stats.staminaLossKillsMe && STAMINAMAX > 0 && STAMINA < 1) ||
                (stats.furyLossKillsMe && FURYMAX > 0 && FURY < 1))
                { return true; }//LIFE = 0; 

            return false;
        }
    }
}