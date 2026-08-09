/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("POISON STATS")]

    // D A M A G E  M U L T I P L I E R S
    [SerializeField]
    protected LinearFloat _poisonDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float poisonDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsPoisonDamageMultiplier);
            return _poisonDamageMultiplier.Get(level) + buffBonus;
        }
        set { _poisonDamageMultiplier = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  B O N U S
    [SerializeField]
    protected LinearInt _poisonDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int poisonDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsPoisonDamageBonus);
            return _poisonDamageBonus.Get(level) + buffBonus;
        }
        set { _poisonDamageBonus = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsPoisonDamageMultiplier { get { return data.buffsPoisonDamageMultiplier.Get(level); } }
    public int buffsPoisonDamageBonus { get { return data.buffsPoisonDamageBonus.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsPoisonDamageMultiplier;
    public LinearInt buffsPoisonDamageBonus;
}
*/
