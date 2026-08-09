/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("SPELL DEFENSE")]
    [SerializeField]
    protected LinearInt _physicalDefense = new LinearInt { baseValue = 1 };
    public virtual int physicalDefense
    {
        get
        {
            // base + passives + buffs
            int passiveBonus = (from skill in skills
                                where skill.level > 0 && skill.data is PassiveSkill
                                select ((PassiveSkill)skill.data).addedPhysicalDefense.Get(skill.level)).Sum();
            int buffBonus = buffs.Sum(buff => buff.buffsPhysicalDefense);
            return _physicalDefense.Get(level) + passiveBonus + buffBonus;
        }
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int addedPhysicalDefense { get { return data.addedPhysicalDefense.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsPhysicalDefense;
}

public abstract partial class BonusSkill : ScriptableSkill
{
    public LinearInt addedPhysicalDefense;
}
*/
