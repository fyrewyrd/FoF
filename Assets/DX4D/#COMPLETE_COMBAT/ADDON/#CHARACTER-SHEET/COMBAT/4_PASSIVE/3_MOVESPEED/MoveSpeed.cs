using Mirror;
using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("MOVEMENT SPEED")]
    // M O V E  S P E E D  M U L T I P L I E R
    [SerializeField] protected LinearFloat _moveSpeedMultiplier = new LinearFloat { baseValue = 1.0f };
    [SyncVar] float moveSpeed = 1.0f;
    public virtual float moveSpeedMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsMoveSpeedMultiplier);
            return (_moveSpeedMultiplier.Get(level) + buffBonus) * moveSpeed;
        }
        set
        {
            moveSpeed = Mathf.Clamp(value, 0.0f, 20.0f);
        }
    }
    /*
    [Server] public void SetMoveSpeedMultiplier(float amount)
    {
        moveSpeed = Mathf.Clamp(amount, 0.0f, 20.0f);
    }*/
}

// - - - - -
// B U F F S
public partial struct Buff
{
    // S P E E D  M U L T I P L I E R
    public float buffsMoveSpeedMultiplier { get { return data.buffsMoveSpeedMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // S P E E D  M U L T I P L I E R 
    public LinearFloat buffsMoveSpeedMultiplier;
}

/*TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));
    // e v a s i o n
    tip.Replace("{BUFFSDODGECHANCE}", buffsDodgeChance.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPELLDODGECHANCE}", buffsSpellDodgeChance.Get(skillLevel).ToString());

    // o f f e n s i v e
    tip.Replace("{BUFFSMAGICDAMAGEMODIFIER}", buffsMagicDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDAMAGEMODIFIER}", buffsElementalDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDAMAGEMODIFIER}", buffsBloodDamageModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDAMAGEMODIFIER}", buffsSpiritDamageModifier.Get(skillLevel).ToString());

    // d e f e n s i v e
    tip.Replace("{BUFFSMAGICDEFENSEMODIFIER}", buffsMagicDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDEFENSEMODIFIER}", buffsElementalDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDEFENSEMODIFIER}", buffsBloodDefenseModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDEFENSEMODIFIER}", buffsSpiritDefenseModifier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/
