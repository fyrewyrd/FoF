/* //DEPRECIATED
using UnityEngine;
using Mirror;

public partial class Entity : NetworkBehaviourNonAlloc
{
    // - - - - - - - - - - - -
    // A P P L Y  S T A T U S
    [Server]
    public void RemoveStatus(Entity defender, StatusEffect status)
    {
        if (status != StatusEffect.None)
        {
            //TODO: Strip out None
            status &= (~StatusEffect.None);

            defender.activeStatusEffects &= (~status);
            Debug.Log("STATUS - " + status + " removed from " + "<" + defender.name + "> at " + NetworkTime.time.ToString()); //DEBUG
        }
    }
}
*/
