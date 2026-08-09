using System.Text;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName="DX4D/ITEM/Skill Item", order=1)]
public class SkillItem : UsableItem
{
    [Header("Attached Skill")]
    [SyncVar] public ScriptableSkill attachedSkill;

    [Header("Usage Costs")]
    [SerializeField] public CastingCost costs;
    [SerializeField] public bool consumeOnUse = true;

    [Header("Automated Tooltips")]
    [SerializeField] public bool automaticTooltip = true;
    [SerializeField, TextArea(1, 30)] public string description;

    // usage
    public override void Use(Player player, int inventoryIndex)
    {
        Use(player, inventoryIndex);
    }
    public void Use(PlayerCharacter player, int inventoryIndex)
    {
        if (inventoryIndex < 0)
        {
            player.TargetShowTextPopup("[you have no "+ name.ToLower() +"]");
            return;
        }

        if (attachedSkill != null)
        {
            player.RpcShowTextPopup(attachedSkill.name.ToUpper() + "!!!");

            int skillNumber = player.GetSkillIndexByName(attachedSkill.name);
#if UNITY_EDITOR
            Debug.Log("ITEM TRIGGERED SKILL NUMBER " + skillNumber.ToString()); //DEBUG
#endif

            if (skillNumber > -1)
            {
                player.currentSkill = skillNumber; //NOTE: One of these is probably redundant
            }
        }

        if (consumeOnUse)
        {
            ItemSlot inventorySlot = player.INVENTORY[inventoryIndex];
            inventorySlot.DecreaseAmount(1);
            player.INVENTORY[inventoryIndex] = inventorySlot;
        }
    }
    
    public virtual void OnUsed(PlayerCharacter player)
    {
        int skillNumber = -1;
        skillNumber = player.GetSkillIndexByName(attachedSkill.name);
        if (skillNumber > -1)
        {
            //USE SKILL
            player.currentSkill = skillNumber; //NOTE: One of these is probably redundant

            //APPLY COSTS
            player.PayCost(costs.me);
            player.PayCost(costs.pet);
            player.PayCost(costs.mount);
            //if (cost.life > 0) player.health -= cost.life;
            //if (cost.mana > 0) player.mana -= cost.mana;
            //if (cost.blood > 0) player.blood -= cost.blood;
            //if (cost.spirit > 0) player.spirit -= cost.spirit;
            //if (cost.stamina > 0) player.stamina -= cost.stamina;
            //if (cost.fury > 0) player.fury -= cost.fury;
            //if (cost.experience > 0) player.experience -= cost.experience;

            //if (player.activePet != null)
            //{
            //    if (allyCost.petLife > 0) player.activePet.health -= cost.petLife;
            //    if (cost.petMana > 0) player.activePet.mana -= cost.petMana;
            //    if (cost.petBlood > 0) player.activePet.blood -= cost.petBlood;
            //    if (cost.petSpirit > 0) player.activePet.spirit -= cost.petSpirit;
            //    if (cost.petStamina > 0) player.activePet.stamina -= cost.petStamina;
            //    if (cost.petFury > 0) player.activePet.fury -= cost.petFury;
            //    if (cost.petExperience > 0) player.activePet.experience -= cost.petExperience;
            //}
        }
        else
        {
            player.TargetShowTextPopup("*fizzled*");
#if UNITY_EDITOR
            Debug.LogError(player.name + " can not use " + name + "...you must add it to the Skill Templates of your Player Prefab for this item to work."); //DEBUG
#endif
        }
    }

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());

        if (automaticTooltip)
        {
            tip.Append("<b>{NAME}</b>");
            tip.Append("<b>{DESCRIPTION}</b>");
            tip.Append("{USESKILL}");
            tip.Append("{CASTINGCOST}");
            if (attachedSkill != null)
            {
                if (attachedSkill is ScriptableDamageSkill)
                {
                    ScriptedDamage[] addedDamage = (attachedSkill as ScriptableDamageSkill).skillDamage.damageOverTime.ToArray();
                    for (int i = 0; i < addedDamage.Length; i++)
                    {
                        tip.Append("\n"
                            + ((addedDamage[i].damage.fixedDamage) ? ("") : (addedDamage[i].damage.min + " - "))
                            + addedDamage[i].damage.total
                            + " " + addedDamage[i].damage.method
                            + " " + addedDamage[i].damage.element
                            + " damage"
                            );
                    }
                }
            }
        }
        tip.Replace("{NAME}", name);
        tip.Replace("{DESCRIPTION}", (description != string.Empty) ? "\n" + description : "");
        tip.Replace("{USESKILL}", (attachedSkill != null) ? "\n<b><i>Casts Skill</i></b> - " + attachedSkill.name : "");
        tip.Replace("{CASTINGCOST}", Tooltip.CastingCostToolTip(costs));
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
