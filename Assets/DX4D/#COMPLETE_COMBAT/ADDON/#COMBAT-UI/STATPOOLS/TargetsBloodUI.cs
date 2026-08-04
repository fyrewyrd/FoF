using UnityEngine;
using UnityEngine.UI;

public partial class TargetsBloodUI : MonoBehaviour
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

        if (!target || target.IsDead || target.BLOODMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }


        if (slider != null) slider.value = target.BLOODFILLED();
        if (status != null) status.text = target.BLOOD + " / " + target.BLOODMAX;
    }
}
    /*
        if (!Local.player || !Local.player.character || !Local.player.character.target || Local.player.character.target.IsDead || Local.player.character.target.BLOODMAX < 1)
        {
            panel.SetActive(false);
            return;
        }
        else { panel.SetActive(true); }

        slider.value = Local.player.character.target.BLOODFILLED();
        status.text = Local.player.character.target.BLOOD + " / " + Local.player.character.target.BLOODMAX;
        */

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        //CharacterSheet target = player.nextTarget ?? player.target;
        //if (!target || target.IsDead)
        //{
        //    panel.SetActive(false);
        //    return;
        //}
        //else if( target != null ) { panel.SetActive(true); }
