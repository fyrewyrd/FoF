using Mirror;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class LaunchedProjectile : Targetable
{
    [Header("DAMAGE")]
    [Tooltip("Damage that is dealt immediately when this projectile strikes.")]
    public List<ScriptedDamage> onStrikeDamage = new List<ScriptedDamage>(); //TODO: set by skill???
    [Tooltip("Damage that is dealt when this projectile's stick in target duration expires.")]
    public List<ScriptedDamage> onDestroyedDamage = new List<ScriptedDamage>(); //TODO: set by skill???

    [Header("DAMAGE SETTINGS")]
    [Tooltip("A representation of the damage dealing conditions of the projectile.\nNOTE: When this is added you will be able to edit the attached object down below.")]
    [SerializeField] public ScriptedProjectileDamage damage;
    [Header("FLIGHT PATH")]
    [Tooltip("Information about the projectile's flight path.\nNOTE: When this is added you will be able to edit the attached object down below.")]
    [SerializeField] public ScriptedFlightPath path;
    [Header("VISUAL EFFECTS")]
    [Tooltip("Visual Effects that will be triggered during the projectile's lifetime.\nNOTE: When this is added you will be able to edit the attached object down below.")]
    [SerializeField] public ScriptedProjectileVFX vfx;
    //[Tooltip("")]
    //public StatusEffectList statusEffects; // set by skill

    //[Header("PROJECTILE PATH")]
    //public bool targetSeeking = false;
    //bool destroyIfNoTarget { get { return targetSeeking; } }
    //public AnimationCurve launchArc = new AnimationCurve(new Keyframe(0.0f, 0.0f),new Keyframe(0.4f, 1.0f),  new Keyframe(0.8f, 0.8f), new Keyframe(1.0f, 0.0f));
    //public float launchHeight = 0.0f;
    //public float flightSpeedBonus = 0;
    //public float accelerationRate = 0.01f;
    
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

    //[Header("AOE CONDITIONS")]
    //public bool casterImmuneToAOE = true;
    //public bool friendsImmuneToAOE = false;
    //public bool targetImmuneToAOE = true;

    //[Header("PROJECTILE STRIKE EFFECTS")]
    //public bool sticksInTarget = false;
    //public float stickInTargetForSeconds = 3.0f;
    //public float groupingSize = 0.2f;

    //[Header("PROJECTILE JITTER")]
    //public bool applyJitterOnStrike = true;
    //public float jitterAmount = 0.1f;
    //[Range(0.01f, 0.99f)] public float jitterIntensity = 0.5f;
    //[Header("AOE DAMAGE CONFIGURATION")]




    //[Header("CASTER VISUAL EFFECTS")]
    //public GameObject onFiredVFX;

    //[Header("TARGET VISUAL EFFECTS")]
    //public GameObject onStrikeVFX;
    //public GameObject onDestroyedVFX;
    //public GameObject AOETargetVFX;

    //[Header("PROJECTILE VISUAL EFFECTS")]
    //public GameObject projectileMountedVFX;

    //COPIED FROM THE SKILL THAT LAUNCHED THIS EFFECT
    [HideInInspector] public float projectileSpeed = 35;
    [HideInInspector] public DamageInfo projectileDamage = new DamageInfo();

    //[HideInInspector] public int damage = 0; // set by skill
    //[HideInInspector] public int bonusDamage = 0; // set by skill
    //[HideInInspector] public float damageMultiplier = 1.0f; // set by skill
    //[HideInInspector] public float stunProbability = 0; // set by skill
    //[HideInInspector] public float stunDuration = 0; // set by skill
    [HideInInspector] public float delayBetweenDamage = 0.25f; // set by skill
    //[HideInInspector] public MethodOfDamage damageMethod = MethodOfDamage.Physical; // set by skill
    //[HideInInspector] public Element damageElement = Element.Neutral; // set by skill
    //[HideInInspector] public StatusEffect damageStatusEffect = StatusEffect.None; // set by skill

    //public StatusEffectList statusEffects;

    //[HideInInspector]
    bool targetReached = false;
    //[HideInInspector]
    bool jitterComplete = false;


    // D A M A G E  D E A L I N G

    [Server]
    public void DealTargetedDamage(CharacterSheet defender, List<ScriptedDamage> addedDamage)//, MethodOfDamage damageMethod = MethodOfDamage.Physical, Element element = Element.Neutral, StatusEffect statusEffect = StatusEffect.None, float stunProbability = 0, float stunDuration = 0)
    {
        if (defender.IsDead) return;
        // new StatusInfo[1]{ new StatusInfo(StatusEffect.Stun, stunProbability, stunDuration) });
        //caster.DealDamageAt(defender, (caster.damage + bonusDamage), damageMethod, damageElement, damageStatusEffect, stunProbability, stunDuration); //DEPRECIATED

        if (addedDamage != null && addedDamage.Count > 0)
        {
            caster.StartCoroutine(caster.ApplyDamageOverTime(defender, addedDamage.ToArray(), delayBetweenDamage, projectileDamage.total));// (int)((damage * damageMultiplier) + bonusDamage) ));
        }
        else
        {
            //if (projectileDamage.total > 0)
            caster.DealCombatDamage(defender, projectileDamage);//, statusEffects);// new DamageInfo(damage, damageMethod, damageElement, bonusDamage, damageMultiplier), statusEffects);
        }
    }

    [Server] public void DealAOEDamage(Vector3 targetPosition, List<ScriptedDamage> addedDamage, float aoeRadius, bool noSelfDamage = true, bool noFriendlyFire = false)
    {
        Collider[] colliders = Physics.OverlapSphere(targetPosition, aoeRadius);

        foreach (Collider collision in colliders)
        {
            CharacterSheet aoeTarget = collision.GetComponentInParent<CharacterSheet>();
            if (aoeTarget != null)
            {
                if (aoeTarget.IsDead 
                    || (damage.targetImmuneToAOE && (aoeTarget.GetHashCode() == target.GetHashCode()))
                    || (noFriendlyFire && (aoeTarget.GetType() == caster.GetType()))
                    || (noSelfDamage && (aoeTarget.GetHashCode() == caster.GetHashCode()))
                    )
                {
                    //INVALID TARGET
                }
                else
                {
                    DealTargetedDamage(aoeTarget, addedDamage);
                    //VFX - AOE TARGET
                    if (vfx != null)
                    {
                        if (vfx.AOETargetVFX != null) aoeTarget.vfx.TriggerVisualEffect(aoeTarget.transform, vfx.AOETargetVFX); //Trigger a Visual Effect on the Target
                    }
                }
            }
        }
    }

    Vector3 projectileGoal;
    Vector3 projectileGoalVariance;
    Vector3 origin = Vector3.zero;
    Vector3 initialTarget = Vector3.zero;
    public float DistanceToTarget
    {
        get { return Vector3.Distance(origin, projectileGoal); }
    }
    public float DistanceTraveled
    {
        get { return Vector3.Distance(origin, transform.position); }
    }

    public override void OnStartClient()
    {
        //VFX - PROJECTILE MOUNTED + ON FIRED
        if (vfx != null)
        {
            if (target != null && caster != null)
            {
                if (vfx.projectileMountedVFX != null) caster.vfx.MountVisualEffect(transform, vfx.projectileMountedVFX); //Mount a Visual Effect to the Projectile
                if (vfx.onFiredVFX != null) caster.vfx.TriggerVisualEffect(caster.transform, vfx.onFiredVFX); //Trigger a Visual Effect on the Caster
            }
        }

        origin = transform.position; //LAUNCH POSITION
        initialTarget = target.collider.bounds.center;
        targetReached = false;
        jitterComplete = false;
        projectileGoalVariance = DX4D.Tools.GetRandom.Vector(-path.groupingSize, path.groupingSize);

        transform.position = caster.launchOrigin.position;
        transform.LookAt(initialTarget);
    }


    // fixedupdate on client and server to simulate the same effect without
    // using a NetworkTransform
    void FixedUpdate()
    {
        if (caster == null) //NO CASTER
        {
            if (isServer) NetworkServer.Destroy(gameObject);
            return;
        }
        if (path == null) //NO PATH
        {
            #region DEBUG
#if UNITY_EDITOR
            Debug.LogWarning(" <b>[PROJECTILE]</b> "
                + "\n<color=red>[ERROR]</color> " + name + " does not have a path attached...this projectile has been discarded." +
                "\n<color=green>[FIX]</color> " + "Attach a Flight Path to " + name + " to solve this issue."
                );
#endif
            #endregion
            if (isServer) NetworkServer.Destroy(gameObject);
            return;
        }
        if (path.destroyIfNoTarget && target == null) //NO TARGET
        {
            if (isServer) NetworkServer.Destroy(gameObject);
            return;
        }

        //SET PROJECTILE GOAL
        //if(damageApplied && !varianceAdded) varianceAdded = (Random.value < jitterIntensity);
        // =  + (!damageApplied ? DX4D.Tools.GetRandomVector(0.01f, 0.2f) : Vector3.zero); //((damageApplied && !varianceAdded && applyJitterOnStrike) ? DX4D.Tools.GetRandomVector(0.01f, 0.2f) : Vector3.zero);
        projectileGoal = (path.targetSeeking
            || (path.sticksInTarget && targetReached && path.stickInTargetForSeconds > 0)
            ) ? target.collider.bounds.center : initialTarget;

        //VFX - JITTER
        if (vfx != null)
        {
            if (vfx.applyJitterOnStrike && targetReached && !jitterComplete) { projectileGoal += DX4D.Tools.GetRandom.Vector(-vfx.jitterAmount, vfx.jitterAmount); jitterComplete = (Random.value > vfx.jitterIntensity); }
        }

        //UPDATE ACCELERATION AND GOAL VARIANCE
        Vector3 currentGoal = projectileGoal + projectileGoalVariance;
        if (path.accelerationRate > 0) path.flightSpeedBonus += path.accelerationRate;

        //MOVE TOWARD TARGET
        Vector3 modifiedPosition = Vector3.MoveTowards(transform.position, currentGoal, (projectileSpeed + path.flightSpeedBonus) * Time.fixedDeltaTime);
        if (path.launchHeight > 0 && DistanceToTarget > 0 && DistanceTraveled > 0)
        {
            float currentHeight = DistanceTraveled / DistanceToTarget;
            //if (currentHeight > 0.5f)
            //    modifiedPosition.y += launchHeight * launchArc.Evaluate(currentHeight);
            //else
            modifiedPosition.y += path.launchHeight * path.launchArc.Evaluate(currentHeight) * Time.fixedDeltaTime;
        }
        transform.position = modifiedPosition;
        if (path.targetSeeking) transform.LookAt(currentGoal);

        //CHECK FOR TARGET REACHED
        if (!targetReached)
        {
            // server: reached it? apply skill and destroy self
            float projectileRadius = 1.0f;
            if (isServer
                //X
                && (
                    transform.position.x > currentGoal.x - projectileRadius
                    && transform.position.x < currentGoal.x + projectileRadius
                )
                //Y
                && (
                    transform.position.y > currentGoal.y - projectileRadius
                    && transform.position.y < currentGoal.y + projectileRadius
                )
                //Z
                && (
                    transform.position.z > currentGoal.z - projectileRadius
                    && transform.position.z < currentGoal.z + projectileRadius
                )
                )
            {
                targetReached = true;

                //DAMAGE - ON STRIKE
                if (damage != null)
                {
                    if (damage.DamageOnStrike) DealTargetedDamage(target, onStrikeDamage);
                    //if (DamageOnStrike) DealTargetedDamage(target); //DEPRECIATED
                    if (damage.AOEOnStrike) DealAOEDamage(transform.position, onStrikeDamage, damage.onStrikeAOERadius, damage.casterImmuneToAOE, damage.friendsImmuneToAOE);
                }

                StartCoroutine(DestroyAfter(gameObject, path.stickInTargetForSeconds)); //START THE COUNTDOWN TO DESTRUCTION

                //VFX - ON STRIKE
                if (vfx != null)
                {
                    if (vfx.onStrikeVFX != null) target.vfx.TriggerVisualEffect(target.transform, vfx.onStrikeVFX); //Trigger a Visual Effect on the Target
                }
            }
        }
    }

    [Server] public IEnumerator DestroyAfter(GameObject targetToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            //DAMAGE - ON DESTROYED
            if (damage != null)
            {
                if (damage.DamageOnDestroyed) DealTargetedDamage(target, onDestroyedDamage);
                if (damage.AOEOnDestroyed) DealAOEDamage(targetToDestroy.transform.position, onDestroyedDamage, damage.onDestroyedAOERadius, damage.casterImmuneToAOE, damage.friendsImmuneToAOE);
            }
            //VFX - ON DESTROYED
            if (vfx != null)
            {
                if (vfx.onDestroyedVFX != null) target.vfx.TriggerVisualEffect(target.transform, vfx.onDestroyedVFX); //Trigger a Visual Effect on the Target when the projectile is destroyed
            }
        }

        yield return new WaitForEndOfFrame();

        NetworkServer.Destroy(targetToDestroy);
    }
}
