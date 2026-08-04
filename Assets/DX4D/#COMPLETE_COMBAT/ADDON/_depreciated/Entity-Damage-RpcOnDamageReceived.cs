/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [ClientRpc]
    void RpcOnDamageReceived(int amount, DefensiveState defensiveState, MethodOfDamage method, Element element)
    {
        ShowDamagePopup(amount, defensiveState, method, element);

        // addon system hooks
        //Utils.InvokeMany(typeof(Entity), this, "OnDamageReceived_", amount, method, element);
    }
}
*/
