/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("BLOOD DAMAGE")]

    // D A M A G E  M U L T I P L I E R S
    [SerializeField]
    protected LinearFloat _bloodDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float bloodDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBloodDamageMultiplier);
            return _bloodDamageMultiplier.Get(level) + buffBonus;
        }
        set { _bloodDamageMultiplier = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  B O N U S
    [SerializeField]
    protected LinearInt _bloodDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int bloodDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBloodDamageBonus);
            return _bloodDamageBonus.Get(level) + buffBonus;
        }
        set { _bloodDamageBonus = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsBloodDamageMultiplier { get { return data.buffsBloodDamageMultiplier.Get(level); } }
    public int buffsBloodDamageBonus { get { return data.buffsBloodDamageBonus.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsBloodDamageMultiplier;
    public LinearInt buffsBloodDamageBonus;
}
*/
