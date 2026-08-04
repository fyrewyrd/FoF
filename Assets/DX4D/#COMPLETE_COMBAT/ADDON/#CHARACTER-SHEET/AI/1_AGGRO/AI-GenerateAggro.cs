#define AUTO_ADD_COMPONENTS //NOTE: It is usually best to add components manually rather than at runtime like this does
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public partial class AI// : NetworkBehaviour
{
    //AGGRO SYSTEM
    public int hateRequiredToBreakAggroFocus
    {
        get { return Mathf.RoundToInt(priorityTargetsHateLevel * aggroFocus); }
    }

    //TODO: A Dictionary might be better here, worth a try if performance is in question
    [SerializeField, HideInInspector] List<int> aggroLevels = new List<int>();
    [SerializeField, HideInInspector] internal List<CharacterSheet> aggroTargets = new List<CharacterSheet>();

    [SerializeField, HideInInspector] public int priorityTargetId = 0;
    [SerializeField, HideInInspector] public int priorityTargetsHateLevel = 0;

    [Server]
    public void GenerateAggro(CharacterSheet attacker, int amount)
    {
        if (character.player) return; //NO AGGRO FOR PLAYERS

        if (attacker != null)
        {
            if (amount < 1) { amount = 1; }

            //LAUNCH AGGRO EVENTS
            EventTriggerManager eventLauncher;
            eventLauncher = GetComponent<EventTriggerManager>();
#if AUTO_ADD_COMPONENTS
            //manager
            if (!eventLauncher) eventLauncher = gameObject.AddComponent<EventTriggerManager>();
            //data
            eventLauncher.data = gameObject.GetComponent<EventTriggerData>();
#endif
            if (eventLauncher == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("DX4D AGGRO SYSTEM: You must add an Event Launcher component to " + name + " in order for the monster to generate aggro.");
#endif
                return;
            }

            if (aggroTargets != null && aggroTargets.Count > 0 && aggroTargets.Contains(attacker))
            {
                for (int i = 0; i < aggroTargets.Count; i++)
                {
                    //COMPARE HASHES TO MATCH PLAYER TO AN AGGRO TARGET
                    if (aggroTargets[i] != null && aggroTargets[i].GetHashCode() == attacker.GetHashCode())
                    {
#if UNITY_EDITOR
                        Debug.Log("<b>| <color=orange>A G G R O  L O G</color> |</b>" + "\n{" + name.ToUpper() + "} ADDED " + amount.ToString() + " HATE FOR {" + aggroTargets[i].name.ToUpper() + "}");//DEBUG
#endif
                        aggroLevels[i] += amount; //ADD AGGRO/HATE/ENMITY

                        //AGGRO RETARGETING
                        if (attacker.GetHashCode() != aggroTargets[priorityTargetId].GetHashCode()
                            && aggroLevels[i] > (priorityTargetsHateLevel + hateRequiredToBreakAggroFocus))
                        {
                            //OnAggro(attacker); //TODO: Do we need this?
                            GetComponent<CharacterSheet>().target = aggroTargets[i]; //NEW TARGET

                            //LAUNCH SWITCHTARGET EVENTS
                            eventLauncher.Trigger(character, attacker, EventTriggerType.OnSwitchTarget);

#if UNITY_EDITOR
                            Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} CHANGED TARGET TO {" + attacker.name.ToUpper() + "}");//DEBUG
#endif
                        }
                    }

                    //TODO: Get the lowest aggro amount then subtract that from each target next frame.
                }
            }
            else
            {
                //LAUNCH AGGRO EVENTS
                eventLauncher.Trigger(character, attacker, EventTriggerType.OnAggro);

#if UNITY_EDITOR
                Debug.Log("<b>| <color=orange>A G G R O  L O G</color> |</b>" + "\n{" + name.ToUpper() + "} ADDED {" + attacker.name.ToUpper() + "} TO THE KILL LIST!!!");//DEBUG
#endif
                aggroLevels.Add(amount);
                aggroTargets.Add(attacker);
            }
        }
    }

    [Server] protected void OnDeath_ResetAggro()
    {
#if UNITY_EDITOR
        Debug.Log("<b>| A G G R O  L O G |</b>" + "\n{" + name.ToUpper() + "} AGGRO RESET ON DEATH");//DEBUG
#endif
        aggroLevels.Clear();
        aggroTargets.Clear();

        //base.OnDeath();
        // addon system hooks
        //Utils.InvokeMany(typeof(Monster), this, "OnDeath_ResetAggro_");
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

