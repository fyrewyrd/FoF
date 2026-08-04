/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("CAST RANGE")]
    // C A S T  S P E E D  M U L T I P L I E R
    [SerializeField] protected LinearFloat _castRangeMultiplier = new LinearFloat { baseValue = 1.0f };
    [SyncVar] private float castRange = 1.0f;
    public virtual float castRangeMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsCastRangeMultiplier);
            return (int)((_castRangeMultiplier.Get(level) + buffBonus) * castRange);
        }
        set
        {
            castRange = Mathf.Clamp(value, 0.0f, 20.0f);
        }
    }

    [Command] public void CmdSetCastRange(float newRangeMultiplier)
    {
        castRange = newRangeMultiplier;
    }

    //[Server] public void SetCastSpeedMultiplier(float amount)
    //{
    //    castSpeed = Mathf.Clamp(amount, 0.0f, 20.0f);
    //}
}

// - - - - -
// B U F F S
public partial struct Buff
{
    // S P E E D  M U L T I P L I E R
    public float buffsCastRangeMultiplier { get { return data.buffsCastRangeMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // S P E E D  M U L T I P L I E R 
    public LinearFloat buffsCastRangeMultiplier;
}
*/
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
