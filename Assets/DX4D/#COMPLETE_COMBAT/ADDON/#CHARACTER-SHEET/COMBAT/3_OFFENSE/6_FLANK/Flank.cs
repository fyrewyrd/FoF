using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("FLANKING")]
    [SerializeField] bool _canFlank = true;
    public bool canFlank { get { return _canFlank; } }

    [SerializeField] bool _canBeFlanked = true;
    public bool canBeFlanked { get { return _canBeFlanked; } }
    
    [SerializeField] protected LinearFloat _flankDamageMultiplier = new LinearFloat { baseValue = 1.5f };
    public virtual float flankDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsFlankDamageMultiplier);
            return _flankDamageMultiplier.Get(level) + buffBonus;
        }
    }
}

public partial struct Buff
{
    public float buffsFlankDamageMultiplier { get { return data.buffsFlankDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsFlankDamageMultiplier;
}