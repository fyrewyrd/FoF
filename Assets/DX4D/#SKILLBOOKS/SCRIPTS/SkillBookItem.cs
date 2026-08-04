using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName="DX4D/ITEMS/Skill Book", order=999)]
public class SkillBookItem : UsableItem
{
    [Header(" [ SKILL TO LEARN ] ")]
    [SerializeField] ScriptableSkill _teachesSkill = null;
    public ScriptableSkill teachesSkill { get { return _teachesSkill; } }

    //public int usageHealth;
    //public int usageMana;
    //public int usageExperience;
    //public int usagePetHealth; // to heal pet

    // usage
    public override void Use(Player player, int inventoryIndex)
    {
        // always call base function too
        base.Use(player, inventoryIndex);


        OnUsed(player, inventoryIndex);
        // increase health/mana/etc.
        //player.health += usageHealth;
        //player.mana += usageMana;
        //player.experience += usageExperience;
        //if (player.activePet != null) player.activePet.health += usagePetHealth;

        // decrease amount
        ItemSlot slot = player.inventory[inventoryIndex];
        slot.DecreaseAmount(1);
        player.inventory[inventoryIndex] = slot;
    }
    public virtual void OnUsed(Player player, int inventoryIndex)
    {
        if (_teachesSkill) player.skills.Add(new Skill(_teachesSkill));

        if (skillSets.Count <= 0) return;

        foreach (ScriptableSkillSet skillset in skillSets)
        {
            foreach (Skill skill in skillset.skills)
            {
                player.skills.Add(skill);
            }
        }
    }

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());

                tip.Append("<b>" + "Skill Book" + "</b>");
        if (_teachesSkill)
        {
                tip.Append("\n <i>" + "Learn Skill: " + _teachesSkill.name + "</i>");
        }
        if (skillSets.Count > 0)
        {
            foreach (ScriptableSkillSet skillset in skillSets)
            {
                //tip.Append("/n<b>" + skillset.name + "</b>");

                foreach (Skill skill in skillset.skills)
                {
                    tip.Append("\n <i>" + "Learn Skill: " + skill.name + "</i>");
                    //tip.Append("/n   " + skill.name);
                }
            }
        }
        //tip.Replace("{USAGEHEALTH}", usageHealth.ToString());
        //tip.Replace("{USAGEMANA}", usageMana.ToString());
        //tip.Replace("{USAGEEXPERIENCE}", usageExperience.ToString());
        //tip.Replace("{USAGEPETHEALTH}", usagePetHealth.ToString());

        return tip.ToString();
    }
}
