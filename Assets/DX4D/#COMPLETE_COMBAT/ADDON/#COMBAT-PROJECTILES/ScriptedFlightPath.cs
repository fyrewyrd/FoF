using System;
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/PROJECTILE/Flight Path", order = 52)]
[Serializable] public class ScriptedFlightPath : ScriptableObject
{
    [Header("PROJECTILE PATH")]
    public bool targetSeeking = false;
    public bool destroyIfNoTarget { get { return targetSeeking; } }
    public AnimationCurve launchArc = new AnimationCurve(new Keyframe(0.0f, 0.0f),new Keyframe(0.4f, 1.0f),  new Keyframe(0.8f, 0.8f), new Keyframe(1.0f, 0.0f));
    public float launchHeight = 0.0f;
    public float flightSpeedBonus = 0;
    public float accelerationRate = 0.01f;

    [Header("PROJECTILE STRIKE EFFECTS")]
    public bool sticksInTarget = false;
    public float stickInTargetForSeconds = 3.0f;
    public float groupingSize = 0.2f;
    
    //[SerializeField] public WeaponCategory weaponTypeRequired;
    //[SerializeField] public List<ScriptableSkill> skillList = new List<ScriptableSkill>();
    
    //ON STRIKE
    //[Header("ON STRIKE DAMAGE")]
    //public bool DamageOnStrike = true;
    //public bool AOEOnStrike = false;
    //public float onStrikeAOERadius = 5;
    //public List<ScriptedDamage> onStrikeDamage = new List<ScriptedDamage>(); // set by skill
    //ON DESTROYED
    //[Header("ON DESTROYED DAMAGE")]
    //public bool DamageOnDestroyed = false;
    //public bool AOEOnDestroyed = false;
    //public float onDestroyedAOERadius = 5;
    //public List<ScriptedDamage> onDestroyedDamage = new List<ScriptedDamage>(); // set by skill

    //[Header("PROJECTILE JITTER")]
    //public bool applyJitterOnStrike = true;
    //public float jitterAmount = 0.1f;
    //[Range(0.01f, 0.99f)] public float jitterIntensity = 0.5f;
    //[Header("AOE DAMAGE CONFIGURATION")]

    //[Header("AOE CONDITIONS")]
    //public bool casterImmuneToAOE = true;
    //public bool friendsImmuneToAOE = false;
    //public bool targetImmuneToAOE = true;

    //[Header("CASTER VISUAL EFFECTS")]
    //public GameObject onFiredVFX;

    //[Header("TARGET VISUAL EFFECTS")]
    //public GameObject onStrikeVFX;
    //public GameObject onDestroyedVFX;
    //public GameObject AOETargetVFX;

    //[Header("PROJECTILE VISUAL EFFECTS")]
    //public GameObject projectileMountedVFX;

    //COPIED FROM THE SKILL THAT LAUNCHED THIS EFFECT
    //[HideInInspector] public float projectileSpeed = 35;
    //[HideInInspector] public DamageInfo projectileDamage = new DamageInfo();

    //[HideInInspector] public int damage = 0; // set by skill
    //[HideInInspector] public int bonusDamage = 0; // set by skill
    //[HideInInspector] public float damageMultiplier = 1.0f; // set by skill
    //[HideInInspector] public float stunProbability = 0; // set by skill
    //[HideInInspector] public float stunDuration = 0; // set by skill
    //[HideInInspector] public float delayBetweenDamage = 0.25f; // set by skill
    //[HideInInspector] public MethodOfDamage damageMethod = MethodOfDamage.Physical; // set by skill
    //[HideInInspector] public Element damageElement = Element.Neutral; // set by skill
    //[HideInInspector] public StatusEffect damageStatusEffect = StatusEffect.None; // set by skill

    //public StatusEffectList statusEffects;

}
