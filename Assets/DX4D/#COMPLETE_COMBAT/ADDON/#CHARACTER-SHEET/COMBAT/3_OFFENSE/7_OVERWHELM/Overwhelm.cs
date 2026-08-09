using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("OVERWHELM")]
    [SerializeField] bool _canOverwhelm = true;
    public bool canOverwhelm { get { return _canOverwhelm; } }

    [SerializeField] bool _canBeOverwhelmed = true;
    public bool canBeOverwhelmed { get { return _canBeOverwhelmed; } }
    
    [SerializeField] protected LinearFloat _overwhelmDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float overwhelmDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsOverwhelmDamageMultiplier);
            return _overwhelmDamageMultiplier.Get(level) + buffBonus;
        }
    }
}

public partial struct Buff
{
    public float buffsOverwhelmDamageMultiplier { get { return data.buffsOverwhelmDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsOverwhelmDamageMultiplier;
}