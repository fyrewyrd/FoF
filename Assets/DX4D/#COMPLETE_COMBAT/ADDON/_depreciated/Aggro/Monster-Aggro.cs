/* //DEPRECIATED
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [ServerCallback]
    public virtual void OnDamaged(Entity attacker, int damageAmount) { }
}

public partial class Monster : Entity
{
    //AGGRO SYSTEM
    /// <summary>
    /// The amount of excess hate required to break the enemy's focus on a target.
    /// Increase this to prevent mobs from getting confused and "rubberbanding" between targets.
    /// </summary>
    [Header("HATE/ENMITY/AGGRO")]
    [Tooltip("The amount of hate required to take aggro from the top damager." +
        "\n1.0 here would mean you would have to do twice as much damage to take aggro..." +
        "something closer to 0.2 is more ideal")]
    [SerializeField] public float aggroFocus = 0.1f;

    public int hateRequiredToBreakAggroFocus
    {
        get { return Mathf.RoundToInt(priorityTargetsHateLevel * aggroFocus); }
    }

    //TODO: A Dictionary might be better here, worth a try if performance is in question
    [SerializeField, HideInInspector] List<int> aggroLevels = new List<int>();
    [SerializeField, HideInInspector] List<Entity> aggroTargets = new List<Entity>();

    [SerializeField, HideInInspector] public int priorityTargetId = 0;
    [SerializeField, HideInInspector] public int priorityTargetsHateLevel = 0;

    [Server] public void GenerateAggro(Entity attacker, int amount)
    {
        if (aggroTargets.Contains(attacker))
        {
            for (int i = 0; i < aggroTargets.Count; i++)
            {
                //COMPARE HASHES TO MATCH PLAYER TO AN AGGRO TARGET
                if (aggroTargets[i].GetHashCode() == attacker.GetHashCode())
                {
                    Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} ADDED " + amount.ToString() + " HATE FOR {" + aggroTargets[i].name.ToUpper() + "}");//DEBUG
                    aggroLevels[i] += amount; //ADD AGGRO/HATE/ENMITY
                    if (attacker.GetHashCode() != aggroTargets[priorityTargetId].GetHashCode()
                        && aggroLevels[i] > (priorityTargetsHateLevel + hateRequiredToBreakAggroFocus))
                    {
                        OnAggro(attacker);
                        target = aggroTargets[i]; //NEW TARGET

                        //LAUNCH ONSWITCHTARGET TRIGGERS
                        LaunchTriggeredActions(attacker, sayOnSwitchTarget, onSwitchTargetSound, onSwitchTargetClipName, onSwitchTargetSpawn, onSwitchTargetCastSkill);

                        Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} CHANGED TARGET TO {" + attacker.name.ToUpper() + "}");//DEBUG
                    }
                }

                //TODO: Get the lowest aggro amount then subtract that from each target next frame.
            }
        }
        else
        {
            if (target != null)
            {
                //LAUNCH ONAGGRO TRIGGERS
                LaunchTriggeredActions(attacker, sayOnAggro, onAggroSound, onAggroClipName, onAggroSpawn, onAggroCastSkill);
            }
            Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} ADDED {" + attacker.name.ToUpper() + "} TO THE KILL LIST!!!");//DEBUG
            aggroLevels.Add(amount);
            aggroTargets.Add(attacker);
        }
    }

    [ServerCallback]
    public override void OnDamaged(Entity attacker, int damageAmount)
    {
        //VALIDATE
        if (attacker == null || !CanAttack(attacker)) return;

        //FIX TARGET
        if (!target) { target = attacker; }
        //TODO: Make the 0.8 a global config var
        else if (attacker != target) // don't check distance if same target
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            float distanceToAttacker = Vector3.Distance(transform.position, attacker.transform.position);
            if (distanceToAttacker < distanceToTarget * 0.8) target = attacker;
        }

        //GAIN HATE
        GenerateAggro(target, damageAmount);

        // addon system hooks
        Utils.InvokeMany(typeof(Monster), this, "OnDamaged_", attacker, damageAmount);
    }
    
    [Server]
    protected void OnDeath_ResetAggro()
    {
        Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} AGGRO RESET ON DEATH");//DEBUG
        aggroLevels.Clear();
        aggroTargets.Clear();

        //base.OnDeath();
        // addon system hooks
        //Utils.InvokeMany(typeof(Monster), this, "OnDeath_ResetAggro_");
    }
}
*/

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

