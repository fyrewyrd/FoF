using System;

public partial class Database
{
    static runeType[] LoadUpgradeInd(string ind)
    {
        runeType[] upgradeInd = null;
        if (!string.IsNullOrEmpty(ind))
        {
            string temp = ind;
            upgradeInd = new runeType[1];
            //We are looping through all instances of the letter in the given string
            int i = 0;
            while (temp.IndexOf(";") != -1)
            {
                if (upgradeInd.Length <= i) Array.Resize(ref upgradeInd, upgradeInd.Length + 1);
                upgradeInd[i] = (runeType)int.Parse(temp.Substring(0, temp.IndexOf(";")));
                temp = temp.Remove(0, temp.IndexOf(";") + 1);
                i++;
            }
        }

        return upgradeInd;
    }
    static string SaveUpgradeInd(Item item)
    {
        if (item.upgradeInd != null && item.upgradeInd.Length > 0)
        {
            string upgradeInd = "";
            for (int i = 0; i < item.upgradeInd.Length; i++)
                upgradeInd += (int)item.upgradeInd[i] + ";";

            return upgradeInd;
        }
        else return "";
    }

    //add rows
    partial class character_inventory
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_storage
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_storage_guild
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_auction
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_mail
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_mounts_inventory
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }
    partial class character_mounts_equipment
    {
        public int holes { get; set; }
        public string upgradeInd { get; set; }
    }

    //load
    Item CharacterStorageLoad_Upgrade(Item item, character_storage row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item CharacterStorageGuildLoad_Upgrade(Item item, character_storage_guild row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item CharacterAuctionLoad_Upgrade(Item item, character_auction row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item CharacterMailLoad_Upgrade(Item item, character_mail row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item MountInventoryLoad_Upgrade(Item item, character_mounts_inventory row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item MountEquipmentLoad_Upgrade(Item item, character_mounts_equipment row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }
    Item GCPCharacterEquipmentLoad_Upgrade(Item item, character_equipment row)
    {
        item.holes = row.holes;
        item.upgradeInd = LoadUpgradeInd(row.upgradeInd);

        return item;
    }


    //save
    character_inventory CharacterInventorySave_Upgrade(character_inventory row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_storage CharacterStorageSave_Upgrade(character_storage row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_storage_guild CharacterStorageGuildSave_Upgrade(character_storage_guild row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_auction CharacterAuctionSave_Upgrade(character_auction row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_mail CharacterMailSave_Upgrade(character_mail row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_mounts_inventory MountInventorySave_Upgrade(character_mounts_inventory row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
    character_mounts_equipment MountEquipmentSave_Upgrade(character_mounts_equipment row, Item item)
    {
        row.holes = item.holes;
        row.upgradeInd = SaveUpgradeInd(item);

        return row;
    }
}