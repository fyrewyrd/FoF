using Mirror;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum runeType { extract, damage, defense, accuracy, dodge, range, vampirism, crit, block, moveSpeed, attackSpeed, antiCrit, antiStun, antiBlock }

public partial class UIShortcuts
{
    [Header("GFF Upgrade Addon")]
    public Button buttonEnchantment;

    public void Update_Enchantment(Player player)
    {
        buttonEnchantment.gameObject.SetActive(UIUpgrade.singleton.operatingMode == EnchantmentAddonMode.shortcuts);
        buttonEnchantment.onClick.SetListener(() =>
        {
            if (UIUpgrade.singleton.panel.activeSelf) UIUpgrade.singleton.Сlose();
            else UIUpgrade.singleton.Show();
        });
    }
}

public partial class UINpcDialogue
{
    [Header("GFF Upgrade Addon")]
    public Button buttonEnchantment;

    public void Update_Enchantment(Player player, Npc npc)
    {
        //checks      
        buttonEnchantment.gameObject.SetActive(npc.itemEnchantment
            && npc.characterLevelRequiredForEnchantments <= player.level // player level
            && (npc.characterClassRequired.Count == 0 || npc.characterClassRequired.Contains(player.className)) //player class
            && (UIUpgrade.singleton.operatingMode == EnchantmentAddonMode.npc || UIUpgrade.singleton.operatingMode == EnchantmentAddonMode.npcAndRemote)); //Enchantment Addon Mode

        buttonEnchantment.onClick.SetListener(() =>
        {
            UIUpgrade.singleton.Show();
            inventoryPanel.SetActive(true);
            panel.SetActive(false);
        });
    }
}

public partial class Npc
{
    [Header("GFF Item Enchantment Addon")]
    public bool itemEnchantment = true;
    public int characterLevelRequiredForEnchantments;
    public List<string> characterClassRequired;
    //public int amountofSlotsForEnchantmentInItemsSold = UIUpgrade.maxUpgrade;
}

public partial struct Item
{
    public int holes;
    public runeType[] upgradeInd;

    public int amountRunes(runeType type)
    {
        int amount = 0;
        if (upgradeInd != null)
        {
            for (int i = 0; i < upgradeInd.Length; ++i)
            {
                if (upgradeInd[i] == type) amount += 1;
            }
        }

        return amount;
    }

    public int BonusAsPercentageOfEnchantment(int value, runeType runetype)
    {
        if (upgradeInd != null && upgradeInd.Length > 0)
        {
            int amount = amountRunes(runetype);
            UpgradeRuneItem rune = UpgradeRuneItem.GetRuneFromDictByType(runetype);

            //if the rune is found
            if (rune != null && amount > 0)
            {
                return value + (int)(((float)value / 100) * rune.effect[amount - 1]);
            }
        }

        return value;
    }

    public int BonusOfEnchantment(int value, runeType runetype)
    {
        if (upgradeInd != null && upgradeInd.Length > 0)
        {
            int amount = amountRunes(runetype);
            UpgradeRuneItem rune = UpgradeRuneItem.GetRuneFromDictByType(runetype);

            //if the rune is found
            if (rune != null && amount > 0)
            {
                return value + rune.effect[amount - 1];
            }
        }

        return value;
    }
    public float BonusOfEnchantment(float value, runeType runetype)
    {
        if (upgradeInd != null && upgradeInd.Length > 0)
        {
            int amount = amountRunes(runetype);
            UpgradeRuneItem rune = UpgradeRuneItem.GetRuneFromDictByType(runetype);

            //if the rune is found
            if (rune != null && amount > 0)
            {
                return value + rune.effect[amount - 1];
            }
        }

        return value;
    }

    public int ItemUpgradeBonus(runeType type)
    {
        if (upgradeInd != null && upgradeInd.Length > 0)
        {
            int amount = amountRunes(type);
            UpgradeRuneItem rune = UpgradeRuneItem.GetRuneFromDictByType(type);

            //if the rune is found
            if (rune != null && amount > 0) return rune.effect[amount - 1];
        }

        return 0;
    }

    public string UpgradeEffects()
    {
        string info = "";

        foreach (var v in UpgradeRuneItem.runeDict)
        {
            int amount = amountRunes(v.Value.runeType);
            if(amount > 0)
            {
                info += v.Value.runeName + " +" + v.Value.effect[amount - 1] + "" + v.Value.textInfo;
            }
        }

        return info;
    }

    void ToolTip_Upgrade(StringBuilder tip)
    {
        if (data is EquipmentItem item)
        {
            if (upgradeInd != null && upgradeInd.Length > 0)
            {
                //damage
                tip.Replace("{DAMAGEBONUS}", BonusAsPercentageOfEnchantment(item.damageBonus, runeType.damage).ToString());

                //Min and Max addon
                //tip.Replace("{DAMAGEBONUS}", BonusAsPercentageOfEnchantment(item.damageMinBonus, runeType.damage) + "/" + BonusAsPercentageOfEnchantment(item.damageMaxBonus, runeType.damage));

                //defense
                tip.Replace("{DEFENSEBONUS}", BonusAsPercentageOfEnchantment(item.defenseBonus, runeType.defense).ToString());

                tip.Replace("{UPGRADE}", "+" + upgradeInd.Length.ToString());
            }
            else
            {
                //original
                tip.Replace("{DAMAGEBONUS}", item.damageBonus.ToString());

                //if used Min and Max Damage Addon
                //tip.Replace("{DAMAGEBONUS}", item.damageMinBonus + "/" + item.damageMaxBonus);

                tip.Replace("{DEFENSEBONUS}", item.defenseBonus.ToString());

                tip.Replace("{UPGRADE}", "none");
            }
        }
    }
}

public partial class EquipmentItem
{
    [Header("What runes can be used to enchant this item?")]
    public List<UpgradeRuneItem> runes;
}

public partial struct ScriptableItemAndAmount
{
    [Header("if used Item Enchantment addon")]
    public int holes;
    public runeType[] upgradeInd;
}

public partial class Entity
{
    float BonusUpgradeMoveSpeed_Upgrade(ItemSlot slot)
    {
        return slot.item.ItemUpgradeBonus(runeType.moveSpeed);
    }
}

public partial class Player
{
    [HideInInspector] public SyncListInt upgradeIndices = new SyncListInt() { -1, -1, -1, -1, -1, -1 };

    public float UpgradeTimeRemaining() => NetworkTime.time >= upgradeTimeEnd ? 0 : (float)(upgradeTimeEnd - NetworkTime.time);
    [SyncVar] public double upgradeTimeEnd; // server time. double for long term precision.
    bool checkDurability;
    int rarityValue = 0;

    void OnDragAndDrop_InventorySlot_UpgradeSlot(int[] slotIndices)
    {
        ItemSlot slot = inventory[slotIndices[0]];

        //if this armor or weapon
        if (slot.amount > 0 && slotIndices[1] == 0 && slot.item.data is EquipmentItem item && item.runes.Count > 0)
        {
            // swap them
            CmdSwapInventoryUpgrade(slotIndices);
            UIUpgrade.singleton.PlaySoundItemSwap();
        }
        //if this is rune
        if (slot.amount > 0 && slot.item.data is UpgradeRuneItem && slotIndices[1] == 1)
        {
            // swap them
            CmdSwapInventoryUpgrade(slotIndices);
            UIUpgrade.singleton.PlaySoundItemSwap();
        }
        //if this is gems
        if (slot.amount > 0 && slot.item.data is UpgradeGemItem gem && gem.gemType == slotIndices[1] - 1)
        {
            // swap them
            CmdSwapInventoryUpgrade(slotIndices);
            UIUpgrade.singleton.PlaySoundItemSwap();
        }
    }

    [Command]
    public void CmdSwapInventoryUpgrade(int[] slotIndices)
    {
        ItemSlot slot = inventory[slotIndices[0]];

        //if this armor or weapon
        if (slot.amount > 0 && slotIndices[1] == 0 && slot.item.data is EquipmentItem item && item.runes.Count > 0)
        {
            // swap them
            upgradeIndices[slotIndices[1]] = slotIndices[0];
        }
        //if this is rune
        if (slot.amount > 0 && slot.item.data is UpgradeRuneItem && slotIndices[1] == 1)
        {
            // swap them
            upgradeIndices[slotIndices[1]] = slotIndices[0];
        }
        //if this is gems
        if (slot.amount > 0 && slot.item.data is UpgradeGemItem gem && gem.gemType == slotIndices[1] - 1)
        {
            // swap them
            upgradeIndices[slotIndices[1]] = slotIndices[0];
        }
    }

    [Command]
    public void CmdClearUpgradeIndices()
    {
        for (int i = 0; i < upgradeIndices.Count; i++)
        {
            upgradeIndices[i] = -1;
        }

    }
    [Command]
    public void CmdClearUpgradeIndex(int index)
    {
        upgradeIndices[index] = -1;
    }

    [Command]
    public void CmdItemUpgrade()
    {
        //are slots with enchanted item and rune not empty?
        if (upgradeIndices[0] != -1 && inventory[upgradeIndices[0]].amount > 0 && upgradeIndices[1] != -1 && inventory[upgradeIndices[1]].amount > 0)
        {
            ItemSlot slot_0 = inventory[upgradeIndices[0]];
            runeType newRuneType = ((UpgradeRuneItem)inventory[upgradeIndices[1]].item.data).runeType;

            if (newRuneType == runeType.extract)
            {
                if (slot_0.item.upgradeInd.Length > 0) StartEnchantment(0);
                else TargetItemUpgradeError("It is impossible to use this rune");
            }
            else
            {
                //check item durability
                checkDurability = true;

                // addon system hooks (durability)
                Utils.InvokeMany(typeof(Player), this, "checkDurability_", slot_0);

                if (checkDurability)
                {
                    //need holes for upgrade ?
                    if ((slot_0.item.upgradeInd == null || slot_0.item.upgradeInd.Length < UIUpgrade.maxUpgrade) &&
                                (UIUpgrade.singleton.needHolesForUpgradeItem == false || (UIUpgrade.singleton.needHolesForUpgradeItem && slot_0.item.holes > slot_0.item.upgradeInd.Length)))
                    {
                        //check the combination

                        //if use different runes or the item is not yet enchanted or 
                        if ((UIUpgrade.singleton.canUseDifferentRunes || slot_0.item.upgradeInd.Length == 0 || newRuneType == slot_0.item.upgradeInd[0]) &&
                            ((EquipmentItem)slot_0.item.data).runes.Contains((UpgradeRuneItem)inventory[upgradeIndices[1]].item.data))
                        {
                            StartEnchantment(newRuneType);
                        }
                        else TargetItemUpgradeError("Runes do not match");
                    }
                    else TargetItemUpgradeError("Maximum Enchantment");
                }
                else TargetItemUpgradeError("Item Durability is too low");
            }
        }
        else TargetItemUpgradeError("The combination does not match");
    }

    [Server]
    void StartEnchantment(runeType newRuneType)
    {
        float coefficientOfGems = 0;         //gemstone coefficient of influence
        float item_level_factor = 0;         //item level coefficient
        float accounting_chance = 0;         //chance of successful enchantment

        //if rune is not extract
        if (newRuneType != runeType.extract)
        {
            //gemstone coefficient of influence
            for (int i = 0; i < upgradeIndices.Count; i++)
            {
                if (upgradeIndices[i] != -1 && inventory[upgradeIndices[i]].item.data is UpgradeGemItem gem)
                    coefficientOfGems = coefficientOfGems + gem.gemChanceIncrease;
            }

            //item level coefficient
            if (UIUpgrade.singleton.considerItemLevel == false) item_level_factor = UIUpgrade.singleton.difficultyOfEnchantment;
            else item_level_factor = UIUpgrade.singleton.difficultyOfEnchantment / ((UsableItem)inventory[upgradeIndices[0]].item.data).minLevel;

            // addon system hooks (Item rarity)
            rarityValue = 0;
            Utils.InvokeMany(typeof(UIUpgrade), this, "rarityValue_", this);

            //ru - базовый шанс заточки (берем из айтема в зависимости от апгрэйда)
            //eng - chance of successful enchantment
            if (!UIUpgrade.singleton.useTheChanceOfRarityItem) accounting_chance = UIUpgrade.singleton.defaultBaseChance * (coefficientOfGems * 100 / 4);
            else accounting_chance = UIUpgrade.singleton.Rarity[rarityValue].Chanse[inventory[upgradeIndices[0]].item.upgradeInd.Length] * (coefficientOfGems * 100 / 4);

            //eng - calculate the final chance of enchantment
            ItemSlot slot_0 = inventory[upgradeIndices[0]];

            //if final chance of enchantment more than random value
            if (accounting_chance * item_level_factor > UnityEngine.Random.Range(0, 10000))
            {
                if (slot_0.item.upgradeInd == null) slot_0.item.upgradeInd = new runeType[1];
                else Array.Resize(ref slot_0.item.upgradeInd, slot_0.item.upgradeInd.Length + 1);
                slot_0.item.upgradeInd[slot_0.item.upgradeInd.Length - 1] = newRuneType;

                inventory[upgradeIndices[0]] = slot_0;

                //show informational messages
                TargetItemUpgradeSuccess();

                if (UIUpgrade.singleton.showSuccessfullyEnchantmentOnChat && slot_0.item.upgradeInd.Length >= UIUpgrade.singleton.showSuccessfullyEnchantmentAmount)
                {
                    string message = "Successfully enchanted " + slot_0.item.name + " on " + slot_0.item.upgradeInd.Length;

                    //if used chat extended addon
                    //chat.OnSubmit(message, chatChannel.info);

                    //chat
                    chat.OnSubmit(message);
                }

                if (slot_0.item.upgradeInd.Length >= UIUpgrade.singleton.showSuccessfullyEnchantmentAmount)
                {
                    foreach (Player player in Player.onlinePlayers.Values)
                    {
                        player.RpcSuccessfullyEnchantedOpenPanel(name, slot_0.item.name, slot_0.item.upgradeInd.Length);
                    }
                }
            }
            else
            {
                int upgradeIndLength = slot_0.item.upgradeInd != null ? slot_0.item.upgradeInd.Length : 0;

                if (UIUpgrade.singleton.itemDestruction && UIUpgrade.singleton.Rarity[rarityValue].Miss[upgradeIndLength] > UnityEngine.Random.Range(0, 10000))
                {
                    slot_0.amount = 0;
                    inventory[upgradeIndices[0]] = slot_0;

                    TargetItemUpgradeError("Item Destroyed");
                }
                else if (UIUpgrade.singleton.runesDestruction && UIUpgrade.singleton.Rarity[rarityValue].Lost[upgradeIndLength] > UnityEngine.Random.Range(0, 10000))
                {
                    // Item Durability
                    /*if (UIUpgrade.singleton.useDurabilityAddon && UIUpgrade.singleton.durabilityDecreasesWhenRunesDestroyed)
                        slot_0.item.DecreaseDurability(UIUpgrade.singleton.durabilityDecreaseValue);*/

                    slot_0.item.upgradeInd = null;
                    inventory[upgradeIndices[0]] = slot_0;

                    TargetItemUpgradeError("Runes Destroyed");
                }
                else
                {
                    // Item Durability
                    /*if (UIUpgrade.singleton.useDurabilityAddon && UIUpgrade.singleton.durabilityDecreasesWhenRunesDestroyed)
                        slot_0.item.DecreaseDurability(UIUpgrade.singleton.durabilityDecreaseValue);*/

                    TargetItemUpgradeError("Modification failed");
                }

                //show informational messages
                if (UIUpgrade.singleton.showFailedEnchantmentOnChat && slot_0.item.upgradeInd != null && slot_0.item.upgradeInd.Length >= UIUpgrade.singleton.showFailedEnchantmentAmount)
                {
                    string message = "Failed enchanted " + slot_0.item.name + " on " + slot_0.item.upgradeInd.Length;

                    //if used chat extended addon
                    //chat.OnSubmit(message, chatChannel.info);
                    chat.OnSubmit(message);
                }
            }

            //decrease amount gems and rune on 1
            for (int i = 1; i < upgradeIndices.Count; i++)
            {
                if (upgradeIndices[i] != -1)
                {
                    ItemSlot slot_i = inventory[upgradeIndices[i]];
                    slot_i.amount = slot_i.amount - 1;
                    inventory[upgradeIndices[i]] = slot_i;
                }
            }
        }
        else
        {
            ItemSlot slot_0 = inventory[upgradeIndices[0]];  //item
            ItemSlot slot_1 = inventory[upgradeIndices[1]];  //rune

            UpgradeRuneItem item = UpgradeRuneItem.GetRuneFromDictByType(newRuneType);
            if (item != null)
            {
                //add rune to inventory
                if (InventoryAdd(new Item(item), 1))
                {
                    //decrease enchantment value
                    //slot_0.item.upgradeInd.RemoveAt(slot_0.item.upgradeInd.Length);
                    Array.Resize(ref slot_0.item.upgradeInd, slot_0.item.upgradeInd.Length - 1);

                    // Item Durability
                    /*if (UIUpgrade.singleton.useDurabilityAddon && UIUpgrade.singleton.durabilityDecreasesWhenRunesDestroyed)
                        slot_0.item.DecreaseDurability(UIUpgrade.singleton.durabilityDecreaseValue);*/

                    inventory[upgradeIndices[0]] = slot_0;

                    //decrease amount the extract rune
                    slot_1.amount -= 1;
                    inventory[upgradeIndices[1]] = slot_1;

                    TargetItemUpgradeSuccess();
                }
                else TargetItemUpgradeError("Inventory is full");
            }
        }
    }

    [TargetRpc] // only send to one client
    public void TargetItemUpgradeError(string message)
    {
        UIUpgrade.singleton.ItemUpgradeError(message);
    }
    [TargetRpc] // only send to one client
    public void TargetItemUpgradeSuccess()
    {
        UIUpgrade.singleton.ItemUpgradeSuccess();
    }

    [ClientRpc]
    void RpcSuccessfullyEnchantedOpenPanel(string playername, string itemname, int amount)
    {
        UIUpgrade.singleton.ItemInfoUpgradeSuccess(playername, itemname, amount);
    }
}

public partial class ItemDropChance
{
    [Header("GFF Item Enchantment Addon")]
    public int minAmountHolesForUpgrade = 1;
    public int maxAmountHolesForUpgrade = UIUpgrade.maxUpgrade;
}

public partial class UIInventoryExtended
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIEquipmentExtended
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UITargetViewEquipment
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIStorage
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}
public partial class UIStorageGuild
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIMountInventory
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}
public partial class UIMountEquipment
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIMailViewMessage
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}
public partial class UIMailNewMessage
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIItemDurability
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}

public partial class UIGameControlAccount
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }
}
public partial class UIGameControlCharacters
{
    public Item UpdateItem_upgrade(Item item, ScriptableItemAndAmount fromItem)
    {
        item.holes = fromItem.holes;
        item.upgradeInd = fromItem.upgradeInd;

        return item;
    }
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd != null && item.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
            slot.upgradeText.fontSize = 16;
        }
        else slot.upgradeText.text = "";
    }
}
public partial class UIGameControlShop
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }

    public Item UpdateItem_upgrade(Item item, ScriptableItemAndAmount fromItem)
    {
        item.holes = fromItem.holes;
        item.upgradeInd = fromItem.upgradeInd;

        return item;
    }
}
public partial class UIGameControlPremium
{
    public Item UpdateItem_upgrade(Item item, ScriptableItemAndAmount fromItem)
    {
        item.holes = fromItem.holes;
        item.upgradeInd = fromItem.upgradeInd;

        return item;
    }
}
public partial class UIGameControlBonuses
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd.Length > 0)
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
        else slot.upgradeText.text = "";
    }

    public Item UpdateItem_upgrade(Item item, ScriptableItemAndAmount fromItem)
    {
        item.holes = fromItem.holes;
        item.upgradeInd = fromItem.upgradeInd;

        return item;
    }
}
public partial class UIGameControlAuction
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot item)
    {
        //paint upgrade values
        if (item.amount > 0 && item.item.data is EquipmentItem it && it.runes.Count > 0 && item.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + item.item.upgradeInd.Length.ToString();
            slot.upgradeText.fontSize = 17;
        }
        else slot.upgradeText.text = "";
    }
}

public partial class UINpcTradingExtended
{
    public void UpdateItemSlot_upgrade(UniversalSlot slot, ItemSlot itemSlot)
    {
        //paint upgrade values
        if (itemSlot.amount > 0 && itemSlot.item.data is EquipmentItem it && itemSlot.item.upgradeInd != null && itemSlot.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + itemSlot.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }
}

public partial class UINewQuest
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot itemSlot)
    {
        //paint upgrade values
        if (itemSlot.amount > 0 && itemSlot.item.data is EquipmentItem && itemSlot.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + itemSlot.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }
}
public partial class UIQuestsByNpc
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot itemSlot)
    {
        //paint upgrade values
        if (itemSlot.amount > 0 && itemSlot.item.data is EquipmentItem && itemSlot.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + itemSlot.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }
}
public partial class UIQuestsExtended
{
    public void Update_upgrade(UniversalSlot slot, ItemSlot itemSlot)
    {
        //paint upgrade values
        if (itemSlot.amount > 0 && itemSlot.item.data is EquipmentItem && itemSlot.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + itemSlot.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }
}

public partial class CraftingRecipe
{
    [Header("Settings : If use addon enchantment?")]
    public float[] craftingChance = new float[UIUpgrade.maxUpgrade + 1];
    public bool holesRandom;
    public int minHoles = 1;
    public int maxHoles;
}
public partial class UICraftingExtended
{
    public void UpdateItemSlot_upgrade(UniversalSlot slot, ItemSlot itemSlot)
    {
        //paint upgrade values
        if (itemSlot.amount > 0 && itemSlot.item.data is EquipmentItem it && itemSlot.item.upgradeInd.Length > 0)
        {
            slot.upgradeText.text = "+" + itemSlot.item.upgradeInd.Length.ToString();
        }
        else slot.upgradeText.text = "";
    }
}