using Mirror;
using System;
using UnityEngine;
using UnityEngine.AI;

public class SyncListCharacter : SyncList<GameObject> { }

[RequireComponent(typeof(AI))]
[RequireComponent(typeof(Targeting))]
[RequireComponent(typeof(Movement))]

[RequireComponent(typeof(Skills))]

[RequireComponent(typeof(StatPool))] [RequireComponent(typeof(CombatStats))]
[RequireComponent(typeof(Resistances))] [RequireComponent(typeof(Weaknesses))]

[RequireComponent(typeof(Wealth))] [RequireComponent(typeof(Backpack))]
[RequireComponent(typeof(Gear))] [RequireComponent(typeof(Storage))]
[RequireComponent(typeof(Followers))]


[RequireComponent(typeof(EventTriggerManager))]

#if !RPG2D
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
#endif
//[RequireComponent(typeof(NetworkProximityGridChecker))]
//[RequireComponent(typeof(AudioSource))]

[DisallowMultipleComponent]
[Serializable] public partial class CharacterSheet : NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField] [TextArea(1, 4)]
    string componentDescription =
            "The Character Sheet contains links to all of the components that make up a character." +
        "\nNOTE: Components link automatically..." +
        "\nNO NEED TO LINK THESE MANUALLY";
#endif
    #endregion

    [Header("REQUIRED ENTITY")]
    public Entity entity;

    [Header("EFFECT RESULTS")]
    public bool bloodLossKills = true;
    public bool spiritLossKills = true;
    public bool staminaLossKills = true;
    public bool furyLossKills = true;
}