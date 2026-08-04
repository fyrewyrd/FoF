// Dialogue System for Unity addon.
// Add to Dialogue Manager GameObject.
// Handles data sync and Lua functions.
using UnityEngine;

namespace PixelCrushers.DialogueSystem.uMMORPGSupport
{

    public class DialogueSystem_uMMORPG : MonoBehaviour
    {

        public bool updateServerOnConversationEnd = true;
        public bool updateServerOnQuestStateChange = true;

        [Tooltip("If unticked, Lua functions only operate during conversations where NPCs can apply their reward authorization rules.")]
        public bool allowLuaOutsideConversations = true;

        [Tooltip("If unticked, disable Lua functions that change data, such as AddPlayerGold.")]
        public bool allowLuaToChangeData = true;

        private void Awake()
        {
            //
            Lua.RegisterFunction("UpdateServer", this, SymbolExtensions.GetMethodInfo(() => UpdateServer()));
            Lua.RegisterFunction("SyncVarToParty", this, SymbolExtensions.GetMethodInfo(() => SyncVarToParty(string.Empty)));
            //
            Lua.RegisterFunction("GetPlayerName", this, SymbolExtensions.GetMethodInfo(() => GetPlayerName()));
            Lua.RegisterFunction("GetPlayerClass", this, SymbolExtensions.GetMethodInfo(() => GetPlayerClass()));
            Lua.RegisterFunction("GetPlayerLevel", this, SymbolExtensions.GetMethodInfo(() => GetPlayerLevel()));
            Lua.RegisterFunction("GetPlayerHealth", this, SymbolExtensions.GetMethodInfo(() => GetPlayerHealth()));
            Lua.RegisterFunction("GetPlayerMana", this, SymbolExtensions.GetMethodInfo(() => GetPlayerMana()));
            Lua.RegisterFunction("GetPlayerStr", this, SymbolExtensions.GetMethodInfo(() => GetPlayerStr()));
            Lua.RegisterFunction("GetPlayerInt", this, SymbolExtensions.GetMethodInfo(() => GetPlayerInt()));
            Lua.RegisterFunction("GetPlayerExp", this, SymbolExtensions.GetMethodInfo(() => GetPlayerExp()));
            Lua.RegisterFunction("GetPlayerSkillExp", this, SymbolExtensions.GetMethodInfo(() => GetPlayerSkillExp()));
            Lua.RegisterFunction("CanPlayerLearnSkill", this, SymbolExtensions.GetMethodInfo(() => CanPlayerLearnSkill(string.Empty)));
            Lua.RegisterFunction("GetSkillLevel", this, SymbolExtensions.GetMethodInfo(() => GetPlayerSkillLevel(string.Empty)));
            Lua.RegisterFunction("SetSkillLevel", this, SymbolExtensions.GetMethodInfo(() => SetPlayerSkillLevel(string.Empty, (double)0)));
            Lua.RegisterFunction("GetPlayerSkillLevel", this, SymbolExtensions.GetMethodInfo(() => GetPlayerSkillLevel(string.Empty)));
            Lua.RegisterFunction("SetPlayerSkillLevel", this, SymbolExtensions.GetMethodInfo(() => SetPlayerSkillLevel(string.Empty, (double)0)));
            Lua.RegisterFunction("GetPlayerGold", this, SymbolExtensions.GetMethodInfo(() => GetPlayerGold()));
            Lua.RegisterFunction("GetPlayerCoins", this, SymbolExtensions.GetMethodInfo(() => GetPlayerCoins()));
            Lua.RegisterFunction("GetPlayerItemAmount", this, SymbolExtensions.GetMethodInfo(() => GetPlayerItemAmount(string.Empty)));
            //
            Lua.RegisterFunction("AddPlayerHealth", this, SymbolExtensions.GetMethodInfo(() => AddPlayerHealth((double)0)));
            Lua.RegisterFunction("AddPlayerMana", this, SymbolExtensions.GetMethodInfo(() => AddPlayerMana((double)0)));
            Lua.RegisterFunction("AddPlayerStr", this, SymbolExtensions.GetMethodInfo(() => AddPlayerStr((double)0)));
            Lua.RegisterFunction("AddPlayerInt", this, SymbolExtensions.GetMethodInfo(() => AddPlayerInt((double)0)));
            Lua.RegisterFunction("AddPlayerExp", this, SymbolExtensions.GetMethodInfo(() => AddPlayerExp((double)0)));
            Lua.RegisterFunction("AddPlayerSkillExp", this, SymbolExtensions.GetMethodInfo(() => AddPlayerSkillExp((double)0)));
            Lua.RegisterFunction("AddPlayerGold", this, SymbolExtensions.GetMethodInfo(() => AddPlayerGold((double)0)));
            Lua.RegisterFunction("AddPlayerCoins", this, SymbolExtensions.GetMethodInfo(() => AddPlayerCoins((double)0)));
            Lua.RegisterFunction("AddPlayerItemAmount", this, SymbolExtensions.GetMethodInfo(() => AddPlayerItemAmount(string.Empty, (double)0)));
            //
            Lua.RegisterFunction("OpenTrading", this, SymbolExtensions.GetMethodInfo(() => OpenTrading()));
            Lua.RegisterFunction("OpenGuild", this, SymbolExtensions.GetMethodInfo(() => OpenGuild()));
            Lua.RegisterFunction("OpenRevive", this, SymbolExtensions.GetMethodInfo(() => OpenRevive()));
            Lua.RegisterFunction("CanTrade", this, SymbolExtensions.GetMethodInfo(() => CanTrade()));
            Lua.RegisterFunction("CanManageGuild", this, SymbolExtensions.GetMethodInfo(() => CanManageGuild()));
            Lua.RegisterFunction("CanRevive", this, SymbolExtensions.GetMethodInfo(() => CanRevive()));
        }

        public bool IsLuaDataChangePermitted()
        {
            return allowLuaToChangeData && (allowLuaOutsideConversations || DialogueManager.isConversationActive);
        }

        public bool IsLuaPermitted() { return IsLuaDataChangePermitted(); } // For backward compatibility.

        public void ToggleQuestLogWindow()
        {
            var questLogWindow = GetComponentInChildren<StandardUIQuestLogWindow>();
            if (questLogWindow)
            {
                questLogWindow.pauseWhileOpen = false;
                questLogWindow.Toggle();
            }
        }

        public void OnConversationEnd(Transform actor)
        {
            if (updateServerOnConversationEnd) UpdateServer();
        }

        public void OnQuestStateChange(string questName)
        {
            if (updateServerOnQuestStateChange) UpdateServer();
        }

        public static void UpdateServer()
        {
            var player = Player.localPlayer;
            if (player) player.UpdateDialogueSystemData();
        }

        public static void SyncVarToParty(string varName)
        {
            var player = Player.localPlayer;
            if (player) player.SyncVarToParty_DialogueSystem(varName);
        }

        public string GetPlayerName()
        {
            var player = Player.localPlayer;
            return player ? player.name : "Player";
        }

        public string GetPlayerClass()
        {
            var player = Player.localPlayer;
            return player ? player.className : "Person";
        }

        public double GetPlayerLevel()
        {
            var player = Player.localPlayer;
            return player ? player.level : 1;
        }

        public double GetPlayerHealth()
        {
            var player = Player.localPlayer;
            return player ? player.health : 1;
        }

        public double GetPlayerMana()
        {
            var player = Player.localPlayer;
            return player ? player.mana : 1;
        }

        public double GetPlayerStr()
        {
            var player = Player.localPlayer;
            return player ? player.strength : 1;
        }

        public double GetPlayerInt()
        {
            var player = Player.localPlayer;
            return player ? player.intelligence : 1;
        }

        public double GetPlayerExp()
        {
            var player = Player.localPlayer;
            return player ? player.experience : 0;
        }

        public double GetPlayerSkillExp()
        {
            var player = Player.localPlayer;
            return player ? player.skillExperience : 0;
        }

        public bool CanPlayerLearnSkill(string skillName)
        {
            var player = Player.localPlayer;
            if (!player) return false;
            for (int i = 0; i < player.skills.Count; i++)
            {
                var skill = player.skills[i];
                if (string.Equals(skill.name, skillName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public double GetPlayerSkillLevel(string skillName)
        {
            var player = Player.localPlayer;
            if (!player) return 0;
            for (int i = 0; i < player.skills.Count; i++)
            {
                var skill = player.skills[i];
                if (string.Equals(skill.name, skillName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return skill.level;
                }
            }
            return 0;
        }

        public void SetPlayerSkillLevel(string skillName, double level)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdSetSkillLevel_DialogueSystem(skillName, (int)level);
        }

        public double GetPlayerGold()
        {
            var player = Player.localPlayer;
            return player ? player.gold : 0;
        }

        public double GetPlayerCoins()
        {
            var player = Player.localPlayer;
            return player ? player.coins : 0;
        }

        public double GetPlayerItemAmount(string itemName)
        {
            var player = Player.localPlayer;
            if (!player) return 0;
            var item = GetItem(itemName);
            if (Equals(item, default(global::Item))) return 0;
            return player.InventoryCount(GetItem(itemName));
        }

        public void AddPlayerHealth(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddHealth_DialogueSystem((int)x);
        }

        public void AddPlayerMana(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddMana_DialogueSystem((int)x);
        }

        public void AddPlayerStr(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddStr_DialogueSystem((int)x);
        }

        public void AddPlayerInt(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddInt_DialogueSystem((int)x);
        }

        public void AddPlayerExp(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddExp_DialogueSystem((int)x);
        }

        public void AddPlayerSkillExp(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddSkillExp_DialogueSystem((int)x);
        }

        public void AddPlayerGold(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddGold_DialogueSystem((int)x);
        }

        public void AddPlayerCoins(double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddCoins_DialogueSystem((int)x);
        }

        public void AddPlayerItemAmount(string itemName, double x)
        {
            if (!IsLuaDataChangePermitted()) return;
            var player = Player.localPlayer;
            if (player) player.CmdAddItemAmount_DialogueSystem(itemName, (int)x);
        }

        public static global::Item GetItem(string itemName)
        {
            ScriptableItem template;
            if (ScriptableItem.dict.TryGetValue(itemName.GetStableHashCode(), out template)) return new global::Item(template);
            else return default(global::Item);
        }

        public void OpenTrading()
        {
            var uiNpcDialogue = FindObjectOfType<UINpcDialogue>();
            uiNpcDialogue.npcTradingPanel.SetActive(true);
            uiNpcDialogue.inventoryPanel.SetActive(true);
        }

        public void OpenGuild()
        {
            var uiNpcDialogue = FindObjectOfType<UINpcDialogue>();
            uiNpcDialogue.npcGuildPanel.SetActive(true);
        }

        public void OpenRevive()
        {
            var uiNpcDialogue = FindObjectOfType<UINpcDialogue>();
            uiNpcDialogue.npcRevivePanel.SetActive(true);
            uiNpcDialogue.inventoryPanel.SetActive(true);
        }

        public bool CanTrade()
        {
            var player = Player.localPlayer;
            if (player == null) return false;
            var npc = (Npc)player.target;
            return (npc != null) ? npc.saleItems.Length > 0 : false;
        }

        public bool CanManageGuild()
        {
            var player = Player.localPlayer;
            if (player == null) return false;
            var npc = (Npc)player.target;
            return (npc != null) ? npc.offersGuildManagement : false;
        }

        public bool CanRevive()
        {
            var player = Player.localPlayer;
            if (player == null) return false;
            var npc = (Npc)player.target;
            return (npc != null) ? npc.offersSummonableRevive: false;
        }

    }
}