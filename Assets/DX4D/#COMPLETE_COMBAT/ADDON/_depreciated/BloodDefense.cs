/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("BLOOD DEFENSE")]

    // V U L N E R A B I L I T Y
    [SerializeField]
    protected LinearFloat _bloodVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float bloodVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBloodVulnerability);
            return _bloodVulnerability.Get(level) + buffBonus;
        }
        set { _bloodVulnerability = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  R E D U C T I O N
    [SerializeField]
    protected LinearInt _bloodDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int bloodDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBloodDamageReduction);
            return _bloodDamageReduction.Get(level) + buffBonus;
        }
        set { _bloodDamageReduction = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsBloodVulnerability { get { return data.buffsBloodVulnerability.Get(level); } }
    public int buffsBloodDamageReduction { get { return data.buffsBloodDamageReduction.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsBloodVulnerability;
    public LinearInt buffsBloodDamageReduction;
}
*/
