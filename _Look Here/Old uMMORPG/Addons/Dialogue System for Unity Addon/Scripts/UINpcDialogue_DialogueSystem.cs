// Dialogue System for Unity addon.
// Adds Dialogue System Use button.
// Add Dialogue System Trigger to NPC & set to OnUse.
// Add this to the NpcDialogue GameObject.
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(UINpcDialogue))]
public class UINpcDialogue_DialogueSystem : MonoBehaviour
{
    [Tooltip("Button to send OnUse to NPC's Dialogue System Trigger.")]
    public Button dialogueSystemUseButton;

    private UINpcDialogue m_uiNpcDialogue;
    private bool m_lastPanelState;

    private void Start()
    {
        m_uiNpcDialogue = GetComponent<UINpcDialogue>();
        if (m_uiNpcDialogue != null)
        {
            m_lastPanelState = m_uiNpcDialogue.panel.activeSelf;
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        Player player = Player.localPlayer;
        if (!player) return;

        if (m_uiNpcDialogue.panel.activeSelf != m_lastPanelState)
        {
            m_lastPanelState = m_uiNpcDialogue.panel.activeSelf;
            if (player.target != null && player.target is Npc &&
#if USE_UMMORPG2D
                Utils.ClosestDistance(player.collider, player.target.collider) <= player.interactionRange)
#else
                Utils.ClosestDistance(player, player.target) <= player.interactionRange)
#endif
            {
                Npc npc = (Npc)player.target;
                if (npc.bypassNpcDialoguePanel) return;

                // talk
                DialogueSystemTrigger usableTrigger = null;
                foreach (var trigger in npc.GetComponents<DialogueSystemTrigger>())
                {
                    if (trigger.trigger == DialogueSystemTriggerEvent.OnUse) usableTrigger = trigger;
                }
                dialogueSystemUseButton.gameObject.SetActive(usableTrigger != null);
                dialogueSystemUseButton.onClick.SetListener(() =>
                {
                    usableTrigger.OnUse(player.transform); // use trigger
                    m_uiNpcDialogue.panel.SetActive(false);
                });
            }
        }
    }

    private void LateUpdate()
    {
        if (m_uiNpcDialogue.panel.activeSelf)
        {
            Player player = Player.localPlayer;
            if (!player) return;
            Npc npc = (Npc)player.target;
            if (npc.bypassNpcDialoguePanel)
            {
                m_uiNpcDialogue.panel.SetActive(false);
                foreach (var trigger in npc.GetComponents<DialogueSystemTrigger>())
                {
                    if (trigger.trigger == DialogueSystemTriggerEvent.OnUse) trigger.OnUse(player.transform);
                }
            }
        }
    }
}
