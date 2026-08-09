using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ SPIRIT POOL ] ")]
    [SerializeField] protected LinearInt _spiritMax = new LinearInt { baseValue = 100 };
    public LinearInt spiritMax { get { return _spiritMax; } }
    [SyncVar, SerializeField] int _spirit = 100;
    internal int spirit { get { return _spirit; } set { _spirit = value; } }

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowSpiritThreshold = 0.1f; // 1.0f == 100%
    
    public bool spiritLossKillsMe = true;
    public int spiritGainRate = 1;
    public int spiritLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger spiritGainTriggers =
        PassiveTrigger.NonCombat;
    [SerializeField] [EnumButtons] public PassiveTrigger spiritLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int SPIRITMAX
    {
        get
        {
            if (Player.localPlayer != null)
            {
                // sum up manually. Linq.Sum() is HEAVY(!) on GC and performance (190 KB/call!)
                int passiveBonus = 0;
                foreach (Skill skill in entity.skills)
                    if (skill.level > 0 && skill.data is PassiveSkill passiveSkill)
                        passiveBonus += passiveSkill.spiritMaxBonus.Get(skill.level);

                int buffBonus = 0;
                for (int i = 0; i < entity.buffs.Count; ++i)
                    buffBonus += entity.buffs[i].buffsSpiritMax;

                // base + passives + buffs
                return stats.spiritMax.Get(level) + passiveBonus + buffBonus;
            }
            else
            {
                int buffBonus = BUFFS.Sum(buff => buff.buffsSpiritMax);
                return stats.spiritMax.Get(level) + buffBonus;
            }
        }
    }
    // C U R R E N T
    public int SPIRIT
    {
        get { return Mathf.Min(stats.spirit, SPIRITMAX); }
        set { stats.spirit = Mathf.Clamp(value, 0, SPIRITMAX); }
    }

    // P E R C E N T
    public float SPIRITFILLED()
    {
        return (SPIRIT != 0 && SPIRITMAX != 0) ? (float)SPIRIT / (float)SPIRITMAX : 0;
    }
    // L O W
    public bool SPIRITISLOW { get { return (SPIRITFILLED() <= stats.lowSpiritThreshold); } }
}

// BASE
public partial struct Buff
{
    // S P I R I T
    public int buffsSpiritMax { get { return data.buffsSpiritMax.Get(level); } }
    public float buffsSpiritRegenRate { get { return data.buffsSpiritRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // S P I R I T
    public LinearInt buffsSpiritMax;
    public LinearFloat buffsSpiritRegenRate; // 0.1=10%; can be negative too
}

public abstract partial class BonusSkill : ScriptableSkill
{
    [FormerlySerializedAs("bonusSpiritMax")] public LinearInt spiritMaxBonus;
}

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

    // s p i r i t
    tip.Replace("{BUFFSSPIRITMAX}", buffsSpiritMax.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITPERCENTPERSECOND}", Mathf.RoundToInt(buffsSpiritPercentPerSecond.Get(skillLevel) * 100).ToString());
    return tip.ToString();
}
*/

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // s p i r i t
    tip.Replace("{BUFFSSPIRITMAX}", buffsSpiritMax.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITPERCENTPERSECOND}", Mathf.RoundToInt(buffsSpiritPercentPerSecond.Get(skillLevel) * 100).ToString());
    return tip.ToString();
}
*/
