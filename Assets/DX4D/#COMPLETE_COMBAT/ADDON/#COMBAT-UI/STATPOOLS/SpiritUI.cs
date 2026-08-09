using UnityEngine;
using UnityEngine.UI;

public partial class SpiritUI : MonoBehaviour
{
    public GameObject panel;
    public Slider spiritSlider;
    public Text spiritStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.SPIRITMAX < 1) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        spiritSlider.value = Local.player.character.SPIRITFILLED();
        spiritStatus.text = Local.player.character.SPIRIT + " / " + Local.player.character.SPIRITMAX;
    }
}
