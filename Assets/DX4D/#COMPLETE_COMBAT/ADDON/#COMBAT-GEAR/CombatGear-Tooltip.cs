using System.Text;

public abstract partial class CombatGear : EquipmentItem
{
    // T O O L T I P
    public override string ToolTip()
    {
        StringBuilder info = new StringBuilder(base.ToolTip());
        if(automaticTooltip) info.Append("{ITEMDATA}");

        info.Replace("{ITEMDATA}", "{ITEMNAME}{INFO}{DURABILITY}{ITEMDESCRIPTION}");

        info.Replace("OneHanded", "");
        info.Replace("TwoHanded", "");
        //info.Replace("Siege", "");

        info.Replace("{ITEMNAME}", "{ITEMNAMEPREFIX}" + "{NAME}" + "{ITEMNAMESUFFIX}");

        //ITEM INFO
        if (showItemDetails)
        {
            info.Replace("{INFO}", "\n{ITEMLEVEL} {RARITY} {MATERIAL} {ITEMTYPE}{LEVELREQ}");
        }
        else
        {
            info.Replace("{INFO}", "");
        }

        //DURABILITY
        if (maxDurability > 0)
        {
            info.Replace("{DURABILITY}", "\nDurability: " + durability.ToString() + " / " + maxDurability.ToString());
        }
        else
        {
            info.Replace("{DURABILITY}", "");
        }

        //DESCRIPTION //NOTE: Put description first so all tags will be replaced further down the method
        if (description != string.Empty)
        {
            info.Replace("{ITEMDESCRIPTION}", ( "\n" + "\n" + "<color=" + DX4D.Tools.GetHex.FromColor(Config.Text.Equipment.descriptionTextColor) + ">" + "{DESCRIPTION}" + "</color>" + "\n"));
        }
        else
        {
            info.Replace("ITEMDESCRIPTION", "");
        }

        //LEVEL REQ
        if (minLevel > 0)
        {
            info.Replace("{LEVELREQ}", ("\n" + "Level {ITEMLEVELREQ} required"));
            info.Replace("{ITEMLEVELREQ}", minLevel.ToString());
        }
        else
        {
            info.Replace("{LEVELREQ}", "");
        }

        //info.Replace("{CATEGORY}", category.ToString());

        info.Replace("{ITEMNAMEPREFIX}", "<b>" + ((automaticName) ? "{MATERIAL} " : ""));
        info.Replace("{ITEMNAMESUFFIX}", "</b>");

        info.Replace("{DESCRIPTION}", description); //we put description first so all tags will be replaced further down the method
        
        info.Replace("{ITEMLEVEL}", (level < 2) ? "" : "Level " + level.ToString());

        info.Replace("{RARITY}", ((gearRarity == GearRarity.Common) ? "" : "" + gearRarity.ToString()));
        info.Replace("{MATERIAL}", ((material == CraftingMaterialType.Bone) ? "" : material.ToString()) );

        info.Replace("{ITEMTYPE}", category.ToString());
        info.Replace("Weapon", "");
        info.Replace("OneHanded", "");
        info.Replace("TwoHanded", "");
        info.Replace("Unarmed", "");
        info.Replace("Siege", "");

        info.Replace("{NAME}", "<b><color=" + DX4D.Tools.GetHex.FromRarity(gearRarity) + ">" + name + "</color></b>");

        return info.ToString();
    }
}
