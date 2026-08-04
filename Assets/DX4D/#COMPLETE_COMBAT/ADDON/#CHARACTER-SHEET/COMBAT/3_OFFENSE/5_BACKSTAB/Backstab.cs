using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("BACKSTABBING")]
    [SerializeField] bool _canBackstab = true;
    public bool canBackstab { get { return _canBackstab; } }

    [SerializeField] bool _canBeBackstabbed = true;
    public bool canBeBackstabbed { get { return _canBeBackstabbed; } }
    
    [SerializeField] protected LinearFloat _backstabDamageMultiplier = new LinearFloat { baseValue = 2.0f };
    public virtual float backstabDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBackstabDamageMultiplier);
            return _backstabDamageMultiplier.Get(level) + buffBonus;
        }
    }
}

public partial struct Buff
{
    public float buffsBackstabDamageMultiplier { get { return data.buffsBackstabDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsBackstabDamageMultiplier;
}