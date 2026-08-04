/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // V U L N E R A B I L I T Y
    [SerializeField]
    protected LinearFloat _spiritVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float spiritVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsSpiritVulnerability);
            return _spiritVulnerability.Get(level) + buffBonus;
        }
        set { _spiritVulnerability = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  R E D U C T I O N
    [SerializeField]
    protected LinearInt _spiritDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int spiritDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsSpiritDamageReduction);
            return _spiritDamageReduction.Get(level) + buffBonus;
        }
        set { _spiritDamageReduction = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsSpiritVulnerability { get { return data.buffsSpiritVulnerability.Get(level); } }
    public int buffsSpiritDamageReduction { get { return data.buffsSpiritDamageReduction.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsSpiritVulnerability;
    public LinearInt buffsSpiritDamageReduction;
}
*/
