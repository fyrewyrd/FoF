using UnityEngine;
using UnityEngine.UI;

public partial class FuryUI : MonoBehaviour
{
    public GameObject panel;
    public Slider furySlider;
    public Text furyStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.FURYMAX < 1) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        furySlider.value = Local.player.character.FURYFILLED();
        furyStatus.text = Local.player.character.FURY + " / " + Local.player.character.FURYMAX;
    }
}
