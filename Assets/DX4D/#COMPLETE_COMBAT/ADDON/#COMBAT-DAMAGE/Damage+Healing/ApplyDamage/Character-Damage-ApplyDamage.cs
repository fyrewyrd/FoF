//#define ummorpg //NOTE: Don't need this anymore
using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - -
    // A P P L Y  D A M A G E
    [Server]
    public void ApplyDamage(CharacterSheet defender, int damageAmount, MethodOfDamage method)
    {
        int damageToDeal = AbsorbDamage(defender, method, damageAmount); //SHIELD ABSORB

        if (damageToDeal < 1) return; //NO DAMAGE

        switch (method)
        {
            case MethodOfDamage.NoDamage:
                {
                    // N O  D A M A G E
                    break;
                }
            case MethodOfDamage.Physical:
                {
                    // H E A L T H
                    defender.LIFE -= damageToDeal;
#if ummorpg
                    defender.GetComponent<Entity>().health -= damageToDeal;
#endif
                    break;
                }
            case MethodOfDamage.Magic:
                {
                    // M A G I C
                    defender.LIFE -= damageToDeal;
#if ummorpg
                    defender.GetComponent<Entity>().health -= damageToDeal;
#endif
                    break;
                }
            case MethodOfDamage.Mana:
                {
                    // M A N A
                    defender.MANA -= damageToDeal;
#if ummorpg
                    defender.GetComponent<Entity>().mana -= damageToDeal;
#endif
                    break;
                }
            case MethodOfDamage.Blood:
                {
                    // B L O O D
                    defender.BLOOD -= damageToDeal;
                    break;
                }
            case MethodOfDamage.Spirit:
                {
                    // S P I R I T
                    defender.SPIRIT -= damageToDeal;
                    break;
                }
            case MethodOfDamage.Stamina:
                {
                    // S T A M I N A
                    defender.STAMINA -= damageToDeal;
                    break;
                }
            case MethodOfDamage.Fury:
                {
                    // F U R Y
                    defender.FURY -= damageToDeal;
                    break;
                }
            case MethodOfDamage.Poison:
                {
                    // P O I S O N
                    defender.LIFE -= damageToDeal;
#if ummorpg
                    defender.GetComponent<Entity>().health -= damageToDeal;
#endif
                    break;
                }
        }

        //TODO: Determine if we still need this
        //NOTE: Causes death from alternative damage methods by setting health to 0
#if ummorpg
        if (defender.bloodLossKills && defender.LIFE < 1) { defender.GetComponent<Entity>().health = 0; }// Debug.Log(defender.name + " died of life loss..."); }
        if (defender.bloodLossKills && defender.BLOOD < 1) { defender.GetComponent<Entity>().health = 0; }// Debug.Log(defender.name + " died of blood loss..."); }
        if (defender.spiritLossKills && defender.SPIRIT < 1) { defender.GetComponent<Entity>().health = 0; }// defender.RpcShowTextPopup(defender.name + " died of spirit loss..."); }
        if (defender.staminaLossKills && defender.STAMINA < 1) { defender.GetComponent<Entity>().health = 0; }// defender.RpcShowTextPopup(defender.name + " died of stamina loss..."); }
        if (defender.furyLossKills && defender.FURY < 1) { defender.GetComponent<Entity>().health = 0; }// defender.RpcShowTextPopup(defender.name + " died of fury loss..."); }
#endif

        // PVP KILL MESSAGES //NOTE: This announces when a player kills a player
        //if (this is Player && defender is Player) //TODO: Create a gobal setting to turn this off //TODO: Make this a chat message
        //{
        //    if (defender.health < 1) RpcShowOverheadPopup("killed " + defender.name + " with " + method.ToString() + " damage");
        //}
    }
}