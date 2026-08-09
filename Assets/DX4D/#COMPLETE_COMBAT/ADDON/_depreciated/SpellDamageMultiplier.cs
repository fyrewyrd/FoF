/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("MAGIC STATS")]
    
    // D A M A G E  M U L T I P L I E R S
    [SerializeField]
    protected LinearFloat _spellDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float spellDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsSpellDamageMultiplier);
            return _spellDamageMultiplier.Get(level) + buffBonus;
        }
        set { _spellDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    // D A M A G E  B O N U S
    [SerializeField]
    protected LinearInt _magicDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int magicDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsMagicDamageBonus);
            return _magicDamageBonus.Get(level) + buffBonus;
        }
        set { _magicDamageBonus = new LinearInt { baseValue = value }; }
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    // D A M A G E  M O D I F I E R
    public float buffsSpellDamageMultiplier { get { return data.buffsSpellDamageMultiplier.Get(level); } }
    //public int buffsMagicDamageBonus { get { return data.buffsMagicDamageBonus.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // D A M A G E  M O D I F I E R
    public LinearFloat buffsSpellDamageMultiplier;

    // D A M A G E  B O N U S
    //public LinearInt buffsMagicDamageBonus;
}
*/
/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // o f f e n s i v e
    tip.Replace("{BUFFSMAGICDAMAGEMODIFIER}", buffsMagicDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDAMAGEMODIFIER}", buffsElementalDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDAMAGEMODIFIER}", buffsBloodDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDAMAGEMODIFIER}", buffsSpiritDamageMultiplier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // d e f e n s i v e
    tip.Replace("{BUFFSMAGICDEFENSEMODIFIER}", buffsMagicDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDEFENSEMODIFIER}", buffsElementalDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDEFENSEMODIFIER}", buffsBloodDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDEFENSEMODIFIER}", buffsSpiritDamageReductionModifier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/
