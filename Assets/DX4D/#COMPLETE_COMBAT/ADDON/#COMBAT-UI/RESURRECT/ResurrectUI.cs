using UnityEngine;
using UnityEngine.UI;

public partial class ResurrectUI : MonoBehaviour
{
    public GameObject revivePanel;

    public Button respawnButton;
    public Button resurrectButton;

    void Update()
    {
        if (Local.player && Local.player.character.IsDead)
        {
            revivePanel.SetActive(true);
            respawnButton.onClick.SetListener(() => { Local.player.CmdRespawn(); });
            if (Local.player.character.CanResurrect)
            {
                resurrectButton.interactable = true;
                resurrectButton.onClick.SetListener(() => { Local.player.CmdResurrect(); });
            }
            else { resurrectButton.interactable = false; }
        }
        else { revivePanel.SetActive(false); }
    }
}
