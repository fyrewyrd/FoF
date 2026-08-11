// Dialogue System for Unity addon.
// Adds check for Dialogue System Trigger.
using UnityEngine;
using Mirror; // Pre v1.130, use: UnityEngine.Networking;

public partial class Monster
{
    [Header("Dialogue System")]

    [Tooltip("If monster has Dialogue System Trigger set to OnUse, trigger it when dead.")]
    public bool useOnDeath = false;

    [Command]
    public void CmdWarp_DialogueSystem(Vector3 destination)
    {
        agent.Warp(destination);
    }
}
