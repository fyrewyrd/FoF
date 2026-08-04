using Mirror;
using System.Linq;
using UnityEngine;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ DAMAGE BARRIER ] ")]
    [SerializeField] protected LinearInt _barrierMax = new LinearInt { baseValue = 0 };
    public LinearInt barrierMax { get { return _barrierMax; } }
    [SyncVar, SerializeField] int _barrier = 0;
    internal int barrier { get { return _barrier; } set { _barrier = value; } }
    [SyncVar, SerializeField, HideInInspector] double _barrierBreakTime = 0;
    internal double barrierBreakTime { get { return _barrierBreakTime; } set { _barrierBreakTime = value; } }
    [Tooltip("When your Barrier reaches 0 it breaks...\nThis is the amount of time from that point before it can regenerate again.")]
    public float barrierBreakDuration = 30f;

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowBarrierThreshold = 0.1f; // 1.0f == 100%

    [SerializeField] [EnumButtons] public MethodOfDamage barrierAbsorbs = MethodOfDamage.Physical | MethodOfDamage.Magic;
    
    public bool barrierLossKillsMe = false;
    public int barrierGainRate = 1;
    public int barrierLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger barrierGainTriggers =
        PassiveTrigger.NotMoving | PassiveTrigger.Moving;
    [SerializeField] [EnumButtons] public PassiveTrigger barrierLossTriggers =
        PassiveTrigger.None;
        //PassiveTrigger.OnPhysicalDamageTaken | PassiveTrigger.OnMagicDamageTaken;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int BARRIERMAX
    {
        get
        {
            // base + buffs
            int buffBonus = BUFFS.Sum(buff => buff.buffsBarrierMax);
            return stats.barrierMax.Get(level) + buffBonus;
        }
    }
    // C U R R E N T
    public int BARRIER
    {
        get { return Mathf.Min(stats.barrier, BARRIERMAX); }
        set { stats.barrier = Mathf.Clamp(value, 0, BARRIERMAX); if (BarrierIsBroken()) stats.barrier = 0; }
    }
    // P E R C E N T
    public float BARRIERFILLED()
    {
        return (BARRIER > 0 && BARRIERMAX > 0) ? (float)BARRIER / (float)BARRIERMAX : 0;
    }
    // L O W
    public bool BARRIERISLOW { get { return (BARRIERFILLED() <= stats.lowBarrierThreshold); } }

    // B R E A K  D U R A T I O N
    public float BARRIERBREAKDURATION
    {
        get { return stats.barrierBreakDuration; }
        set { stats.barrierBreakDuration = value; }
    }
    // B R E A K  T I M E
    public double BARRIERBREAKTIME
    {
        get { return stats.barrierBreakTime; }
        set { stats.barrierBreakTime = value; }
    }
    // A C T I V E
    //public bool BarrierIsActive()
    //{
    //    return (BarrierMAX > 0);
    //}
    // B R O K E N
    public bool BarrierIsBroken()
    {
        return (BARRIERBREAKTIME > 0 && NetworkTime.time <= BARRIERBREAKTIME);
    }
    public void BreakBarrier()
    {
        BARRIERBREAKTIME = NetworkTime.time + BARRIERBREAKDURATION;
        BARRIER = 0;
        RpcShowTextPopup("<b><color=orange>**Barrier broke**</color></b>");
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int buffsBarrierMax { get { return data.buffsBarrierMax.Get(level); } }
    public float buffsBarrierRegenRate { get { return data.buffsBarrierRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsBarrierMax;
    public LinearFloat buffsBarrierRegenRate; // 0.1=10%; can be negative too
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
