//#define ummorpg //Toggle for ummorpg integration
using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ MANA POOL ] ")]
    [SerializeField] protected LinearInt _manaMax = new LinearInt { baseValue = 100 };
    public LinearInt manaMax { get { return _manaMax; } }
    [SyncVar, SerializeField] int _mana = 100;
    internal int mana { get { return _mana; } set { _mana = value; } }

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowManaThreshold = 0.1f; // 1.0f == 100%
    
    public bool manaLossKillsMe = true;
    public int manaGainRate = 1;
    public int manaLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger manaGainTriggers =
        PassiveTrigger.NotMoving | PassiveTrigger.Moving;
    [SerializeField] [EnumButtons] public PassiveTrigger manaLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int MANAMAX
    {
        get
        {
            int buffBonus = BUFFS.Sum(buff => buff.buffsManaMax);
            return stats.manaMax.Get(level) + buffBonus;
        }
    }
    // C U R R E N T
    public int MANA
    {
        get { return Mathf.Min(stats.mana, Player.localPlayer != null ? entity.manaMax : MANAMAX); }
        set
        {
#if ummorpg
            //mana = 
#endif
            stats.mana = Mathf.Clamp(value, 0, Player.localPlayer != null ? entity.manaMax : MANAMAX);
        }
    }
    // P E R C E N T
    public float MANAFILLED()
    {
        return (MANA != 0 && MANAMAX != 0) ? (float)MANA / (float)MANAMAX : 0;
    }
    // L O W
    public bool MANALOW { get { return (MANAFILLED() <= stats.lowManaThreshold); } }
}

// - - - - -
// B U F F S
// BASE
public partial struct Buff
{
    public int buffsManaMax { get { return data.buffsManaMax.Get(level); } }
    public float buffsManaRegenRate { get { return data.buffsManaRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsManaMax;
    public LinearFloat buffsManaRegenRate; // 0.1=10%; can be negative too
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
