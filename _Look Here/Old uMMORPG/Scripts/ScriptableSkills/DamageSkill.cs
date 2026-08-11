// Base type for damage skill templates.
// => there may be target damage, targetless damage, aoe damage, etc.
using System.Text;
using UnityEngine;

public abstract partial class DamageSkill : ActiveSkill
{
    [Header("Damage")]
    public LinearInt damage = new LinearInt{baseValue=1};
    public LinearFloat stunChance; // range [0,1]
    public LinearFloat stunTime; // in seconds

    // tooltip
    public string OldToolTip(int skillLevel, bool showRequirements = false)
    {
        StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));
        tip.Replace("{DAMAGE}", damage.Get(skillLevel).ToString());
        tip.Replace("{STUNCHANCE}", Mathf.RoundToInt(stunChance.Get(skillLevel) * 100).ToString());
        tip.Replace("{STUNTIME}", stunTime.Get(skillLevel).ToString("F1"));
        return tip.ToString();
    }
}
