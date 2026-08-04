using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrushers.DialogueSystem.uMMORPGSupport
{
    /// <summary>
    /// This component syncs Dialogue System variables with items
    /// after the loot window closes. You can use it for gather quests.
    /// </summary>
    public class DialogueSystemLootQuestInfo : MonoBehaviour
    {
        [Serializable]
        public class ItemVariable
        {
            [VariablePopup] public string variable;
            public ScriptableItem item;
            public int maxValue = 1;
        }

        public List<ItemVariable> itemVariables = new List<ItemVariable>();

        public UnityEvent onVariablesUpdated = new UnityEvent();

        private void Start()
        {
            var lootPanel = FindObjectOfType<UILoot>().transform.GetChild(0).gameObject;
            if (lootPanel == null)
            {
                Debug.LogError("Dialogue System: Can't find LootPanel.", this);
            }
            else
            {
                var trigger = lootPanel.AddComponent<DialogueSystemTrigger>();
                trigger.trigger = DialogueSystemTriggerEvent.OnDisable;
                trigger.onExecute.AddListener((GameObject go) => { OnLootPanelClosed(); });
            }
        }

        private void OnLootPanelClosed()
        {
            var player = Player.localPlayer;
            if (player == null) return;
            foreach (var element in itemVariables)
            {
                if (string.IsNullOrEmpty(element.variable) || element.item == null) continue;
                var item = DialogueSystem_uMMORPG.GetItem(element.item.name);
                DialogueLua.SetVariable(element.variable, Mathf.Min(element.maxValue, player.InventoryCount(item)));
            }
            DialogueManager.SendUpdateTracker();
            onVariablesUpdated.Invoke();
        }
    }
}
