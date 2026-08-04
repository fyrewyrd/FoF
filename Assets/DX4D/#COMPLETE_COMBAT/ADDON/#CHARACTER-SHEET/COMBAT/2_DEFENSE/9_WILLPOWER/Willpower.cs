using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    // W I L L P O W E R
    [Header("WILLPOWER")]
    [SerializeField][Tooltip("Willpower gives a flat base damage reduction vs all non-physical damage methods.")]
    protected LinearInt _willpower = new LinearInt { baseValue = 0 };
    public virtual int willpower
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsWillpower);
            return _willpower.Get(level) + buffBonus;
        }
    }

    // W I L L P O W E R  M U L T I P L I E R
    //[SerializeField]
    //protected LinearFloat _willpowerMultiplier = new LinearFloat { baseValue = 1.0f };
    //public virtual float willpowerMultiplier
    //{
    //    get
    //    {
    //        float buffBonus = buffs.Sum(buff => buff.buffsWillpowerMultiplier);
    //        return _willpowerMultiplier.Get(level) + buffBonus;
    //    }
    //}
}

// - - - - -
// B U F F S
public partial struct Buff
{
    // F L A T
    public int buffsWillpower { get { return data.buffsWillpower.Get(level); } }
    // M U L T I P L I E R
    //public float buffsWillpowerMultiplier { get { return data.buffsWillpowerMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // F L A T
    public LinearInt buffsWillpower;
    // M U L T I P L I E R
    //public LinearFloat buffsWillpowerMultiplier;
}

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));
    // e v a s i o n
    tip.Replace("{BUFFSDODGECHANCE}", buffsDodgeChance.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPELLDODGECHANCE}", buffsSpellDodgeChance.Get(skillLevel).ToString());

    // o f f e n s i v e
    tip.Replace("{BUFFSMAGICDAMAGEMODIFIER}", buffsMagicDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDAMAGEMODIFIER}", buffsElementalDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDAMAGEMODIFIER}", buffsBloodDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDAMAGEMODIFIER}", buffsSpiritDamageModifier.Get(skillLevel).ToString());

    // d e f e n s i v e
    tip.Replace("{BUFFSMAGICDEFENSEMODIFIER}", buffsMagicDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDEFENSEMODIFIER}", buffsElementalDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDEFENSEMODIFIER}", buffsBloodDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDEFENSEMODIFIER}", buffsSpiritDefenseModifier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/
