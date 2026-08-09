using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable] public partial class GainAndLossTriggers
{
}

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ BLOOD POOL ] ")]
    [SerializeField] protected LinearInt _bloodMax = new LinearInt { baseValue = 100 };
    public LinearInt bloodMax { get { return _bloodMax; } }
    [SyncVar, SerializeField] int _blood = 100;
    internal int blood { get { return _blood; } set { _blood = value; } }

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowBloodThreshold = 0.1f; // 1.0f == 100%
    
    public bool bloodLossKillsMe = true;
    public int bloodGainRate = 1;
    public int bloodLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger bloodGainTriggers =
        PassiveTrigger.NonCombat;
    [SerializeField] [EnumButtons] public PassiveTrigger bloodLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int BLOODMAX
    {
        get
        {
            if (Player.localPlayer != null)
            {
                // sum up manually. Linq.Sum() is HEAVY(!) on GC and performance (190 KB/call!)
                int passiveBonus = 0;
                foreach (Skill skill in entity.skills)
                    if (skill.level > 0 && skill.data is PassiveSkill passiveSkill)
                        passiveBonus += passiveSkill.bloodMaxBonus.Get(skill.level);

                int buffBonus = 0;
                for (int i = 0; i < entity.buffs.Count; ++i)
                    buffBonus += entity.buffs[i].buffsBloodMax;

                // base + passives + buffs
                return stats.bloodMax.Get(level) + passiveBonus + buffBonus;
            }
            else
            {
                int buffBonus = BUFFS.Sum(buff => buff.buffsBloodMax);
                return stats.bloodMax.Get(level) + buffBonus;
            }
        }
    }
    // C U R R E N T
    public int BLOOD
    {
        get { return Mathf.Min(stats.blood, BLOODMAX); }
        set { stats.blood = Mathf.Clamp(value, 0, BLOODMAX); }
    }
    // P E R C E N T
    public float BLOODFILLED()
    {
        return (BLOOD != 0 && BLOODMAX != 0) ? (float)BLOOD / (float)BLOODMAX : 0;
    }
    // L O W
    public bool BLOODLOW { get { return (BLOODFILLED() <= stats.lowBloodThreshold); } }

    /*public virtual int bloodRegenRate
    {
        get
        {
            float buffPercent = buffs.Sum(buff => buff.buffsBloodRegenRate);
            return stats.bloodRegenRate.Get(level) + Convert.ToInt32(buffPercent * bloodMax);
        }
    }*/
}

// - - - - -
// B U F F S
// BASE
public partial struct Buff
{
    public int buffsBloodMax { get { return data.buffsBloodMax.Get(level); } }
    public float buffsBloodRegenRate { get { return data.buffsBloodRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsBloodMax;
    public LinearFloat buffsBloodRegenRate; // 0.1=10%; can be negative too
}

public abstract partial class BonusSkill : ScriptableSkill
{
    [FormerlySerializedAs("bonusBloodMax")] public LinearInt bloodMaxBonus;
}

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // b l o o d
    tip.Replace("{BUFFSBLOODMAX}", buffsBloodMax.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODPERCENTPERSECOND}", Mathf.RoundToInt(buffsBloodPercentPerSecond.Get(skillLevel) * 100).ToString());

    return tip.ToString();
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

    // b l o o d
    tip.Replace("{BUFFSBLOODMAX}", buffsBloodMax.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODPERCENTPERSECOND}", Mathf.RoundToInt(buffsBloodPercentPerSecond.Get(skillLevel) * 100).ToString());

    return tip.ToString();
}
*/
