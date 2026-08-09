using Mirror;
using UnityEngine;
using UnityEngine.AI;

//public class SyncListCombatant : SyncList<GameObject> { }

[RequireComponent(typeof(StatPool))] [RequireComponent(typeof(CombatStats))]
[RequireComponent(typeof(Resistances))] [RequireComponent(typeof(Weaknesses))]
//[RequireComponent(typeof(Movement))]

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(NetworkProximityGridChecker))] [RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(AudioSource))]
[System.Serializable] public partial class Combatant : NetworkBehaviour
{ }