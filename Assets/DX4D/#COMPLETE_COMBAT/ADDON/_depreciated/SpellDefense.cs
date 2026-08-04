/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("SPELL DEFENSE")]
    [SerializeField]
    protected LinearInt _spellDefense = new LinearInt { baseValue = 1 };
    public virtual int spellDefense
    {
        get
        {
            // base + passives + buffs
            int passiveBonus = (from skill in skills
                                where skill.level > 0 && skill.data is PassiveSkill
                                select ((PassiveSkill)skill.data).addedSpellDefense.Get(skill.level)).Sum();
            int buffBonus = buffs.Sum(buff => buff.addedSpellDefense);
            return _spellDefense.Get(level) + passiveBonus + buffBonus;
        }
    }
}

// - - - - -
// B U F F S
public partial struct Buff
{
    public int addedSpellDefense { get { return data.addedSpellDefense.Get(level); } }
    //public int buffsSpellDefense { get { return data.buffsSpellDefense.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearInt buffsSpellDefense;
}

public abstract partial class BonusSkill : ScriptableSkill
{
    public LinearInt addedSpellDefense;
}
*/
