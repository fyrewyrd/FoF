using System.Text;

public static partial class Tooltip
{
    public static string CastingCostToolTip(CastingCost castingCosts)
    {
        StringBuilder tip = new StringBuilder();

        //SELF CASTING COSTS
        if (castingCosts.me.total > 0)
        {
            tip.Append("{CASTINGCOSTS}");
            tip.Replace("{CASTINGCOSTS}", "\n\n<b><color=white>[costs]</color></b>" + "{SHIELDCOST}{BARRIERCOST}{LIFECOST}{BLOODCOST}{SPIRITCOST}{MANACOST}{FURYCOST}{STAMINACOST}");

            tip.Replace("{SHIELDCOST}", (castingCosts.me.shield > 0) ? ("\n" + castingCosts.me.shield.ToString() + " shield") : "");
			tip.Replace("{BARRIERCOST}", (castingCosts.me.barrier > 0) ? ("\n" + castingCosts.me.barrier.ToString() + " barrier") : "");
            tip.Replace("{LIFECOST}", (castingCosts.me.life > 0) ? ("\n" + castingCosts.me.life.ToString() + " life") : "");
            tip.Replace("{MANACOST}", (castingCosts.me.mana > 0) ? ("\n" + castingCosts.me.mana.ToString() + " mana") : "");
            tip.Replace("{BLOODCOST}", (castingCosts.me.blood > 0) ? ("\n" + castingCosts.me.blood.ToString() + " blood") : "");
            tip.Replace("{SPIRITCOST}", (castingCosts.me.spirit > 0) ? ("\n" + castingCosts.me.spirit.ToString() + " spirit") : "");
            tip.Replace("{FURYCOST}", (castingCosts.me.fury > 0) ? ("\n" + castingCosts.me.fury.ToString() + " fury") : "");
            tip.Replace("{STAMINACOST}", (castingCosts.me.stamina > 0) ? ("\n" + castingCosts.me.stamina.ToString() + " stamina") : "");
        }

        //PET CASTING COSTS
        if (castingCosts.pet.total > 0)
        {
            tip.Append("{PETCASTINGCOSTS}");
            tip.Replace("{PETCASTINGCOSTS}", "\n\n<b><color=white>[pet costs]</color></b>" + "{PETSHIELDCOST}{PETBARRIERCOST}{PETLIFECOST}{PETBLOODCOST}{PETSPIRITCOST}{PETMANACOST}{PETFURYCOST}{PETSTAMINACOST}");

            tip.Replace("{PETSHIELDCOST}", (castingCosts.pet.shield > 0) ? ("\n" + castingCosts.pet.shield.ToString() + " pet shield") : "");
			tip.Replace("{PETBARRIERCOST}", (castingCosts.pet.barrier > 0) ? ("\n" + castingCosts.pet.barrier.ToString() + " pet barrier") : "");
            tip.Replace("{PETLIFECOST}", (castingCosts.pet.life > 0) ? ("\n" + castingCosts.pet.life.ToString() + " pet life") : "");
            tip.Replace("{PETMANACOST}", (castingCosts.pet.mana > 0) ? ("\n" + castingCosts.pet.mana.ToString() + " pet mana") : "");
            tip.Replace("{PETBLOODCOST}", (castingCosts.pet.blood > 0) ? ("\n" + castingCosts.pet.blood.ToString() + " pet blood") : "");
            tip.Replace("{PETSPIRITCOST}", (castingCosts.pet.spirit > 0) ? ("\n" + castingCosts.pet.spirit.ToString() + " pet spirit") : "");
            tip.Replace("{PETFURYCOST}", (castingCosts.pet.fury > 0) ? ("\n" + castingCosts.pet.fury.ToString() + " pet fury") : "");
            tip.Replace("{PETSTAMINACOST}", (castingCosts.pet.stamina > 0) ? ("\n" + castingCosts.pet.stamina.ToString() + " pet stamina") : "");
        }

        //MOUNT CASTING COSTS
        if (castingCosts.mount.total > 0)
        {
            tip.Append("{MOUNTCASTINGCOSTS}");
            tip.Replace("{MOUNTCASTINGCOSTS}", "\n\n<b><color=white>[mount costs]</color></b>" + "{MOUNTSHIELDCOST}{MOUNTLIFECOST}{MOUNTBLOODCOST}{MOUNTSPIRITCOST}{MOUNTMANACOST}{MOUNTFURYCOST}{MOUNTSTAMINACOST}");

            tip.Replace("{MOUNTSHIELDCOST}", (castingCosts.mount.shield > 0) ? ("\n" + castingCosts.mount.shield.ToString() + " mount shield") : "");
			tip.Replace("{MOUNTBARRIERCOST}", (castingCosts.mount.barrier > 0) ? ("\n" + castingCosts.mount.barrier.ToString() + " mount barrier") : "");
            tip.Replace("{MOUNTLIFECOST}", (castingCosts.mount.life > 0) ? ("\n" + castingCosts.mount.life.ToString() + " mount life") : "");
            tip.Replace("{MOUNTMANACOST}", (castingCosts.mount.mana > 0) ? ("\n" + castingCosts.mount.mana.ToString() + " mount mana") : "");
            tip.Replace("{MOUNTBLOODCOST}", (castingCosts.mount.blood > 0) ? ("\n" + castingCosts.mount.blood.ToString() + " mount blood") : "");
            tip.Replace("{MOUNTSPIRITCOST}", (castingCosts.mount.spirit > 0) ? ("\n" + castingCosts.mount.spirit.ToString() + " mount spirit") : "");
            tip.Replace("{MOUNTFURYCOST}", (castingCosts.mount.fury > 0) ? ("\n" + castingCosts.mount.fury.ToString() + " mount fury") : "");
            tip.Replace("{MOUNTSTAMINACOST}", (castingCosts.mount.stamina > 0) ? ("\n" + castingCosts.mount.stamina.ToString() + " mount stamina") : "");
        }

        return tip.ToString();
    }
}