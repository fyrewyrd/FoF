// Dialogue System for Unity addon.
// Adds SyncVar for Dialogue System data.
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.uMMORPGSupport;
using Mirror; // Pre v1.130, use: UnityEngine.Networking;

public delegate bool DSValidateInt(Player player, int x);
public delegate bool DSValidateItem(Player player, string itemName, int amount);

public partial class Player : Entity
{
    [Header("Dialogue System")]
    [Tooltip("Holds saved state of Dialogue System environment.")]
    [SyncVar] public string dialogueSystemData;

    [Tooltip("Use player prefab's Portrait Icon as player portrait in conversations. Portrait Icon MUST be placed in a Resources folder!")]
    public bool usePortraitIconForConversations = false;

    #region Initialization

    void OnStartLocalPlayer_DialogueSystem()
    {
        DialogueManager.ResetDatabase(DatabaseResetOptions.KeepAllLoaded);
        PersistentDataManager.ApplySaveData(dialogueSystemData);
        var player = Player.localPlayer;
        if (usePortraitIconForConversations && player && player.portraitIcon != null)
        {
            DialogueLua.SetActorField("Player", DialogueSystemFields.CurrentPortrait, player.portraitIcon.name);
        }
        StartCoroutine(UpdateQuestStateListenersAndClassNameAfterOneFrame());
    }

    IEnumerator UpdateQuestStateListenersAndClassNameAfterOneFrame()
    {
        yield return null;
        foreach (var questStateListener in FindObjectsOfType<QuestStateListener>())
        {
            questStateListener.UpdateIndicator();
        }
        CmdRequestMyClassName_DialogueSystem(name);
    }

    #endregion

    #region Update Dialogue System Data On Server

    private Coroutine m_updateDialogueSystemDataCoroutine = null;

    // called by DialogueSystem_uMMORPG UpdateServer Lua function:
    public void UpdateDialogueSystemData()
    {
        // coroutine ensures we only update data once during any given frame:
        if (m_updateDialogueSystemDataCoroutine == null)
        {
            m_updateDialogueSystemDataCoroutine = StartCoroutine(UpdateDialogueSystemDataCoroutine());
        }
    }

    private IEnumerator UpdateDialogueSystemDataCoroutine()
    {
        yield return new WaitForEndOfFrame();
        m_updateDialogueSystemDataCoroutine = null;
        UpdateDialogueSystemDataNow();
    }

    protected void UpdateDialogueSystemDataNow()
    {
        dialogueSystemData = PersistentDataManager.GetSaveData();
        if (!isServer)
        {
            CmdUpdateDialogueSystemDataOnServer(dialogueSystemData);
        }
    }

    [Command]
    public void CmdUpdateDialogueSystemDataOnServer(string newData)
    {
        // client sent new Dialogue System data. update copy on server:
        dialogueSystemData = newData;
    }

    #endregion

    #region Sync To Party

    // called by DialogueSystem_uMMORPG SyncVarToParty lua function:
    public void SyncVarToParty_DialogueSystem(string varName)
    {
        if (!InParty() || string.IsNullOrEmpty(varName)) return;
        var varValue = DialogueLua.GetVariable(varName);
        if (varValue.isBool)
        {
            CmdSyncBoolVarToParty_DialogueSystem(varName, varValue.asBool);
        }
        else if (varValue.isNumber)
        {
            CmdSyncFloatVarToParty_DialogueSystem(varName, varValue.asFloat);
        }
        else if (varValue.isString)
        {
            CmdSyncStringVarToParty_DialogueSystem(varName, varValue.asString);
        }
    }

    [Command]
    public void CmdSyncBoolVarToParty_DialogueSystem(string varName, bool value)
    {
        foreach (var member in GetPartyMembersInProximity())
        {
            if (member != this)
            {
                TargetSetBoolVar_DialogueSystem(member.connectionToClient, varName, value);
            }
        }
    }

    [Command]
    public void CmdSyncFloatVarToParty_DialogueSystem(string varName, float value)
    {
        foreach (var member in GetPartyMembersInProximity())
        {
            if (member != this)
            {
                TargetSetFloatVar_DialogueSystem(member.connectionToClient, varName, value);
            }
        }
    }

    [Command]
    public void CmdSyncStringVarToParty_DialogueSystem(string varName, string value)
    {
        foreach (var member in GetPartyMembersInProximity())
        {
            if (member != this)
            {
                TargetSetStringVar_DialogueSystem(member.connectionToClient, varName, value);
            }
        }
    }

    [TargetRpc]
    public void TargetSetBoolVar_DialogueSystem(NetworkConnection target, string varName, bool value)
    {
        DialogueLua.SetVariable(varName, value);
        DialogueSystem_uMMORPG.UpdateServer();
    }

    [TargetRpc]
    public void TargetSetFloatVar_DialogueSystem(NetworkConnection target, string varName, float value)
    {
        DialogueLua.SetVariable(varName, value);
        DialogueSystem_uMMORPG.UpdateServer();
    }

    [TargetRpc]
    public void TargetSetStringVar_DialogueSystem(NetworkConnection target, string varName, string value)
    {
        DialogueLua.SetVariable(varName, value);
        DialogueSystem_uMMORPG.UpdateServer();
    }

    #endregion

    #region Class Name

    [Command]
    public void CmdRequestMyClassName_DialogueSystem(string playerName)
    {
        if (onlinePlayers.ContainsKey(playerName))
        {
            var player = onlinePlayers[playerName];
            TargetSetClassName_DialogueSystem(player.connectionToClient, player.className);
        }
    }

    [TargetRpc]
    public void TargetSetClassName_DialogueSystem(NetworkConnection target, string className)
    {
        this.className = className;
    }

    #endregion

    #region OnUse When Player Kills Monster

    [Server]
    public void DealDamageAt_DialogueSystem(Entity entity, int amount)
    {
        // when we kill a monster on the server, we want the player's client
        // to trigger the monster's DialogueSystemTriggers' OnUse:
        if (entity.health == 0 && entity is Monster)
        {
            var monster = entity as Monster;
            if (monster.useOnDeath)
            {
                if (InParty())
                {
                    foreach (var member in GetPartyMembersInProximity())
                    {
                        TargetUseTrigger_DialogueSystem(member.connectionToClient, monster.gameObject);
                    }
                }
                else
                {
                    TargetUseTrigger_DialogueSystem(connectionToClient, monster.gameObject);
                }
            }
        }
    }

    [TargetRpc]
    public void TargetUseTrigger_DialogueSystem(NetworkConnection target, GameObject triggerObject)
    {
        // use triggerObject's triggers:
        if (triggerObject == null) return;
        var usedTrigger = false;
        foreach (var trigger in triggerObject.GetComponents<DialogueSystemTrigger>())
        {
            if (trigger.enabled && trigger.trigger == DialogueSystemTriggerEvent.OnUse)
            {
                trigger.OnUse(transform);
                usedTrigger = true;
            }
        }
        if (usedTrigger) DialogueSystem_uMMORPG.UpdateServer();
    }

    #endregion

    #region Server Commands To Modify Player

    // these commands are used by the DialogueSystem_uMMORPG Lua functions

    public DSValidateInt dsValidateAddPlayerExp = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerHealth = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerMana = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerStr = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerInt = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerGold = delegate { return true; };
    public DSValidateInt dsValidateAddPlayerCoins = delegate { return true; };
    public DSValidateItem dsValidateAddPlayerItem = delegate { return true; };

    [Command]
    public void CmdSetSkillLevel_DialogueSystem(string skillName, int level)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            var skill = skills[i];
            if (string.Equals(skill.name, skillName, System.StringComparison.OrdinalIgnoreCase))
            {
                if (skill.level < skill.maxLevel)
                {
                    skill.level = level;
                    skills[i] = skill;
                }
            }
        }
    }

    [Command]
    public void CmdAddExp_DialogueSystem(int x)
    {
        experience += x;
    }

    [Command]
    public void CmdAddSkillExp_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerExp(this, x)) return;
        skillExperience += x;
    }

    [Command]
    public void CmdAddHealth_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerHealth(this, x)) return;
        health += x;
    }

    [Command]
    public void CmdAddMana_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerMana(this, x)) return;
        mana += x;
    }

    [Command]
    public void CmdAddStr_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerStr(this, x)) return;
        strength += x;
    }

    [Command]
    public void CmdAddInt_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerInt(this, x)) return;
        intelligence += x;
    }

    [Command]
    public void CmdAddGold_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerGold(this, x)) return;
        gold += x;
    }

    [Command]
    public void CmdAddCoins_DialogueSystem(int x)
    {
        if (!dsValidateAddPlayerCoins(this, x)) return;
        coins += x;
    }

    [Command]
    public void CmdAddItemAmount_DialogueSystem(string itemName, int amount)
    {
        if (!dsValidateAddPlayerItem(this, itemName, amount)) return;
        var item = PixelCrushers.DialogueSystem.uMMORPGSupport.DialogueSystem_uMMORPG.GetItem(itemName);
        if (Equals(item, default(global::Item))) return;
        if (amount > 0)
        {
            InventoryAdd(item, amount);
        }
        else if (amount < 0)
        {
            InventoryRemove(item, -amount);
        }
    }

    [Command]
    public void CmdWarp_DialogueSystem(Vector3 destination)
    {
        agent.Warp(destination);
    }

    #endregion
}
