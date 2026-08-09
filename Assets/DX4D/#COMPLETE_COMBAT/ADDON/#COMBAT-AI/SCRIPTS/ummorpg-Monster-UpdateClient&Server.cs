using Mirror;
using UnityEngine;

public partial class Monster : Entity
{

    [Server]
    protected override string UpdateServer()
    {
        if (state == "IDLE") return UpdateServer_IDLE();
        if (state == "MOVING") return UpdateServer_MOVING();
        if (state == "CASTING") return UpdateServer_CASTING();
        if (state == "STUNNED") return UpdateServer_STUNNED();
        if (state == "DEAD") return UpdateServer_DEAD();
        Debug.LogError("invalid state:" + state);
        return "IDLE";
    }

    // finite state machine - client ///////////////////////////////////////////
    [Client]
    protected override void UpdateClient()
    {
#if !RPG2D
        if (state == "CASTING")
        {
            // keep looking at the target for server & clients (only Y rotation)
            if (target) LookAtY(target.transform.position);
        }

        // show loot indicator on clients while it still has items
        if (lootIndicator != null)
        {
            // only set active once. we don't want to reset the particle
            // system all the time.
            bool hasLoot = HasLoot();
            if (hasLoot && !lootIndicator.isPlaying)
                lootIndicator.Play();
            else if (!hasLoot && lootIndicator.isPlaying)
                lootIndicator.Stop();
        }
#endif
        // addon system hooks
        Utils.InvokeMany(typeof(Monster), this, "UpdateClient_");
    }
}