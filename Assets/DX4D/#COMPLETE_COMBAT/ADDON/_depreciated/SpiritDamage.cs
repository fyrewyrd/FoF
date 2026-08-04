/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("SPIRIT STATS")]

    // D A M A G E  M U L T I P L I E R S
    [SerializeField]
    protected LinearFloat _spiritDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float spiritDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsSpiritDamageMultiplier);
            return _spiritDamageMultiplier.Get(level) + buffBonus;
        }
        set { _spiritDamageMultiplier = new LinearFloat { baseValue = value }; }
    }

    // D A M A G E  B O N U S
    [SerializeField]
    protected LinearInt _spiritDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int spiritDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsSpiritDamageBonus);
            return _spiritDamageBonus.Get(level) + buffBonus;
        }
        set { _spiritDamageBonus = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    public float buffsSpiritDamageMultiplier { get { return data.buffsSpiritDamageMultiplier.Get(level); } }
    public int buffsSpiritDamageBonus { get { return data.buffsSpiritDamageBonus.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsSpiritDamageMultiplier;
    public LinearInt buffsSpiritDamageBonus;

}
*/
