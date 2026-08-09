using Mirror;
using UnityEngine;

public partial class Monster : Entity
{
    // aggro ///////////////////////////////////////////////////////////////////
    // this function is called by entities that attack us and by AggroArea
    [ServerCallback]
    public override void OnAggro(Entity entity)
    {
        // are we alive, and is the entity alive and of correct type?
        //if (entity != null && CanAttack(entity))
        if (entity != null && CanAttack(entity))//TODO && IsHostileTowards(entity))
        {
            // no target yet(==self), or closer than current target?
            // => has to be at least 20% closer to be worth it, otherwise we
            //    may end up nervously switching between two targets
            // => we do NOT use Utils.ClosestDistance, because then we often
            //    also end up nervously switching between two animated targets,
            //    since their collides moves with the animation.
            //    => we don't even need closestdistance here because they are in
            //       the aggro area anyway. transform.position is perfectly fine
            if (target == null)
            {
                target = entity;
            }
            else if (entity != target) // no need to check dist for same target
            {
                float oldDistance = Vector3.Distance(transform.position, target.transform.position);
                float newDistance = Vector3.Distance(transform.position, entity.transform.position);
                if (newDistance < oldDistance * 0.8) target = entity;
            }

            // addon system hooks
            Utils.InvokeMany(typeof(Monster), this, "OnAggro_", entity);
        }
    }
}