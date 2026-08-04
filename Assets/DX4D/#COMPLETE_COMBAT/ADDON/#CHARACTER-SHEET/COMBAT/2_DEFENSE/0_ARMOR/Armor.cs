using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("ARMOR")]
    [Header(" [ D E F E N S E ] ")]
    [SerializeField] protected LinearInt _armor = new LinearInt { baseValue = 0 };
    public virtual int armor { get { return _armor.Get(level) + buffs.Sum(buff => buff.buffsArmor); } }
}
public partial struct Buff { public int buffsArmor { get { return data.buffsArmor.Get(level); } } }
public abstract partial class BuffSkill : BonusSkill { public LinearInt buffsArmor; }