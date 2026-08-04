using UnityEngine;
using UnityEngine.UI;

public partial class StaminaUI : MonoBehaviour
{
    public GameObject panel;
    public Slider staminaSlider;
    public Text staminaStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.STAMINAMAX < 1) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        staminaSlider.value = Local.player.character.STAMINAFILLED();
        staminaStatus.text = Local.player.character.STAMINA + " / " + Local.player.character.STAMINAMAX;
    }
}
