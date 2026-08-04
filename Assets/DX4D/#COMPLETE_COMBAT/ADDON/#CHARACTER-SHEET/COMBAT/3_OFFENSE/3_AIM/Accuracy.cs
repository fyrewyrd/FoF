using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("ACCURACY")]
    [SerializeField] protected LinearInt _hitChance = new LinearInt { baseValue = 95 };
    public virtual int hitChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsHitChance);
            return _hitChance.Get(level) + buffBonus;
        }
    }
    
    [SerializeField] protected LinearInt _spellHitChance = new LinearInt { baseValue = 100 };
    public virtual int spellHitChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsSpellHitChance);
            return _spellHitChance.Get(level) + buffBonus;
        }
    }
}

public partial struct Buff
{
    public int buffsHitChance { get { return data.buffsHitChance.Get(level); } }
    public int buffsSpellHitChance { get { return data.buffsSpellHitChance.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsHitChance;
    public LinearInt buffsSpellHitChance;
}