/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // V U L N E R A B I L I T Y
    [SerializeField]
    protected LinearFloat _poisonVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float poisonVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsPoisonVulnerability);
            return _poisonVulnerability.Get(level) + buffBonus;
        }
        set { _poisonVulnerability = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  R E D U C T I O N
    [SerializeField]
    protected LinearInt _poisonDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int poisonDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsPoisonDamageReduction);
            return _poisonDamageReduction.Get(level) + buffBonus;
        }
        set { _poisonDamageReduction = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsPoisonVulnerability { get { return data.buffsPoisonVulnerability.Get(level); } }
    public int buffsPoisonDamageReduction { get { return data.buffsPoisonDamageReduction.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsPoisonVulnerability;
    public LinearInt buffsPoisonDamageReduction;
}
*/
