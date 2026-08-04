using Mirror;
using UnityEngine;

public partial class Monster : Entity
{
    // death ///////////////////////////////////////////////////////////////////
    [Server]
    protected override void OnDeath()
    {
        // take care of entity stuff
        base.OnDeath();

        // set death and respawn end times. we set both of them now to make sure
        // that everything works fine even if a monster isn't updated for a
        // while. so as soon as it's updated again, the death/respawn will
        // happen immediately if current time > end time.
        deathTimeEnd = NetworkTime.time + deathTime;
        respawnTimeEnd = deathTimeEnd + respawnTime; // after death time ended

        // generate gold
        gold = Random.Range(lootGoldMin, lootGoldMax);

        // generate items (note: can't use Linq because of SyncList)
        foreach (ItemDropChance itemChance in dropChances)
            if (Random.value <= itemChance.probability)
                inventory.Add(new ItemSlot(new Item(itemChance.item)));

        // addon system hooks
        Utils.InvokeMany(typeof(Monster), this, "OnDeath_");
    }
}