//#define RPG2D //NOTE: Enable this define for 2D support...or import the 2D_MODE unity package included with this asset

using Mirror;
using System.Text;
//using System.Collections.Generic;
using UnityEngine;

public abstract partial class DamageSkill : ActiveSkill
{
    // T O O L T I P
    public override string ToolTip(int level, bool showRequirements = false)
    {
        StringBuilder tip = new StringBuilder(base.ToolTip(level, showRequirements));

        //tip.Append("\n{CASTINFO}"); //NOTE: Already in BASE class
        tip.Append("{DAMAGE}");

        //tip.Replace("{DAMAGEINFO}", (((damage.Get(level) + totalAddedDamage + skillDamage.total) != 0) ? ("{FINALDAMAGE}" + "" + "{DAMAGETYPE}" + "" + "{ELEMENT}" + " Damage" + "") : ""));
        //CASTINFO - fallback
        tip.Replace("{CASTINFO}",
            "{CASTINGCOST}"
            + "\n" + "Range: {CASTRANGE}" 
            + "\n" + "Cast Time: {CASTTIME}s - Cooldown: {COOLDOWN}s");

        //DAMAGE
        if (skillDamage.max > 0)
        {
            tip.Replace("{DAMAGE}", "\n" + skillDamage.ToString());

            if (skillDamage.fixedDamage)
            {
                tip.Replace("{DAMAGE}","\n" + skillDamage.max + "{DAMAGEBONUS}" + "{DAMAGEMETHOD}" + "{DAMAGEELEMENT}" + " damage");
            }
            else
            {
                tip.Replace("{DAMAGE}", "\n" + skillDamage.min + " - " + skillDamage.max
                    + "{DAMAGEBONUS}"
                    + "{DAMAGEMETHOD}" + "{DAMAGEELEMENT}"
                + " damage");
            }
        }
        else
        {
            tip.Replace("{DAMAGE}", "");
        }

        //DAMAGE BONUS
        if (skillDamage.bonus > 0)
        {
            tip.Replace("{DAMAGEBONUS}", " + " + skillDamage.bonus);
        }
        else
        {
            tip.Replace("{DAMAGEBONUS}", "");
        }

        //DAMAGE PROPERTIES
        tip.Replace("{DAMAGEMETHOD}", (skillDamage.method != MethodOfDamage.NoDamage) ? (" " + skillDamage.method.ToString() + "") : "");
        tip.Replace("{DAMAGEELEMENT}", (skillDamage.element != Element.Neutral) ? (" " + skillDamage.element.ToString() + "") : "");

        //tip.Replace("{DAMAGE}", (damage.Get(level) > 0) ? (damage.Get(level).ToString()) : "");
        //tip.Replace("{DAMAGE}", (skillDamage.max > 0) ? "{DAMAGERANGE}" : "");
        //tip.Replace("{DAMAGERANGE}", (skillDamage.fixedDamage) ? skillDamage.max.ToString() : (skillDamage.min + "-" + skillDamage.max) );

        //tip.Replace("{INITIALDAMAGE}", (skillDamage.total != 0) ? ("Initial Damage " + skillDamage.total.ToString() + "") : "");
        if (skillDamage.damageOverTime.Count > 0)
        {
            tip.Append("\n\n<color=white><b>[damage over time]</b></color>");
            foreach (ScriptedDamage dmg in skillDamage.damageOverTime)
            {
                tip.Append( "\n" + dmg.ToString());
            }
        }
        //tip.Replace("{DOT}", (totalAddedDamage != 0) ? ("Damage Over Time " + totalAddedDamage.ToString() + "") : "");
        //tip.Replace("{FINALDAMAGE}", ((damage.Get(level) + totalAddedDamage + skillDamage.total) != 0) ? ((damage.Get(level) + totalAddedDamage + skillDamage.total).ToString()) : "");

        //STUN
        tip.Replace("{STUNCHANCE}", Mathf.RoundToInt(stunChance.Get(level) * 100).ToString());
        tip.Replace("{STUNTIME}", stunTime.Get(level).ToString("F1"));

        //COSTS TO CAST
        tip.Replace("{CASTINGCOST}", Tooltip.CastingCostToolTip(costs));
        //tip.Replace("{CASTINGCOST}", "{LIFECOSTS}{MANACOSTS}{BLOODCOSTS}{SPIRITCOSTS}{FURYCOSTS}{STAMINACOSTS}");
        //tip.Replace("{LIFECOSTS}", (costs.me.life != 0) ? ("   " + costs.me.life.ToString() + " LIFE") : "");
        //tip.Replace("{MANACOSTS}", (costs.me.mana != 0) ? ("   " + costs.me.mana.ToString() + " MANA") : "");
        //
        //tip.Replace("{BLOODCOSTS}", (costs.me.blood != 0) ? ("   " + costs.me.blood.ToString() + " BLOOD") : "");
        //tip.Replace("{SPIRITCOSTS}", (costs.me.spirit != 0) ? ("   " + costs.me.spirit.ToString() + " SPIRIT") : "");
        //
        //tip.Replace("{FURYCOSTS}", (costs.me.fury != 0) ? ("   " + costs.me.fury.ToString() + " FURY") : "");
        //tip.Replace("{STAMINACOSTS}", (costs.me.stamina != 0) ? ("   " + costs.me.stamina.ToString() + " STAMINA") : "");

        //CAST CONDITIONS
        tip.Replace("{CASTTIME}", (castTime.Get(level) != 0) ? (castTime.Get(level).ToString()) : "");
        tip.Replace("{COOLDOWN}", (cooldown.Get(level) > 0) ? (cooldown.Get(level).ToString()) : "");
        tip.Replace("{CASTRANGE}", (castRange.Get(level) > 0) ? castRange.Get(level).ToString() : "");

        return tip.ToString();
    }
}
