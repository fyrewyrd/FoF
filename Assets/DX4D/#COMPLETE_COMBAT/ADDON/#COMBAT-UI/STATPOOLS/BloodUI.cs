using UnityEngine;
using UnityEngine.UI;

public partial class BloodUI : MonoBehaviour
{
    public GameObject panel;
    public Slider bloodSlider;
    public Text bloodStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.BLOODMAX < 1) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        bloodSlider.value = Local.player.character.BLOODFILLED();
        bloodStatus.text = Local.player.character.BLOOD + " / " + Local.player.character.BLOODMAX;
    }
}
