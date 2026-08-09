using UnityEngine;
using UnityEngine.UI;

public partial class LifeUI : MonoBehaviour
{
    public GameObject panel;
    public Slider lifeSlider;
    public Text lifeStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.LIFEMAX < 1) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }
        
        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        lifeSlider.value = Local.player.character.LIFEFILLED();
        lifeStatus.text = Local.player.character.LIFE + " / " + Local.player.character.LIFEMAX;
    }
}
