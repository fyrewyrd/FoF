using Mirror;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/GEAR/Combat Weapon", order = 13)]
public partial class CombatWeapon : CombatGear
{
    [Header(" [ WEAPON TYPE ] ")]
    [SerializeField] public WeaponCategory weaponCategory = WeaponCategory.UnArmed;
    [SerializeField] public WeaponHand weaponHand = WeaponHand.LeftHanded;

    [Header(" [ WEAPON SKILLS ] ")]
    [SerializeField] public List<ScriptableSkill> weaponSkills = new List<ScriptableSkill>();
    //[SerializeField] public Skill mainWeaponSkill;

    [Header(" [ BASE DAMAGE ] ")]
    [SerializeField] public DamageInfo damage;
    //[SerializeField] public StatusEffectList status;

    //[SerializeField] public MethodOfDamage dealsDamageType = MethodOfDamage.Physical;
    //[SerializeField] public Element dealsElementalDamageType = Element.Neutral;
    //[SerializeField] [EnumButtons] public StatusEffect dealsStatusEffect = StatusEffect.None; //TODO: 

    //[SerializeField][HideInInspector] public DamageEvent onHitDamageEffect; //DEPRECIATED

    [Header(" [ ATTACK SPEED ] ")]
    [Tooltip("The amount of time (in seconds) between attacking with this weapon and the weapon striking the target")]
    [SerializeField] public float attackDelay = 1.0f;
    [Tooltip("Determines how fast a character attacks with this weapon.\nA value of 2 would mean double the cast time, 0.5 would mean half the cast time. 0 Would mean instant cast times.")]
    [SerializeField] public float _attackSpeedMultiplier = 1.0f;
    ///<summary>Determines how fast a character attacks with this weapon.
    ///A value of 2 would mean double the cast time, 0.5 would mean half the cast time. 0 would mean instant cast times.</summary>
    public float attackSpeedMultiplier
    {
        get { return _attackSpeedMultiplier; }
        set { _attackSpeedMultiplier = value; }
    }

    [Header(" [ ATTACK RANGE ] ")]
    [SerializeField] public bool rangedWeapon = false;
    [SyncVar] public float attackRange = 1.15f;
    public const int closeRangeCutoff = 3;

    public bool HasAmmo { get { return (ammunition != null && ammunition.Count > 0); } }
    //[SerializeField]
    [Header(" [ AMMUNITION ] ")]
    [SerializeField] public bool requiresAmmo = false;
    [SyncVar] public List<AmmunitionItem> ammunition = new List<AmmunitionItem>();
    [SerializeField] public int maxAmmo = 18; //Quiver/Magazine Size
    [SerializeField] public AmmoCategory requiredAmmo = AmmoCategory.Charge;

    // T O O L T I P
    public override string ToolTip()
    {
        StringBuilder info = new StringBuilder(base.ToolTip());
        if(automaticTooltip) info.Append( Tooltip.OffenseToolTip(this) );
        info.Replace("{CATEGORY}", weaponCategory.ToString());
        return info.ToString();
    }
}
