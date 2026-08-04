using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName="DX4D/ITEM/Healing Item", order=3)]
public class HealingItem : UsableItem
{
    [Header("HEALING ABILITY")]
    [SerializeField][Range(0, 100)] int healingPower = 0;
    [SerializeField][Range(0, 100)] int refreshPower = 0;
    [SerializeField][Range(0, 100)] int regainPower = 0;
    [SerializeField][Range(0, 100)] int renewPower = 0;
    [SerializeField][Range(0, 100)] int ragePower = 0;
    [SerializeField][Range(0, 100)] int resurrectPower = 0;

    [Header("TARGETING")]
    [SerializeField] public bool canTargetSelf = true;
    [SerializeField] public bool canTargetOthers = true;

    [Header("USAGE COSTS")]
    [SerializeField] public CastingCost costs;
    [SerializeField] public bool consumeOnUse = true;

    [Header("TOOLTIPS")]
    [SerializeField] public bool automaticTooltip = true;
    [SerializeField, TextArea(1, 30)] public string description;
    //StatPool stats;
    //[Header("Potion")]
    //public int usageHealth;
    //public int usageMana;
    //public int usageExperience;
    //public int usagePetHealth; // to heal pet

    // usage
    //[Server]
    //public override void Use(Player player, int inventoryIndex)
    //{
    // always call base function too
    //    base.Use(player, inventoryIndex);

    // increase health/mana/etc.
    //    player.health += usageHealth;
    //    player.mana += usageMana;
    //    player.experience += usageExperience;
    //    if (player.activePet != null) player.activePet.health += usagePetHealth;

    // decrease amount
    //    ItemSlot slot = player.inventory[inventoryIndex];
    //    slot.DecreaseAmount(1);
    //    player.inventory[inventoryIndex] = slot;
    //}
    ///[Server]
    public override void Use(Player player, int inventoryIndex)
    {
        Use(player.GetComponent<PlayerCharacter>(), inventoryIndex);
    }
    public void Use(PlayerCharacter player, int inventoryIndex)
    {
        //VERIFY OWNERSHIP
        if (inventoryIndex < 0)
        {
            player.TargetShowTextPopup("[you have no " + name.ToLower() + "]");
            return;
        }

        //HEAL HP
        if (healingPower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.ApplyHealTo(player.target, (player.target.stats.lifeMax.Get(player.target.level) * healingPower / 100) );
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.ApplyHealTo(player, (player.stats.lifeMax.Get(player.level) * healingPower / 100) );
            }
        }
        
        //HEAL MP
        if (refreshPower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.ApplyHealTo(player.target, (player.target.stats.manaMax.Get(player.target.level) * refreshPower / 100) );
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.ApplyHealTo(player, (player.stats.manaMax.Get(player.level) * refreshPower / 100) );
            }
        }

        //HEAL ST
        if (regainPower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.ApplyHealTo(player.target, (player.target.stats.staminaMax.Get(player.target.level) * regainPower / 100) );
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.ApplyHealTo(player, (player.stats.staminaMax.Get(player.level) * regainPower / 100) );
            }
        }

        //HEAL SP
        if (renewPower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.ApplyHealTo(player.target, (player.target.stats.spiritMax.Get(player.target.level) * renewPower / 100) );
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.ApplyHealTo(player, (player.stats.spiritMax.Get(player.level) * renewPower / 100) );
            }
        }

        //HEAL RP
        if (ragePower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.ApplyHealTo(player.target, (player.target.stats.furyMax.Get(player.target.level) * ragePower / 100) );
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.ApplyHealTo(player, (player.stats.furyMax.Get(player.level) * ragePower / 100) );
            }
        }

        //REVIVE
        if (resurrectPower > 0)
        {
            if (canTargetOthers && player.target && player.target.GetComponent<Player>())
            {
                player.RpcShowTextPopup("Resurrecting " + player.target.name + "...");
                player.target.resurrectionLevel += resurrectPower;
            }
            else if (canTargetSelf)// && !player.target)
            {
                player.RpcShowTextPopup("you feel bolstered against death");
                player.resurrectionLevel += resurrectPower;
            }
        }

        //CONSUME ON USE
        if (consumeOnUse)
        {
            ItemSlot inventorySlot = player.INVENTORY[inventoryIndex];
            inventorySlot.DecreaseAmount(1);
            player.INVENTORY[inventoryIndex] = inventorySlot;
        }
    }

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());

        if (automaticTooltip)
        {
            tip.Append("<b>{NAME}</b>");
            tip.Append("{DESCRIPTION}");
            tip.Append("{CASTINGCOST}");
            tip.Append("{HEALS}");
            tip.Append("{REFRESHES}");
            tip.Append("{REGAINS}");
            tip.Append("{RENEWS}");
            tip.Append("{RAGE}");
            tip.Append("{REVIVES}");
            //if (creates != null) { tip.Append("Creates " + creates.name); }
        }
        tip.Replace("{NAME}", name);
        tip.Replace("{DESCRIPTION}", (description != string.Empty) ? "\n" + description : "");

        tip.Replace("{CASTINGCOST}", Tooltip.CastingCostToolTip(costs));

        tip.Replace("{HEALS}", (healingPower > 0) ? "\n\n" + "Heals target for " + healingPower + "% of their lifeforce" : "");
        tip.Replace("{REFRESHES}", (refreshPower > 0) ? "\n\n" + "Refreshes target for " + refreshPower + "% of their manaforce" : "");
        tip.Replace("{REGAINS}", (regainPower > 0) ? "\n\n" + "Regains target for " + regainPower + "% of their stamina" : "");
        tip.Replace("{RENEWS}", (renewPower > 0) ? "\n\n" + "Renews target for " + renewPower + "% of their soul" : "");
        tip.Replace("{RAGE}", (ragePower > 0) ? "\n\n" + "Enrages target for " + ragePower + "% of their rage" : "");
        tip.Replace("{REVIVES}", (resurrectPower > 0) ? "\n\n" + "Resurrects target with " + resurrectPower + "% of their lifeforce" : "");
        /*//PLAYER
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
    tip.Replace("{HEALTHCOST}", (cost.life > 0) ? "\n" + "health cost " + cost.life.ToString() : "");
    tip.Replace("{MANACOST}", (cost.mana > 0) ? "\n" + "mana cost " + cost.mana.ToString() : "");
    tip.Replace("{BLOODCOST}", (cost.blood > 0) ? "\n" + "blood cost " + cost.blood.ToString() : "");
    tip.Replace("{SPIRITCOST}", (cost.spirit > 0) ? "\n" + "spirit cost " + cost.spirit.ToString() : "");
    tip.Replace("{STAMINACOST}", (cost.stamina > 0) ? "\n" + "stamina cost " + cost.stamina.ToString() : "");
    tip.Replace("{FURYCOST}", (cost.fury > 0) ? "\n" + "fury cost " + cost.fury.ToString() : "");
    tip.Replace("{EXPCOST}", (cost.experience > 0) ? "\n" + "exp cost " + cost.experience.ToString() : "");
    //PET
    tip.Replace("{PETHEALTHCOST}", (cost.petLife > 0) ? "\n" + "pet health cost " + cost.petLife.ToString() : "");
    tip.Replace("{PETMANACOST}", (cost.petMana > 0) ? "\n" + "pet mana cost " + cost.petMana.ToString() : "");
    tip.Replace("{PETBLOODCOST}", (cost.petBlood > 0) ? "\n" + "pet blood cost " + cost.petBlood.ToString() : "");
    tip.Replace("{PETSPIRITCOST}", (cost.petSpirit > 0) ? "\n" + "pet spirit cost " + cost.petSpirit.ToString() : "");
    tip.Replace("{PETSTAMINACOST}", (cost.petStamina > 0) ? "\n" + "pet stamina cost " + cost.petStamina.ToString() : "");
    tip.Replace("{PETFURYCOST}", (cost.petFury > 0) ? "\n" + "pet fury cost " + cost.petFury.ToString() : "");
    tip.Replace("{PETEXPCOST}", (cost.petExperience > 0) ? "\n" + "pet exp cost " + cost.petExperience.ToString() : "");
    */
        return tip.ToString();
    }
}
