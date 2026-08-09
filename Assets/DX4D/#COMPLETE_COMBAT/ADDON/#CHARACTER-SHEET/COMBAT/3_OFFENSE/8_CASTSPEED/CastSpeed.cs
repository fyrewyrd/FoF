using Mirror;
using System.Linq;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("CAST SPEED")]
    [Tooltip("Determines how fast this character casts skills.\nA value of 2 would mean double the cast time, 0.5 would mean half the cast time. 0 Would mean instant cast times.")]
    [SerializeField] protected LinearFloat _castSpeedMultiplier = new LinearFloat { baseValue = 1.0f };
    [SyncVar] float castSpeed = 1.0f;
    ///<summary>Determines how fast this character casts skills. A value of 2 would mean double the cast time, 0.5 would mean half the cast time. 0 would mean instant cast times.</summary>
    public virtual float castSpeedMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsCastSpeedMultiplier);
            return (int)((_castSpeedMultiplier.Get(level) + buffBonus) * castSpeed);
        }
        set
        {
            castSpeed = Mathf.Clamp(value, 0.0f, 20.0f);
        }
    }
}

public partial struct Buff
{
    public float buffsCastSpeedMultiplier { get { return data.buffsCastSpeedMultiplier.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    public LinearFloat buffsCastSpeedMultiplier;
}