using UnityEngine;
using UnityEngine.UI;

public partial class TargetsDamageBarrierUI : MonoBehaviour
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

        if (!target || target.IsDead || target.BARRIERMAX < 1 || target.BarrierIsBroken())
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }


        if (slider != null) slider.value = target.BARRIERFILLED();
        if (status != null) status.text = target.BARRIER + " / " + target.BARRIERMAX;
    }
}
    /*
        if (!Local.player || !Local.player.character.target
            || Local.player.character.target.IsDead
            || Local.player.character.target.BARRIERMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }

        slider.value = Local.player.character.target.BARRIERFILLED();
        status.text = Local.player.character.target.BARRIER + " / " + Local.player.character.target.BARRIERMAX;
        */

    /*
    public GameObject panel;
    public Slider damageBarrierSlider;
    public Text damageBarrierStatus;

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

        damageBarrierSlider.value = target.SHIELDFILLED();
        damageBarrierStatus.text = target.SHIELD + " / " + target.BARRIERMAX;
    }*/