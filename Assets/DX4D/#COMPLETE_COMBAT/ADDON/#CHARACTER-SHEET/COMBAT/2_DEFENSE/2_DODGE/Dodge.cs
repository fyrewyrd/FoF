using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("DODGE")]

    //DODGE PHYSICAL DAMAGE
    // D O D G E  P H Y S I C A L  C H A N C E
    [SerializeField]
    protected LinearInt _dodgePhysicalChance = new LinearInt { baseValue = 5 };
    public virtual int dodgePhysicalChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsDodgePhysicalChance);
            return _dodgePhysicalChance.Get(level) + buffBonus;
        }
    }

    // D O D G E  P H Y S I C A L  D A M A G E  M U L T I P L I E R
    [SerializeField]
    protected LinearFloat _dodgePhysicalDamageMultiplier = new LinearFloat { baseValue = 0.0f };
    public virtual float dodgePhysicalDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsDodgePhysicalDamageMultiplier);
            return _dodgePhysicalDamageMultiplier.Get(level) + buffBonus;
        }
    }

    //DODGE SPELL DAMAGE
    // D O D G E  S P E L L  C H A N C E
    [SerializeField]
    protected LinearInt _dodgeSpellChance = new LinearInt { baseValue = 5 };
    public virtual int dodgeSpellChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsDodgeSpellChance);
            return _dodgeSpellChance.Get(level) + buffBonus;
        }
    }

    // D O D G E  S P E L L  D A M A G E  M U L T I P L I E R
    [SerializeField]
    protected LinearFloat _dodgeSpellDamageMultiplier = new LinearFloat { baseValue = 0.0f };
    public virtual float dodgeSpellDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsDodgeSpellDamageMultiplier);
            return _dodgeSpellDamageMultiplier.Get(level) + buffBonus;
        }
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    // C H A N C E
    public int buffsDodgePhysicalChance { get { return data.buffsDodgePhysicalChance.Get(level); } }
    public int buffsDodgeSpellChance { get { return data.buffsDodgeSpellChance.Get(level); } }
    // D A M A G E  M U L T I P L I E R
    public float buffsDodgePhysicalDamageMultiplier { get { return data.buffsDodgePhysicalDamageMultiplier.Get(level); } }
    public float buffsDodgeSpellDamageMultiplier { get { return data.buffsDodgeSpellDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // C H A N C E
    public LinearInt buffsDodgePhysicalChance;
    public LinearInt buffsDodgeSpellChance;
    // D A M A G E  M U L T I P L I E R
    public LinearFloat buffsDodgePhysicalDamageMultiplier;
    public LinearFloat buffsDodgeSpellDamageMultiplier;
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
