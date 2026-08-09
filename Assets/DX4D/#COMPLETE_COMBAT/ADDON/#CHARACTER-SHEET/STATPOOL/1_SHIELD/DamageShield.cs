using Mirror;
using System.Linq;
using UnityEngine;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ DAMAGE SHIELD ] ")]
    [SerializeField] protected LinearInt _shieldMax = new LinearInt { baseValue = 0 };
    public LinearInt shieldMax { get { return _shieldMax; } }
    [SyncVar, SerializeField] int _shield = 0;
    internal int shield { get { return _shield; } set { _shield = value; } }
    [SyncVar, SerializeField, HideInInspector] double _shieldBreakTime = 0;
    internal double shieldBreakTime { get { return _shieldBreakTime; } set { _shieldBreakTime = value; } }
    [Tooltip("When your shield reaches 0 it breaks...\nThis is the amount of time from that point before it can regenerate again.")]
    public float shieldBreakDuration = 30f;

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowShieldThreshold = 0.1f; // 1.0f == 100%

    [SerializeField] [EnumButtons] public MethodOfDamage shieldAbsorbs = MethodOfDamage.Physical | MethodOfDamage.Magic;
    
    public bool shieldLossKillsMe = false;
    public int shieldGainRate = 1;
    public int shieldLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger shieldGainTriggers =
        PassiveTrigger.NotMoving | PassiveTrigger.Moving;
    [SerializeField] [EnumButtons] public PassiveTrigger shieldLossTriggers =
        PassiveTrigger.None;
        //PassiveTrigger.OnPhysicalDamageTaken | PassiveTrigger.OnMagicDamageTaken;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int SHIELDMAX
    {
        get
        {
            // base + buffs
            int buffBonus = BUFFS.Sum(buff => buff.buffsShieldMax);
            return stats.shieldMax.Get(level) + buffBonus;
        }
    }
    // C U R R E N T
    public int SHIELD
    {
        get { return Mathf.Min(stats.shield, SHIELDMAX); }
        set { stats.shield = Mathf.Clamp(value, 0, SHIELDMAX); if (ShieldIsBroken()) stats.shield = 0; }
    }
    // P E R C E N T
    public float SHIELDFILLED()
    {
        return (SHIELD > 0 && SHIELDMAX > 0) ? (float)SHIELD / (float)SHIELDMAX : 0;
    }
    // L O W
    public bool SHIELDISLOW { get { return (SHIELDFILLED() <= stats.lowShieldThreshold); } }

    // B R E A K  D U R A T I O N
    public float SHIELDBREAKDURATION
    {
        get { return stats.shieldBreakDuration; }
        set { stats.shieldBreakDuration = value; }
    }
    // B R E A K  T I M E
    public double SHIELDBREAKTIME
    {
        get { return stats.shieldBreakTime; }
        set { stats.shieldBreakTime = value; }
    }
    // A C T I V E
    //public bool ShieldIsActive()
    //{
    //    return (SHIELDMAX > 0);
    //}
    // B R O K E N
    public bool ShieldIsBroken()
    {
        return (SHIELDBREAKTIME > 0 && NetworkTime.time <= SHIELDBREAKTIME);
    }
    public void BreakShield()
    {
        SHIELDBREAKTIME = NetworkTime.time + SHIELDBREAKDURATION;
        SHIELD = 0;
        RpcShowTextPopup("<b><color=orange>**Shield broke**</color></b>");
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int buffsShieldMax { get { return data.buffsShieldMax.Get(level); } }
    public float buffsShieldRegenRate { get { return data.buffsShieldRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsShieldMax;
    public LinearFloat buffsShieldRegenRate; // 0.1=10%; can be negative too
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
