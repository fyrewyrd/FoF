using System.Text;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName="DX4D/ITEM/Resource", order=2)]
public class ResourceItem : UsableItem
{
    [SerializeField] public CraftingMaterialType material = CraftingMaterialType.Metal;
    [SerializeField] public Element elementalAffinity = Element.Neutral;
    [SerializeField] [TextArea(1, 3)] public string description;
    //[Header("Attached Skill")]
    //[SyncVar] public ScriptableSkill attachedSkill;
    //[Header("Usage Costs")]
    //[SerializeField] public UsageCosts useCost;
    /*
    [SerializeField] public bool consumeOnUse = true;

    // usage
    public override void Use(Player player, int inventoryIndex)
    {
        if(inventoryIndex < 0)
        {
            player.RpcShowOopsPopup("[out of item]");
            return;
        }
        player.RpcShowActivationPopup(attachedSkill.name.ToUpper() + "!!!");
        
        if (consumeOnUse)
        {
            ItemSlot inventorySlot = player.inventory[inventoryIndex];
            inventorySlot.DecreaseAmount(1);
            player.inventory[inventoryIndex] = inventorySlot;
        }
    }

    public override void OnUsed(Player player)
    {
        int skillNumber = -1;
        skillNumber = player.GetSkillIndexByName(attachedSkill.name);
        if (skillNumber > -1)
        {
            //USE SKILL
            player.CmdUseSkill(skillNumber);

            //APPLY COSTS
            if (useCost.healthCost > 0) player.health -= useCost.healthCost;
            if (useCost.manaCost > 0) player.mana -= useCost.manaCost;
            if (useCost.bloodCost > 0) player.blood -= useCost.bloodCost;
            if (useCost.spiritCost > 0) player.spirit -= useCost.spiritCost;
            if (useCost.staminaCost > 0) player.stamina -= useCost.staminaCost;
            if (useCost.furyCost > 0) player.fury -= useCost.furyCost;
            if (useCost.experienceCost > 0) player.experience -= useCost.experienceCost;

            if (player.activePet != null)
            {
                if (useCost.petHealthCost > 0) player.activePet.health -= useCost.petHealthCost;
                if (useCost.petManaCost > 0) player.activePet.mana -= useCost.petManaCost;
                if (useCost.petBloodCost > 0) player.activePet.blood -= useCost.petBloodCost;
                if (useCost.petSpiritCost > 0) player.activePet.spirit -= useCost.petSpiritCost;
                if (useCost.petStaminaCost > 0) player.activePet.stamina -= useCost.petStaminaCost;
                if (useCost.petFuryCost > 0) player.activePet.fury -= useCost.petFuryCost;
                if (useCost.petExperienceCost > 0) player.activePet.experience -= useCost.petExperienceCost;
            }
        }
        else
        {
            player.RpcShowOopsPopup("*fizzled*");
            Debug.LogError(player.name + " can not use " + name + "...you must add it to the Skill Templates of your Player Prefab for this item to work."); //DEBUG
        }
        base.OnUsed(player);
    }
    */
    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());
        tip.Append("<b>{ELEMENT} {NAME}</b>");
        tip.Append("\n<i>{MATERIAL}</i>");
        tip.Append("\n{DESCRIPTION}");

        tip.Replace("{NAME}", name);
        tip.Replace("{USAGECOSTS}", "");

        tip.Replace("{MATERIAL}", material.ToString());
        tip.Replace("{ELEMENT}", elementalAffinity.ToString());

        tip.Replace("{DESCRIPTION}", description);
        return tip.ToString();
            /*
            //PLAYER
            "{HEALTHCOST}" + "{MANACOST}" +
            "{BLOODCOST}" + "{SPIRITCOST}" +
            "{STAMINACOST}" + "{FURYCOST}" +
            "{EXPCOST}" +
            //PET
            "{PETHEALTHCOST}" + "{PETMANACOST}" +
            "{PETBLOODCOST}" + "{PETSPIRITCOST}" +
            "{PETSTAMINACOST}" + "{PETFURYCOST}" +
            "{PETEXPCOST}"// + "{PETSTAMINACOST}"
            );
        //PLAYER
        tip.Replace("{HEALTHCOST}", (useCost.healthCost > 0) ? "\n" + "health cost " + useCost.healthCost.ToString() : "");
        tip.Replace("{MANACOST}", (useCost.manaCost > 0) ? "\n" + "mana cost " + useCost.manaCost.ToString() : "");
        tip.Replace("{BLOODCOST}", (useCost.bloodCost > 0) ? "\n" + "blood cost " + useCost.bloodCost.ToString() : "");
        tip.Replace("{SPIRITCOST}", (useCost.spiritCost > 0) ? "\n" + "spirit cost " + useCost.spiritCost.ToString() : "");
        tip.Replace("{STAMINACOST}", (useCost.staminaCost > 0) ? "\n" + "stamina cost " + useCost.staminaCost.ToString() : "");
        tip.Replace("{FURYCOST}", (useCost.furyCost > 0) ? "\n" + "fury cost " + useCost.furyCost.ToString() : "");
        tip.Replace("{EXPCOST}", (useCost.experienceCost > 0) ? "\n" + "exp cost " + useCost.experienceCost.ToString() : "");
        //PET
        tip.Replace("{PETHEALTHCOST}", (useCost.petHealthCost > 0) ? "\n" + "pet health cost " + useCost.petHealthCost.ToString() : "");
        tip.Replace("{PETMANACOST}", (useCost.petManaCost > 0) ? "\n" + "pet mana cost " + useCost.petManaCost.ToString() : "");
        tip.Replace("{PETBLOODCOST}", (useCost.petBloodCost > 0) ? "\n" + "pet blood cost " + useCost.petBloodCost.ToString() : "");
        tip.Replace("{PETSPIRITCOST}", (useCost.petSpiritCost > 0) ? "\n" + "pet spirit cost " + useCost.petSpiritCost.ToString() : "");
        tip.Replace("{PETSTAMINACOST}", (useCost.petStaminaCost > 0) ? "\n" + "pet stamina cost " + useCost.petStaminaCost.ToString() : "");
        tip.Replace("{PETFURYCOST}", (useCost.petFuryCost > 0) ? "\n" + "pet fury cost " + useCost.petFuryCost.ToString() : "");
        tip.Replace("{PETEXPCOST}", (useCost.petExperienceCost > 0) ? "\n" + "pet exp cost " + useCost.petExperienceCost.ToString() : "");
        */
    }
}
