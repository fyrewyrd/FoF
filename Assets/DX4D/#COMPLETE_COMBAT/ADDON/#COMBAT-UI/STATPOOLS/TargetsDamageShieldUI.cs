using UnityEngine;
using UnityEngine.UI;

public partial class TargetsDamageShieldUI : MonoBehaviour
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

        if (!target || target.IsDead || target.SHIELDMAX < 1 || target.ShieldIsBroken())
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }


        if (slider != null) slider.value = target.SHIELDFILLED();
        if (status != null) status.text = target.SHIELD + " / " + target.SHIELDMAX;
    }
}
    /*
        if (!Local.player || !Local.player.character.target
            || Local.player.character.target.IsDead
            || Local.player.character.target.SHIELDMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }

        slider.value = Local.player.character.target.SHIELDFILLED();
        status.text = Local.player.character.target.SHIELD + " / " + Local.player.character.target.SHIELDMAX;
        */

    /*
    public GameObject panel;
    public Slider damageShieldSlider;
    public Text damageShieldStatus;

    void Update()
    {
        if (!Local.player) return;

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

        damageShieldSlider.value = target.SHIELDFILLED();
        damageShieldStatus.text = target.SHIELD + " / " + target.SHIELDMAX;
    }*/