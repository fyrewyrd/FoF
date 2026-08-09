using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("BLOCK")] //All block logic already in Entity.cs has been depreciated

    //BLOCK PHYSICAL DAMAGE
    // B L O C K  P H Y S I C A L  C H A N C E
    [SerializeField]
    protected LinearInt _blockPhysicalChance = new LinearInt { baseValue = 5 };
    public virtual int blockPhysicalChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBlockPhysicalChance);
            return _blockPhysicalChance.Get(level) + buffBonus;
        }
    }

    // B L O C K  P H Y S I C A L  D A M A G E  M U L T I P L I E R
    [SerializeField]
    protected LinearFloat _blockPhysicalDamageMultiplier = new LinearFloat { baseValue = 0.5f };
    public virtual float blockPhysicalDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBlockPhysicalDamageMultiplier);
            return _blockPhysicalDamageMultiplier.Get(level) + buffBonus;
        }
    }

    //BLOCK SPELL DAMAGE
    // B L O C K  S P E L L  C H A N C E
    [SerializeField]
    protected LinearInt _blockSpellChance = new LinearInt { baseValue = 5 };
    public virtual int blockSpellChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBlockSpellChance);
            return _blockSpellChance.Get(level) + buffBonus;
        }
    }

    // B L O C K  S P E L L  D A M A G E  M U L T I P L I E R
    [SerializeField]
    protected LinearFloat _blockSpellDamageMultiplier = new LinearFloat { baseValue = 0.5f };
    public virtual float blockSpellDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBlockSpellDamageMultiplier);
            return _blockSpellDamageMultiplier.Get(level) + buffBonus;
        }
    }

}

// - - - - -
// B U F F S
public partial struct Buff
{
    // C H A N C E
    //buffsBlockChance [depreciated] - Already in Entity.cs
    public int buffsBlockPhysicalChance { get { return data.buffsBlockPhysicalChance.Get(level); } }
    public int buffsBlockSpellChance { get { return data.buffsBlockSpellChance.Get(level); } }

    // D A M A G E  M U L T I P L I E R
    public float buffsBlockPhysicalDamageMultiplier { get { return data.buffsBlockPhysicalDamageMultiplier.Get(level); } }
    public float buffsBlockSpellDamageMultiplier { get { return data.buffsBlockSpellDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // C H A N C E
    //buffsBlockChance [depreciated] - Already in Entity.cs
    public LinearInt buffsBlockPhysicalChance;
    public LinearInt buffsBlockSpellChance;

    // D A M A G E  M U L T I P L I E R
    public LinearFloat buffsBlockPhysicalDamageMultiplier;
    public LinearFloat buffsBlockSpellDamageMultiplier;
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
