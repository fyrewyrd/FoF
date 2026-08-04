using Mirror;
using UnityEngine;

//public partial class CharacterSheet : NetworkBehaviour
//{
//    [ServerCallback]
//    public virtual void OnOpponentHealed(CharacterSheet healedBy, int healAmount) { }
//}

public partial class AI : NetworkBehaviour
{
    [ServerCallback]
    public virtual void OnOpponentHealed(CharacterSheet healedBy, int healAmount)
    {
        //VALIDATE
        if (healedBy == null || !CanAttack(healedBy)) return;

        //FIX TARGET
        if (!my.target) { my.target = healedBy; }
        //TODO: Make the 0.8 a global config var
        else if (healedBy != my.target) // don't check distance if same target
        {
            float distanceToTarget = Vector3.Distance(transform.position, my.target.transform.position);
            float distanceToAttacker = Vector3.Distance(transform.position, healedBy.transform.position);
            if (distanceToAttacker < distanceToTarget * my.ai.optimalRange) my.target = healedBy;
        }

        //GAIN HATE
        GenerateAggro(my.target, (int)(healAmount * healerAggroScale));

        // addon system hooks
        Utils.InvokeMany(typeof(Monster), this, "OnOpponentHealed_", healedBy, healAmount);
    }
}
/*
//SPEAK
if (sayOnAggro != string.Empty) { this.RpcShowTextPopup(sayOnAggro); }

//SUMMON ALLIES
if (canSpawnAllies && onAggroSpawn != null && spawnedAllies.Count < maxSpawnedAllies)
{
    GameObject go = Instantiate(onAggroSpawn.gameObject, (transform.position + Tools.GetRandomVector(-1f, 1f)), Quaternion.identity);
    go.name = onAggroSpawn.name;

    Entity summoned = go.GetComponent<Entity>();
    if (summoned != null)
    {
        summoned.name = onAggroSpawn.name;
        spawnedAllies.Add(summoned); //Only Entities will count toward your maxAllies quota
        summoned.OnAggro(attacker); //Aggro our attacker with the newly summoned entity...if it's an Entity
    }

    NetworkServer.Spawn(go);
}

//PLAY SOUND
if (canPlaySound && onAggroSound != null)
{
    AudioSource.PlayClipAtPoint(onAggroSound, transform.position);
}

//PLAY ANIMATION
if (canPlayAnimation && aggroTargets.Count < 1)
{
    if (onAggroClipName != string.Empty) animator.Play(onAggroClipName);
}
*/
//FROM MONSTER.CS
// set death and respawn end times. we set both of them now to make sure
// that everything works fine even if a monster isn't updated for a
// while. so as soon as it's updated again, the death/respawn will
// happen immediately if current time > end time.
//deathTimeEnd = Time.time + deathTime;
//respawnTimeEnd = deathTimeEnd + respawnTime; // after death time ended

// generate gold
//gold = Random.Range(lootGoldMin, lootGoldMax);

// generate items (note: can't use Linq because of SyncList)
//foreach (ItemDropChance itemChance in dropChances)
//if (Random.value <= itemChance.probability)
//inventory.Add(new ItemSlot(new Item(itemChance.item)));

