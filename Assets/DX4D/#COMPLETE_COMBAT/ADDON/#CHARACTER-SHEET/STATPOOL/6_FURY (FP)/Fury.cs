using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ FURY POOL ] ")]
    [SerializeField] protected LinearInt _furyMax = new LinearInt { baseValue = 100 };
    public LinearInt furyMax { get { return _furyMax; } }
    [SyncVar, SerializeField] int _fury = 0;
    internal int fury { get { return _fury; } set { _fury = value; } }

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowFuryThreshold = 0.1f; // 1.0f == 100%
    
    public bool furyLossKillsMe = false;
    public int furyGainRate = 5;
    public int furyLossRate = 1;

    [SerializeField] [EnumButtons]  public PassiveTrigger furyGainTriggers =
        PassiveTrigger.DamageDealt |
        PassiveTrigger.DamageTaken |
        PassiveTrigger.CriticalHit | PassiveTrigger.Backstab | PassiveTrigger.BackstabCrit;
    [SerializeField] [EnumButtons] public PassiveTrigger furyLossTriggers =
        PassiveTrigger.NotMoving |
        PassiveTrigger.Moving |
        PassiveTrigger.MyAttackBlocked |
        PassiveTrigger.MyAttackDodged;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int FURYMAX
    {
        get
        {
            if (Player.localPlayer != null)
            {
                // sum up manually. Linq.Sum() is HEAVY(!) on GC and performance (190 KB/call!)
                int passiveBonus = 0;
                foreach (Skill skill in entity.skills)
                    if (skill.level > 0 && skill.data is PassiveSkill passiveSkill)
                        passiveBonus += passiveSkill.furyMaxBonus.Get(skill.level);

                int buffBonus = 0;
                for (int i = 0; i < entity.buffs.Count; ++i)
                    buffBonus += entity.buffs[i].buffsFuryMax;

                // base + passives + buffs
                return stats.furyMax.Get(level) + passiveBonus + buffBonus;
            }
            else
            {
                int buffBonus = BUFFS.Sum(buff => buff.buffsFuryMax);
                return stats.furyMax.Get(level) + buffBonus;
            }
        }
    }
    // C U R R E N T
    public int FURY
    {
        get { return Mathf.Min(stats.fury, FURYMAX); }
        set { stats.fury = Mathf.Clamp(value, 0, FURYMAX); }
    }
    // P E R C E N T
    public float FURYFILLED()
    {
        return (FURY != 0 && FURYMAX != 0) ? (float)FURY / (float)FURYMAX : 0;
    }
    // L O W
    public bool FURYISLOW { get { return (FURYFILLED() <= stats.lowFuryThreshold); } }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int buffsFuryMax { get { return data.buffsFuryMax.Get(level); } }
    public float buffsFuryRegenRate { get { return data.buffsFuryRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsFuryMax;
    public LinearFloat buffsFuryRegenRate; // 0.1=10%; can be negative too
}

public abstract partial class BonusSkill : ScriptableSkill
{
    [FormerlySerializedAs("bonusFuryMax")] public LinearInt furyMaxBonus;
}