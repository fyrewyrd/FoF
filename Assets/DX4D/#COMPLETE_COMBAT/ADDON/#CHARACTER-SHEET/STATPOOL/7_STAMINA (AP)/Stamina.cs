using Mirror;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public partial class StatPool : NetworkBehaviour
{
    [Header(" [ STAMINA POOL ] ")]
    [SerializeField] protected LinearInt _staminaMax = new LinearInt { baseValue = 100 };
    public LinearInt staminaMax { get { return _staminaMax; } }
    [SyncVar, SerializeField] int _stamina = 100;
    internal int stamina { get { return _stamina; } set { _stamina = value; } }

    [Tooltip("The percentage at which this stat is considered low.\n 1.0f == 100%  0.1f == 10%")]
    [Range(0.0f, 1.0f)] public float lowStaminaThreshold = 0.1f; // 1.0f == 100%
    
    public bool staminaLossKillsMe = false;
    public int staminaGainRate = 1;
    public int staminaLossRate = 1;
    
    [SerializeField] [EnumButtons] public PassiveTrigger staminaGainTriggers =
        PassiveTrigger.NonCombat;
    [SerializeField] [EnumButtons] public PassiveTrigger staminaLossTriggers =
        PassiveTrigger.Moving;
}


public partial class CharacterSheet : NetworkBehaviour
{
    // M A X
    public virtual int STAMINAMAX
    {
        get
        {
            if (Player.localPlayer != null)
            {
                // sum up manually. Linq.Sum() is HEAVY(!) on GC and performance (190 KB/call!)
                int passiveBonus = 0;
                foreach (Skill skill in entity.skills)
                    if (skill.level > 0 && skill.data is PassiveSkill passiveSkill)
                        passiveBonus += passiveSkill.staminaMaxBonus.Get(skill.level);

                int buffBonus = 0;
                for (int i = 0; i < entity.buffs.Count; ++i)
                    buffBonus += entity.buffs[i].buffsStaminaMax;

                // base + passives + buffs
                return stats.staminaMax.Get(level) + passiveBonus + buffBonus;
            }
            else
            {
                int buffBonus = BUFFS.Sum(buff => buff.buffsStaminaMax);
                return stats.staminaMax.Get(level) + buffBonus;
            }
        }
    }
    // C U R R E N T
    public int STAMINA
    {
        get { return Mathf.Min(stats.stamina, STAMINAMAX); }
        set { stats.stamina = Mathf.Clamp(value, 0, STAMINAMAX); }
    }

    // P E R C E N T
    public float STAMINAFILLED()
    {
        return (STAMINA != 0 && STAMINAMAX != 0) ? (float)STAMINA / (float)STAMINAMAX : 0;
    }
    // L O W
    public bool STAMINALOW { get { return (STAMINAFILLED() <= stats.lowStaminaThreshold); } }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int buffsStaminaMax { get { return data.buffsStaminaMax.Get(level); } }
    public float buffsStaminaRegenRate { get { return data.buffsStaminaRegenRate.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsStaminaMax;
    public LinearFloat buffsStaminaRegenRate; // 0.1=10%; can be negative too
}

public abstract partial class BonusSkill : ScriptableSkill
{
    [FormerlySerializedAs("bonusStaminaMax")] public LinearInt staminaMaxBonus;
}

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // s t a m i n a
    tip.Replace("{BUFFSSTAMINAMAX}", buffsStaminaMax.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSTAMINAPERCENTPERSECOND}", Mathf.RoundToInt(buffsStaminaPercentPerSecond.Get(skillLevel) * 100).ToString());

    return tip.ToString();
}
*/
