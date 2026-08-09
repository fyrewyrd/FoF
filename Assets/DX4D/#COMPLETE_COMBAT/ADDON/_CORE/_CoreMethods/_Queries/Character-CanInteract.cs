//#define PRE184 //NOTE: Enable this to support legacy ummorpg versions

using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
// - - - - - - - - - - - - - -
// H E L P E R  M E T H O D S
// - - - - - - - - - - - - - -

    // C A N  I N T E R A C T
    public virtual bool CanInteract()
    {
            return (state == ActiveState.IDLE && target != null &&
               !target.IsDead &&
               (target is CharacterSheet));
    }

    // C A N  R E A C H  T A R G E T
    public virtual bool CanReachTarget(float reachDistance)
    {
#if PRE184 || RPG2D //LRGACY SUPPORT: Scroll up to the top and enable this define to support legacy ummorpg versions
        return (Utils.ClosestDistance(collider, target.collider) <= reachDistance);
#else
        return (Utils.ClosestDistance(gameObject.GetComponent<Entity>(), target.gameObject.GetComponent<Entity>()) <= reachDistance);
#endif
    }
}
