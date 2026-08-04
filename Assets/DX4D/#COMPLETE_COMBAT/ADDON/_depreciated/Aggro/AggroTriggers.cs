/* //DEPRECIATED
using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class AggroTriggers : NetworkBehaviour
{
    [Header("TRIGGERED SPEECH")]
    [SerializeField, TextArea(1, 3)]
    public string onAggroSay;
    [SerializeField, TextArea(1, 3)] public string onSwitchTargetSay;

    [Header("TRIGGERED SKILL CAST")]
    [SerializeField]
    public bool canTriggerSkills = false;
    [SerializeField] public ScriptableSkill onAggroCastSkill;
    [SerializeField] public ScriptableSkill onSwitchTargetCastSkill;

    [Header("TRIGGERED AUDIO")]
    [Tooltip("This can only be turned off if On Aggro Sound is set to none/null")]
    [SerializeField]
    public bool canTriggerSound = false;
    [SerializeField] public AudioClip onAggroSound;
    [SerializeField] public AudioClip onSwitchTargetSound;
    [Header("TRIGGERED ANIMATIONS")]
    //TODO: I have not animated anything in Unity yet, so hopefully this is how it works
    [Tooltip("This can only be turned off if On Aggro Animation is set to none/null")]
    [SerializeField]
    public bool canTriggerAnimation = false;
    [SerializeField] public string onAggroClipName;
    [SerializeField] public string onSwitchTargetClipName;

    [Header("TRIGGERED SPAWNING")]
    public bool canSummonMinions = false;
    [SerializeField] public int maxMinions = 1;
    [SerializeField] public int spawnDistance = 3;

    [SerializeField] public bool minionsDieWithMe = true;
    [SerializeField] public bool noMinionCorpses = false;
    [SerializeField] public CreationList minionSpawnInfo;

    [HideInInspector] public List<Entity> spawnedMinions = new List<Entity>();

    private void OnValidate()
    {
        if (!canTriggerSkills &&
            (onAggroCastSkill != null || onSwitchTargetCastSkill != null))
        {
            canTriggerSkills = true;
        }
        if (!canTriggerAnimation &&
            (onAggroClipName != string.Empty || onSwitchTargetClipName != string.Empty))
        {
            canTriggerAnimation = true;
        }
        if (!canTriggerSound &&
            (onAggroSound != null || onSwitchTargetSound != null))
        {
            canTriggerSound = true;
        }
        if (!canSummonMinions &&
             ((minionSpawnInfo.onAggroSpawn != null && minionSpawnInfo.onAggroSpawn.Length > 0) ||
                (minionSpawnInfo.onSwitchTargetSpawn != null && minionSpawnInfo.onSwitchTargetSpawn.Length > 0)))
        {
            canSummonMinions = true;
        }
    }
}
*/
