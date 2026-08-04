//#define ummorpg //Toggle for ummorpg integration
using Mirror;
using System.Linq;
using UnityEngine;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ LIFE POOL ] ")]
    [SerializeField] protected LinearInt _lifeMax = new LinearInt { baseValue = 100 };
    public LinearInt lifeMax { get { return _lifeMax; } }
    [SyncVar, SerializeField] int _life = 100;
    internal int life { get { return _life; } set { _life = value; } }

    [Tooltip("The percentage where this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowLifeThreshold = 0.1f; // 1.0f == 100%
    
    public bool lifeLossKillsMe = true;
    public int lifeGainRate = 1;
    public int lifeLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger lifeGainTriggers =
        PassiveTrigger.NonCombat;
    [SerializeField] [EnumButtons] public PassiveTrigger lifeLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    private void Start()
    {
        entity = this.GetComponent<Entity>();
    }

    // M A X
    public virtual int LIFEMAX
    {
        get
        {   
            int buffBonus = BUFFS.Sum(buff => buff.buffsLifeMax);
            return stats.lifeMax.Get(level) + buffBonus;
        }
    }
    // C U R R E N T
    public int LIFE
    {
        get { return Mathf.Min(stats.life, Player.localPlayer != null ? entity.healthMax : LIFEMAX); }
        set
        {
#if ummorpg
            //health = 
#endif
            stats.life = Mathf.Clamp(value, 0, Player.localPlayer != null ? entity.healthMax : LIFEMAX);
        }
    }
    // P E R C E N T
    public float LIFEFILLED()
    {
        return (LIFE != 0 && LIFEMAX != 0) ? (float)LIFE / (float)LIFEMAX : 0;
    }
    // L O W
    public bool LIFEISLOW { get { return (LIFEFILLED() <= stats.lowLifeThreshold); } }
}

// - - - - -
// B U F F S
// BASE
public partial struct Buff
{
    public int buffsLifeMax { get { return data.buffsLifeMax.Get(level); } }
    public float buffsLifeRegenRate { get { return data.buffsLifeRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsLifeMax;
    public LinearFloat buffsLifeRegenRate; // 0.1=10%; can be negative too
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
