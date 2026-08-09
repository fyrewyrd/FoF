using UnityEngine;
using UnityEngine.UI;

public partial class DamageShieldUI : MonoBehaviour
{
    public GameObject panel;
    public Slider damageShieldSlider;
    public Text damageShieldStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.SHIELDMAX < 1 || Local.player.character.ShieldIsBroken()) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        //if (player.SHIELD == 0) { panel.SetActive(false); }
        //else { panel.SetActive(true); }

        damageShieldSlider.value = Local.player.character.SHIELDFILLED();
        damageShieldStatus.text = Local.player.character.SHIELD + " / " + Local.player.character.SHIELDMAX;
    }
}
