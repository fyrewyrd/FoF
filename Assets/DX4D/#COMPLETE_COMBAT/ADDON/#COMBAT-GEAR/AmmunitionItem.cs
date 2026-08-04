using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName="DX4D/GEAR/Ammo", order=14)]
public class AmmunitionItem : SkillItem
{
    public virtual bool FireFromWeapon(PlayerCharacter shooter, CombatWeapon weapon, int roundIndex)
    {
        if (weapon.HasAmmo)
        {
            if (weapon.ammunition[roundIndex].attachedSkill != null)
            {
                shooter.TryUseSkill(shooter.GetSkillIndexByName(weapon.ammunition[roundIndex].attachedSkill.name));
                //shooter.currentSkill = shooter.GetSkillIndexByName(weapon.ammunition[roundIndex].attachedSkill.name);
            }
            //shooter.currentSkill = shooter.GetSkillIndexByName(weapon.ammunition[0].attachedSkill.name);
            base.consumeOnUse = weapon.ammunition[roundIndex].consumeOnUse;
            //shooter.CmdUseInventoryItem(shooter.GetInventoryIndexByName(weapon.ammunition[0].name));
            base.Use(shooter, shooter.GetInventoryIndexByName(weapon.ammunition[roundIndex].name));
            //base.OnUsed(shooter);
            weapon.ammunition.RemoveAt(roundIndex); //Remove this ammo from the weapon
            return true;
        }
        else
        {
            //shooter.RpcShowTextPopup("[out of ammo]\n" + weapon.name);
            //shooter.TargetShowTextPopup("[out of ammo]\n" + weapon.name);
            return false;
        }
    }

    public override void OnUsed(Player player)
    {
        //base.OnUsed(player);
    }

    public override void Use(Player user, int inventoryIndex)
    {
        if (!(user.character is PlayerCharacter)) return;

        PlayerCharacter player = user.character as PlayerCharacter;
        player.SyncMyEquipment();

        //consumeOnUse = false;
        if (player.HasSiegeWeapon)
        {
            if (player.siegeWeapon.requiresAmmo)
            {
                if (player.ReloadWeapon(player.siegeWeapon, this) > -1)
                {
                    player.TargetShowTextPopup(name.ToString() + " loaded to " + player.siegeWeapon.name);
                }
            }
            else
            {
                player.TargetShowTextPopup("weapon does not need ammo");
            }
        }
        else if (player.HasMainWeapon)
        {
            //player.SyncEquipmentToGear();

            if (player.mainWeapon.requiresAmmo)
            {
                if (player.ReloadWeapon(player.mainWeapon, this) > -1)
                {
                    player.TargetShowTextPopup(name.ToString() + " loaded to " + player.mainWeapon.name);
                }
            }
            else
            {
                player.TargetShowTextPopup("weapon does not need ammo");
            }

            if (player.HasOffhandWeapon && player.offhandWeapon.requiresAmmo)
            {
                if (player.ReloadWeapon(player.offhandWeapon, this) > -1)
                {
                    player.TargetShowTextPopup(name.ToString() + " loaded to " + player.offhandWeapon.name);
                }
            }
        }
        else
        {
            player.TargetShowTextPopup("no weapon to reload");
        }
    }
                //base.Use(player, inventoryIndex);
                // decrease amount from equipment
                //ItemSlot equipSlot = player.equipment[inventoryIndex];
                //equipSlot.DecreaseAmount(1);
                //player.equipment[inventoryIndex] = equipSlot;

                // decrease amount from inventory
                //ItemSlot inventorySlot = player.inventory[inventoryIndex];
                //inventorySlot.DecreaseAmount(1);
                //player.inventory[inventoryIndex] = inventorySlot;

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());
        //tip.Append("{USAGECOSTS}");
        /*
        tip.Replace("{USAGECOSTS}",
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
        return tip.ToString();
    }
}
/*
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
*/
