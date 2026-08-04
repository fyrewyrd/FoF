using UnityEngine;
using UnityEngine.UI;

public partial class DamageBarrierUI : MonoBehaviour
{
    public GameObject panel;
    public Slider damageBarrierSlider;
    public Text damageBarrierStatus;

    void Update()
    {
        if (!Local.player || Local.player.character.BARRIERMAX < 1 || Local.player.character.BarrierIsBroken()) { panel.SetActive(false); return; }
        else { panel.SetActive(true); }

        //CharacterSheet player = Local.player.character;
        //panel.SetActive(player != null); // hide while not in the game world
        //if (!player) return;

        //if (player.Barrier == 0) { panel.SetActive(false); }
        //else { panel.SetActive(true); }

        damageBarrierSlider.value = Local.player.character.BARRIERFILLED();
        damageBarrierStatus.text = Local.player.character.BARRIER + " / " + Local.player.character.BARRIERMAX;
    }
}
