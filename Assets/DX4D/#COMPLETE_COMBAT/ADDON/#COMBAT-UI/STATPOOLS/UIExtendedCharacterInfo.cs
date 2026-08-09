// Note: this script has to be on an always-active UI parent, so that we can
// always react to the hotkey.
using UnityEngine;
using UnityEngine.UI;

public partial class UIExtendedCharacterInfo : MonoBehaviour
{
    public KeyCode hotKey = KeyCode.R;
    public GameObject panel;
    //public Text damageText;
    //public Text defenseText;


    public Text bloodText;
    public Text spiritText;

    public Text staminaText;
    public Text furyText;


    public Text fireResist;
    public Text fireDefense;
    public Text fireDamage;

    public Text iceResist;
    public Text iceDefense;
    public Text iceDamage;

    public Text lightningResist;
    public Text lightningDefense;
    public Text lightningDamage;

    public Text waterResist;
    public Text waterDefense;
    public Text waterDamage;

    public Text airResist;
    public Text airDefense;
    public Text airDamage;

    public Text earthResist;
    public Text earthDefense;
    public Text earthDamage;

    public Text arcaneResist;
    public Text arcaneDefense;
    public Text arcaneDamage;

    public Text holyResist;
    public Text holyDefense;
    public Text holyDamage;
	
	public Text ancientResist;
    public Text ancientDefense;
    public Text ancientDamage;
	
	public Text spiritResist;
    public Text spiritDefense;
    public Text spiritDamage;
	
	public Text runicResist;
    public Text runicDefense;
    public Text runicDamage;

    //public Text speedText;
    //public Text levelText;
    //public Text currentExperienceText;
    //public Text maximumExperienceText;
    //public Text skillExperienceText;
    //public Text strengthText;
    //public Text intelligenceText;
    //public Button strengthButton;
    //public Button intelligenceButton;

    void Update()
    {
        CharacterSheet player = Local.player.character;
        if (!player) return;

        // hotkey (not while typing in chat, etc.)
        if (Input.GetKeyDown(hotKey) && !UIUtils.AnyInputActive())
            panel.SetActive(!panel.activeSelf);

        // only refresh the panel while it's active
        if (panel.activeSelf)
        {
            //damageText.text = player.damage.ToString();
            //defenseText.text = player.defense.ToString();


            bloodText.text = player.BLOODMAX.ToString();
            spiritText.text = player.SPIRITMAX.ToString();
            
            staminaText.text = player.STAMINAMAX.ToString();
            furyText.text = player.FURYMAX.ToString();

            /* //DEPRECIATED
            fireResist.text = player.fireVulnerability.ToString();
            fireDefense.text = player.fireDamageReduction.ToString();
            fireDamage.text = player.fireDamageMultiplier.ToString();

            iceResist.text = player.iceVulnerability.ToString();
            iceDefense.text = player.iceDamageReduction.ToString();
            iceDamage.text = player.iceDamageMultiplier.ToString();

            lightningResist.text = player.lightningVulnerability.ToString();
            lightningDefense.text = player.lightningDamageReduction.ToString();
            lightningDamage.text = player.lightningDamageMultiplier.ToString();

            waterResist.text = player.waterVulnerability.ToString();
            waterDefense.text = player.waterDamageReduction.ToString();
            waterDamage.text = player.waterDamageMultiplier.ToString();

            airResist.text = player.airVulnerability.ToString();
            airDefense.text = player.airDamageReduction.ToString();
            airDamage.text = player.airDamageMultiplier.ToString();

            earthResist.text = player.earthVulnerability.ToString();
            earthDefense.text = player.earthDamageReduction.ToString();
            earthDamage.text = player.earthDamageMultiplier.ToString();

            darkResist.text = player.darkVulnerability.ToString();
            darkDefense.text = player.darkDamageReduction.ToString();
            darkDamage.text = player.darkDamageMultiplier.ToString();

            holyResist.text = player.holyVulnerability.ToString();
            holyDefense.text = player.holyDamageReduction.ToString();
            holyDamage.text = player.holyDamageMultiplier.ToString();
            */

            //speedText.text = player.speed.ToString();
            //levelText.text = player.level.ToString();
            //currentExperienceText.text = player.experience.ToString();
            //maximumExperienceText.text = player.experienceMax.ToString();
            //skillExperienceText.text = player.skillExperience.ToString();

            //strengthText.text = player.strength.ToString();
            //strengthButton.interactable = player.AttributesSpendable() > 0;
            //strengthButton.onClick.SetListener(() => {
            //    player.CmdIncreaseStrength();
            //});

            //intelligenceText.text = player.intelligence.ToString();
            //intelligenceButton.interactable = player.AttributesSpendable() > 0;
            //intelligenceButton.onClick.SetListener(() => {
            //    player.CmdIncreaseIntelligence();
            //});
        }
    }
}
