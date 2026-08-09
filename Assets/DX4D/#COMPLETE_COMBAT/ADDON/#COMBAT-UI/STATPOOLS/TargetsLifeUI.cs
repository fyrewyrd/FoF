using UnityEngine;
using UnityEngine.UI;

public partial class TargetsLifeUI : MonoBehaviour
{
    public GameObject panel;
    public Slider slider;
    public Text status;
    CharacterSheet target;

    void Update()
    {
        if (!Local.player || !Local.player.character)// || !Local.player.character.target
        {
            panel.SetActive(false);
            return;
        }
        else
        {
            target = Local.player.character.nextTarget ?? Local.player.character.target;
        }

        if (!target || target.IsDead || target.LIFEMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }


        if (slider != null) slider.value = target.LIFEFILLED();
        if (status != null) status.text = target.LIFE + " / " + target.LIFEMAX;
    }
}
    /*
    public GameObject panel;
    public Slider lifeSlider;
    public Text lifeStatus;

    void Update()
    {
        if (!Local.player || !Local.player.target || Local.player.target.character.LIFEMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }

        CharacterSheet player = Local.player.character;
        panel.SetActive(player != null); // hide while not in the game world
        if (!player) return;

        CharacterSheet target = player.nextTarget ?? player.target;
        if (!target || target.IsDead)
        {
            panel.SetActive(false);
            return;
        }
        else if( target != null ) { panel.SetActive(true); }

        lifeSlider.value = target.LIFEFILLED();
        lifeStatus.text = target.LIFE + " / " + target.LIFEMAX;
    }*/
