using System.Linq;
using UnityEngine;

public partial class CombatStats// : Mirror.NetworkBehaviour
{
    [Header("CRITICAL HITS")]
    [SerializeField] protected LinearInt _critChance = new LinearInt { baseValue = 5 };
    public virtual int critChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsCritChance);
            return _critChance.Get(level) + buffBonus;
        }
    }

    [SerializeField] protected LinearFloat _critDamageMultiplier = new LinearFloat { baseValue = 2.0f };
    public virtual float critDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsCritDamageMultiplier);
            return _critDamageMultiplier.Get(level) + buffBonus;
        }
    }
    
    [SerializeField] protected LinearInt _spellCritChance = new LinearInt { baseValue = 5 };
    public virtual int spellCritChance
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsSpellCritChance);
            return _spellCritChance.Get(level) + buffBonus;
        }
    }

    [SerializeField] protected LinearFloat _spellCritDamageMultiplier = new LinearFloat { baseValue = 2.0f };
    public virtual float spellCritDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsSpellCritDamageMultiplier);
            return _spellCritDamageMultiplier.Get(level) + buffBonus;
        }
    }
}

public partial struct Buff
{
    public int buffsCritChance { get { return data.buffsCritChance.Get(level); } }
    public int buffsSpellCritChance { get { return data.buffsSpellCritChance.Get(level); } }
    
    public float buffsCritDamageMultiplier { get { return data.buffsCritDamageMultiplier.Get(level); } }
    public float buffsSpellCritDamageMultiplier { get { return data.buffsSpellCritDamageMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsCritChance;
    public LinearInt buffsSpellCritChance;
    
    public LinearFloat buffsCritDamageMultiplier;
    public LinearFloat buffsSpellCritDamageMultiplier;
}