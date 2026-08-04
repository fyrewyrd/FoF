using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnchantmentAddonMode { npc, remote, npcAndRemote, shortcuts }

public partial class UIUpgrade : MonoBehaviour
{
    [Header("")]
    public Sprite spriteRuneNull;

    [Header("Settings")]
    public EnchantmentAddonMode operatingMode;
    public static int maxUpgrade = 7;
    public bool needHolesForUpgradeItem;
    public bool canUseDifferentRunes = true;
    public bool considerItemLevel;
    public float difficultyOfEnchantment = 1;

    [Header("Settings : if enchantment is failed")]
    public bool itemDestruction;
    public bool runesDestruction;

    [Header("Settings : Item Rarity Addon - if used")]
    public bool useRarityAddon;
    public bool useTheChanceOfRarityItem;
    public float defaultBaseChance = 600;
    [Serializable]public class rar
    {
        public string Rarity;
        public int[] Chanse;
        public int[] Miss;
        public int[] Lost;
    }
    [Header("Rarity Items (BaseChance, Chance Item Destroyed, Chance Rune Destroyed)")]
    public List<rar> Rarity = new List<rar>() { new rar { Rarity = "Category", Chanse = new int[maxUpgrade], Miss = new int[maxUpgrade], Lost = new int[maxUpgrade] } };

    [Header("Settings : Info")]
    public bool showSuccessfullyEnchantmentOnChat;
    public int showSuccessfullyEnchantmentAmount;
    public bool showFailedEnchantmentOnChat;
    public int showFailedEnchantmentAmount;
    public float infoMessageShow;
    double messageTimeEnd;

    [Header("Settings : Sounds")]
    public SoundsSystem soundSystem;
    public AudioSource audioSource;
    public AudioClip soundPanelOpen;
    public AudioClip soundButtonClick;
    public AudioClip soundSuccessfully;
    public AudioClip soundFailed;
    public AudioClip soundItemSwap;

    [Header("UI Elements")]
    public GameObject panel;
    public Transform content;
    public Button buttonUpgrade;

    public GameObject ImageUpgradeIND;
    public Text textUpgradeCurrent;
    public Text textUpgradeNext;
    public Transform contentForUpgradeIND;

    [Header("UI Elements: Info")]
    public GameObject panelInfo;
    public Text textInfo;
    public Text textInfoMessage;

    [Header("UI Elements: panel successfully enchanted")]
    public GameObject panelSuccessfullyEnchanted;
    public Text textSuccessfullyEnchanted;

    //singleton
    public static UIUpgrade singleton;
    public UIUpgrade()
    {
        // assign singleton only once (to work with DontDestroyOnLoad when
        // using Zones / switching scenes)
        //if (singleton == null)
        singleton = this;
    }

    private void Start()
    {
        UIUtils.BalancePrefabs(ImageUpgradeIND.gameObject, maxUpgrade, contentForUpgradeIND);
    }

    public void Show()
    {
        panel.SetActive(true);
        PlaySoundOpenPanel();
    }

    public void Сlose()
    {
        Player.localPlayer.CmdClearUpgradeIndices();

        //null all prefabs
        for (int i = 0; i < content.childCount; ++i)
        {
            UniversalSlot slot = content.GetChild(i).GetChild(0).GetComponent<UniversalSlot>();

            // refresh invalid item
            slot.button.onClick.RemoveAllListeners();
            slot.tooltip.enabled = false;
            slot.dragAndDropable.dragable = false;
            slot.image.color = Color.clear;
            slot.image.sprite = null;
            slot.amountOverlay.SetActive(false);
            slot.upgradeText.text = "";

            //paint rarity color
            //if (useRarityAddon) slot.GetComponent<Image>().color = UIItemRarity.singleton.ColorNull;
        }

        //null runes images
        foreach (Transform child in contentForUpgradeIND.transform) child.gameObject.SetActive(false);
    }

    void Update()
    {
        Player player = Player.localPlayer;
        if (player != null)
        {
            // only update the panel if it's active
            if (panel.activeSelf)
            {
                if (AllowedToShowPanel(player))
                {
                    // refresh all items
                    for (int i = 0; i < player.upgradeIndices.Count; ++i)
                    {
                        UniversalSlot slot = content.GetChild(i).GetChild(0).GetComponent<UniversalSlot>();
                        bool state = false; // for item rarity addon

                        if (player.upgradeIndices[i] != -1 && player.upgradeIndices[i] < player.inventory.Count && player.inventory[player.upgradeIndices[i]].amount > 0)
                        {
                            state = true;

                            ItemSlot TempSlot = player.inventory[player.upgradeIndices[i]];

                            slot.button.onClick.SetListener(() => { player.CmdClearUpgradeIndex(int.Parse(slot.dragAndDropable.name)); });

                            slot.tooltip.enabled = true;
                            slot.tooltip.text = TempSlot.ToolTip();
                            slot.dragAndDropable.dragable = true;
                            slot.image.color = Color.white;
                            slot.image.sprite = TempSlot.item.image;
                            slot.amountOverlay.SetActive(TempSlot.amount > 1);
                            slot.amountText.text = TempSlot.amount.ToString();

                            if (TempSlot.item.data is EquipmentItem item && item.runes.Count > 0 && TempSlot.item.upgradeInd != null && TempSlot.item.upgradeInd.Length > 0)
                                slot.upgradeText.text = "+" + TempSlot.item.upgradeInd.Length.ToString();
                            else slot.upgradeText.text = "";
                        }
                        else
                        {
                            // refresh invalid item
                            slot.button.onClick.RemoveAllListeners();
                            slot.tooltip.enabled = false;
                            slot.dragAndDropable.dragable = false;
                            slot.image.color = Color.clear;
                            slot.image.sprite = null;
                            slot.amountOverlay.SetActive(false);
                            slot.upgradeText.text = "";
                        }

                        // addon system hooks (Item rarity)
                        Utils.InvokeMany(typeof(UIUpgrade), this, "Update_", slot, state ? player.inventory[player.upgradeIndices[i]] : new ItemSlot());
                    }

                    //paint Item rune
                    PaintRunesImages(player);

                    buttonUpgrade.interactable = player.UpgradeTimeRemaining() == 0;
                    buttonUpgrade.onClick.SetListener(() =>
                    {
                        player.CmdItemUpgrade();
                        PlaySoundButtonClick();
                    });

                    //message time end
                    if (panelInfo.activeSelf && messageTimeEnd <= NetworkTime.time) panelInfo.SetActive(false);
                }
                else
                {
                    Сlose();
                    panel.SetActive(false);
                }
            }
        }
        else panel.SetActive(false);
    }

    bool AllowedToShowPanel(Player player)
    {
        if (operatingMode == EnchantmentAddonMode.remote || operatingMode == EnchantmentAddonMode.shortcuts) return true;
        else if (operatingMode == EnchantmentAddonMode.npc && player.target != null && player.target is Npc && Utils.ClosestDistance(player, player.target) <= player.interactionRange) return true;
        else if (operatingMode == EnchantmentAddonMode.npcAndRemote) return true;
        else return false;
    }

    void PaintRunesImages(Player player)
    {
        //if slot 0 is not null
        if (player.upgradeIndices[0] != -1 && player.inventory[player.upgradeIndices[0]].amount > 0)
        {
            ItemSlot slot0 = player.inventory[player.upgradeIndices[0]];

            //enable - disable image prefabs
            if (!needHolesForUpgradeItem)
            {
                //enable all rune images
                foreach (Transform child in contentForUpgradeIND.transform)
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<Image>().sprite = spriteRuneNull;
                    child.GetComponent<Image>().color = Color.gray;
                }
            }
            else
            {
                //enable rune images = holes amount
                for (int i = 0; i < contentForUpgradeIND.childCount; ++i)
                {
                    if (i <= slot0.item.holes) contentForUpgradeIND.GetChild(i).gameObject.SetActive(true);
                    else contentForUpgradeIND.GetChild(i).gameObject.SetActive(false);
                }
            }

            //paint runes images
            if (slot0.item.upgradeInd != null && slot0.item.upgradeInd.Length > 0)
            {
                //check all upgradeIND from item
                UpgradeRuneItem rune = null;
                for (int i = 0; i < contentForUpgradeIND.childCount; ++i)
                {
                    if (i <= slot0.item.upgradeInd.Length-1)
                    {
                        rune = UpgradeRuneItem.GetRuneFromDictByType(slot0.item.upgradeInd[i]);
                        if (rune != null)
                        {
                            contentForUpgradeIND.GetChild(i).GetComponent<Image>().sprite = rune.image;
                            contentForUpgradeIND.GetChild(i).GetComponent<Image>().color = Color.white;
                        }
                    }
                    else
                    {
                        contentForUpgradeIND.GetChild(i).GetComponent<Image>().sprite = spriteRuneNull;
                        contentForUpgradeIND.GetChild(i).GetComponent<Image>().color = Color.gray;
                    }
                }

                //show upgrade info
                if (player.upgradeIndices[1] != -1) rune = ((UpgradeRuneItem)player.inventory[player.upgradeIndices[1]].item.data);
                if (rune != null && ((EquipmentItem)slot0.item.data).runes.Contains(rune))
                {
                    //in the list of all runes we find the index of the rune we want to enchant
                    int amount = slot0.item.amountRunes(rune.runeType);
                    Debug.Log(amount);
                    if (amount > 0)
                    {
                        //current
                        textUpgradeCurrent.text = "+" + rune.effect[amount - 1] + " " + rune.textInfo;

                        //next
                        if ((needHolesForUpgradeItem && slot0.item.upgradeInd.Length < slot0.item.holes) || (!needHolesForUpgradeItem && slot0.item.upgradeInd.Length < maxUpgrade))
                            textUpgradeNext.text = "+" + rune.effect[amount] + " " + rune.textInfo;
                        else textUpgradeNext.text = "Max";
                    }
                    else
                    {
                        textUpgradeCurrent.text = "";
                        textUpgradeNext.text = "";
                    }
                }
            }
            else
            {
                textUpgradeCurrent.text = "";
                textUpgradeNext.text = "";
            }
        }
        else
        {
            //disable all rune images
            foreach (Transform child in contentForUpgradeIND.transform) child.gameObject.SetActive(false);

            textUpgradeCurrent.text = "";
            textUpgradeNext.text = "";
        }
    }

    //info panel
    public void ItemUpgradeError(string message)
    {
        textInfoMessage.text = message;
        panelInfo.SetActive(true);
        messageTimeEnd = NetworkTime.time + infoMessageShow;

        PlaySoundFailed();
    }
    public void ItemUpgradeSuccess()
    {
        PlaySoundSuccess();
    }
    public void ItemInfoUpgradeSuccess(string player, string itemname, int amount)
    {
        textSuccessfullyEnchanted.text = player + " successfully enchanted a " + itemname + " for " + amount;
        panelSuccessfullyEnchanted.SetActive(true);
    }

    //sounds
    void PlaySoundOpenPanel()
    {
        if (soundSystem == SoundsSystem.viaIndividualSounds) audioSource.PlayOneShot(soundPanelOpen);
        else if (soundSystem == SoundsSystem.viaAddonMenu)
        {
            // addon system hooks (Menu Sounds)
            Utils.InvokeMany(typeof(UIUpgrade), this, "PlaySoundUI_", soundPanelOpen);
        }
    }
    void PlaySoundButtonClick()
    {
        if (soundSystem == SoundsSystem.viaIndividualSounds) audioSource.PlayOneShot(soundButtonClick);
        else if (soundSystem == SoundsSystem.viaAddonMenu)
        {
            // addon system hooks (Menu Sounds)
            Utils.InvokeMany(typeof(UIUpgrade), this, "PlaySoundUI_", soundButtonClick);
        }
    }
    void PlaySoundSuccess()
    {
        if (soundSystem == SoundsSystem.viaIndividualSounds) audioSource.PlayOneShot(soundSuccessfully);
        else if (soundSystem == SoundsSystem.viaAddonMenu)
        {
            // addon system hooks (Menu Sounds)
            Utils.InvokeMany(typeof(UIUpgrade), this, "PlaySoundUI_", soundSuccessfully);
        }
    }
    void PlaySoundFailed()
    {
        if (soundSystem == SoundsSystem.viaIndividualSounds) audioSource.PlayOneShot(soundFailed);
        else if (soundSystem == SoundsSystem.viaAddonMenu)
        {
            // addon system hooks (Menu Sounds)
            Utils.InvokeMany(typeof(UIUpgrade), this, "PlaySoundUI_", soundFailed);
        }
    }
    public void PlaySoundItemSwap()
    {
        if (soundSystem == SoundsSystem.viaIndividualSounds) audioSource.PlayOneShot(soundItemSwap);
        else if (soundSystem == SoundsSystem.viaAddonMenu)
        {
            // addon system hooks (Menu Sounds)
            Utils.InvokeMany(typeof(UIUpgrade), this, "PlaySoundUI_", soundItemSwap);
        }
    }
}
