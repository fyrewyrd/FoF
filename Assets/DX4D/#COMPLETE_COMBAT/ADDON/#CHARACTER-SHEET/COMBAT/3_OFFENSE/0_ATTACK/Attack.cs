using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("ATTACK")]
    [Header(" [ O F F E N S E ] ")]
    [SerializeField] protected LinearInt _attack = new LinearInt { baseValue = 1 };
    public virtual int attack { get { return _attack.Get(level) + buffs.Sum(buff => buff.buffsAttack); } }
}
public partial struct Buff { public int buffsAttack { get { return data.buffsAttack.Get(level); } } }
public abstract partial class BuffSkill : BonusSkill { public LinearInt buffsAttack; }