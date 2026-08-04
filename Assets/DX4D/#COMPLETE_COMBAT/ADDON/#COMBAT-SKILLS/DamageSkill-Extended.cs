//#define RPG2D //NOTE: Enable this define for 2D support...or import the 2D_MODE unity package included with this asset
//#define PRE184 //NOTE: Enable this to support legacy ummorpg versions

using Mirror;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class DamageSkill : ActiveSkill
{
    #region WEAPON SKILL CONFIG
    [System.Serializable] public class WeaponSkillConfig
    {
        [Header(" -use weapon damage- ")]
        /// <summary>Add weapon damage method to this skill.</summary>
        [Tooltip("Uses the Weapon's Damage Method.\n(overrides this skill's damage method)")]
        [SerializeField] public bool useWeaponDamageMethod = false;
        /// <summary>Add weapon damage element to this skill.</summary>
        [Tooltip("Uses the Weapon's Damage Element.\n(overrides this skill's damage element)")]
        [SerializeField] public bool useWeaponElement = false;

        /// <summary>Add weapon damage to this skill.</summary>
        [Header(" -add weapon damage- ")]
        [Tooltip("Adds Weapon Damage")]
        [SerializeField] public bool addDamage = true;
        /// <summary>Add weapon damage over time to this skill.</summary>
        [Tooltip("Adds Weapon Damage Over Time")]
        [SerializeField] public bool addDOT = true;
        /// <summary>Add weapon skill effects to this skill.</summary>
        [Tooltip("Adds Weapon Status Effects")]
        [SerializeField] public bool addStatusEffects = true;
        /// <summary>Add weapon cooldowns to this skill.</summary>
        [Tooltip("Adds Weapon Cooldowns")]
        [SerializeField] public bool addCooldown = true;
        
        public WeaponSkillConfig() : this(false, false, true, true, true, true) { }
        public WeaponSkillConfig(
            bool damage, bool damageMethod,
            bool element = true, bool dot = true,
            bool status = true, bool cooldown = true)
        {
            addDamage = damage;
            useWeaponDamageMethod = damageMethod;
            useWeaponElement = element;
            addDOT = dot;
            addStatusEffects = status;
            addCooldown = cooldown;
        }
        public WeaponSkillConfig MakeCopy()
        {
            WeaponSkillConfig info = (WeaponSkillConfig)this.MemberwiseClone();

            info.addDamage = addDamage;
            info.useWeaponDamageMethod = useWeaponDamageMethod;
            info.useWeaponElement = useWeaponElement;
            info.addDOT = addDOT;
            info.addStatusEffects = addStatusEffects;
            info.addCooldown = addCooldown;

            return info;
        }
    }
    #endregion

    [Header("LAUNCHED PROJECTILE")]
    [Tooltip("Launches a projectile whenever this skill is activated.")]
    public LaunchedProjectile launchedProjectile;
    public int launchedProjectileSpeed = 35;

    [Header("WEAPON SKILL CONFIG")]
    [Tooltip("Configures the skill to use values from the equipped weapon.")]
    [SerializeField] WeaponSkillConfig weaponConfig = new WeaponSkillConfig();

    [Header("DAMAGE AND STATUS EFFECTS")]
    [SerializeField] public DamageInfo skillDamage = new DamageInfo();
    //[SerializeField] public StatusEffectList skillStatusEffects;

    public int totalAddedDamage
    {
        get
        {
            int runningTotal = 0;

            for (int i = 0; i < skillDamage.damageOverTime.Count; i++)
            {
                runningTotal += skillDamage.damageOverTime[i].damage.total;
            }

            return runningTotal;
        }
    }

    public override bool CheckSelf(Entity caster, int skillLevel)
    {
        /*
        //AUTO TARGETING
        if (autoTargeting)
        {
            if (!caster.target || caster.target.character.IsDead)
            {
                // find all monsters that are alive, sort by distance
                //GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster"); //DEPRECIATED - Gets ALL the monsters?
                List<CharacterSheet> objects = caster.character.ai.FindAggroTargets(caster.character.ai.detectionRadius);// GameObject.FindGameObjectsWithTag("Monster");


                //caster.player.SetIndicatorViaParent(sorted[0].transform); //TODO Selection Indicator
                caster.player.CmdSetTarget(caster.character.ai.FindClosestCharacter(objects).gameObject);
            }
        }*/

        return base.CheckSelf(caster, skillLevel);
    }

#if RPG2D
    // 2 D  -  C H E C K  D I S T A N C E
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector2 destination)
    {
        //VALIDATE TARGET
        if (caster.target != null)
        {
            if (launchedProjectile != null) { destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position); }
#else
    // 3 D  -  C H E C K  D I S T A N C E
    //  ________________________________________________________________
    //  |                > > >  NOTE TO 2D USERS  < < <                 |
    //  |   If you are using 2D just scroll to the top of this script   |
    //  |   and remove the // from in front of #define RPG2D            |
    //  |   Alternatively you can just import the 2D_MODE.unitypackage  |
    public override bool CheckDistance(Entity caster, int skillLevel, out Vector3 destination)
    {
        if (!approachTarget)
        {
            // can cast anywhere
            destination = caster.transform.position;
            //return true;
#if PRE184 //LEGACY SUPPORT: Scroll up to the top and enable this define to support legacy ummorpg versions
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
#else

            if (Utils.ClosestDistance(caster, caster.target) <= castRange.Get(skillLevel))
            {
                return true;
            }
            else
            {
                caster.character.RpcShowSpeechPopup("[out of range]");
                return false;
            }
#endif
        }

        //VALIDATE TARGET
        if (caster.target != null)
        {
            if (launchedProjectile != null) { destination = caster.target.collider.ClosestPoint(caster.transform.position); }
#endif
            else { destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position); }

#if PRE184 || RPG2D //LEGACY SUPPORT: Scroll up to the top and enable this define to support legacy ummorpg versions
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
#else
            return Utils.ClosestDistance(caster, caster.target) <= castRange.Get(skillLevel);
#endif
        }
        destination = caster.transform.position;
        return false;
    }
    public override void OnCastStarted(CharacterSheet caster)
    {
        //Debug.Log("{" + name.ToUpper() + "} DAMAGE SKILL <" + name.ToUpper() + "> CAST STARTING!!!"); //DEBUG
        
        base.OnCastStarted(caster);

    }
    public override void OnCastFinished(CharacterSheet caster)
    {
        //Debug.Log("{" + caster.name.ToUpper() + "} DAMAGE SKILL <" + name.ToUpper() + "> CAST FINISHING!!!"); //DEBUG
        base.OnCastFinished(caster);
    }


    //DAMAGE TO APPLY //STATUS TO APPLY
    DamageInfo modifiedSkillDamage = new DamageInfo();
    ScriptableDamageSkill ammoSkill;
    //StatusEffectList modifiedSkillStatus;
    public bool SyncWeaponDamage(PlayerCharacter casterAsPlayer, CombatWeapon weapon, int skillLevel = 1)
    {
        // S K I L L  D A M A G E
        modifiedSkillDamage = skillDamage.copy();

        if (!weapon) return false;

        // A M M O
        if (weapon.requiresAmmo && weapon.HasAmmo)
        {
            ammoSkill = (weapon.ammunition[0].attachedSkill as ScriptableDamageSkill);
        }
        else
        {
            ammoSkill = null;
        }
        
        // W E A P O N  D A M A G E
        if (weaponConfig.addDamage)
        {
            //weapon
            modifiedSkillDamage.min += weapon.damage.min;
            modifiedSkillDamage.max += weapon.damage.max;

            modifiedSkillDamage.bonus += weapon.damage.bonus;
            modifiedSkillDamage.multiplier *= weapon.damage.multiplier;
            //ammo
            if (ammoSkill != null)
            {
                modifiedSkillDamage.min += ammoSkill.skillDamage.min;
                modifiedSkillDamage.max += ammoSkill.skillDamage.max;
                //modifiedSkillDamage.min += ammoSkill.damage.Get(skillLevel);
                //modifiedSkillDamage.max += ammoSkill.damage.Get(skillLevel);

                modifiedSkillDamage.bonus += ammoSkill.skillDamage.bonus;
                modifiedSkillDamage.multiplier *= ammoSkill.skillDamage.multiplier;
            }
        }
        //DAMAGE METHOD
        if (weaponConfig.useWeaponDamageMethod)
        {
            //weapon
            modifiedSkillDamage.method = weapon.damage.method;
            //ammo
            if (ammoSkill != null) modifiedSkillDamage.method = ammoSkill.skillDamage.method;
        }
        //ELEMENT
        if (weaponConfig.useWeaponElement)
        {
            //weapon
            modifiedSkillDamage.element = weapon.damage.element;
            //ammo
            if (ammoSkill != null) modifiedSkillDamage.element = ammoSkill.skillDamage.element;
        }
        //DOTs
        if (weaponConfig.addDOT)
        {
            //NOTE: We assign the highest tick interval here to prevent undesirably fast damage ticks in some cases.

            //TODO: Double check that the damageTickInterval is being set as desired
            //weapon
            modifiedSkillDamage.damageTickInterval = System.Math.Max(modifiedSkillDamage.damageTickInterval, weapon.damage.damageTickInterval);
            modifiedSkillDamage.damageOverTime.AddRange(weapon.damage.damageOverTime);
            //ammo
            if (ammoSkill != null)
            {
                modifiedSkillDamage.damageTickInterval = System.Math.Max(modifiedSkillDamage.damageTickInterval, ammoSkill.skillDamage.damageTickInterval);
                modifiedSkillDamage.damageOverTime.AddRange(ammoSkill.skillDamage.damageOverTime);
            }
        }
        //STATUS EFFECTS
        if (weaponConfig.addStatusEffects)
        {
            //weapon
            modifiedSkillDamage.status.statusEffects.AddRange(weapon.damage.status.statusEffects);
            //ammo
            if (ammoSkill != null) modifiedSkillDamage.status.statusEffects.AddRange(ammoSkill.skillDamage.status.statusEffects);
        }
        //COOLDOWN
        if (weaponConfig.addCooldown)
        {
            addedCooldown = 0; //RESET ADDED COOLDOWN
            //weapon
            addedCooldown += (weapon.attackDelay * weapon.attackSpeedMultiplier);
            //ammo
            if (weapon.requiresAmmo && weapon.HasAmmo && ammoSkill != null)
            {
                addedCooldown += (ammoSkill.cooldown.baseValue * weapon.attackSpeedMultiplier);
            }
        }
        //TODO: player cast speed multiplier
        //        cooldown.baseValue = ((cooldown.baseValue * casterAsPlayer.combat.castSpeedMultiplier) + //<-- HERE
        //            (weaponConfig.addCooldown ? (weapon.attackDelay * weapon.attackSpeedMultiplier) : 0)
        //            );


        // A M M U N I T I O N  D A M A G E
        if (weapon.requiresAmmo)
        {
            if (weapon.HasAmmo)
            {
                return casterAsPlayer.FireWeapon(weapon);
            }
            else
            {
                casterAsPlayer.RpcShowTextPopup("[out of ammo]");
                return false;
            }
        }

        return true;
    }
    public override void Apply(Entity caster, int skillLevel)
    {
        Apply(caster.character, (caster.target.character ?? null), skillLevel);
        //caster.DealDamageAt(caster.target, modifiedSkillDamage.total, 0f, 0f);
        //caster.DealDamageAt(caster.target,
        //                    caster.character.combat.attack + damage.Get(skillLevel),
        //                    stunChance.Get(skillLevel),
        //                    stunTime.Get(skillLevel));
    }

    public void Apply(CharacterSheet caster, CharacterSheet target, int skillLevel)
    {
        if (!caster) return; //NO CASTER
        if (!ValidateTarget(caster, target)) return; //VALIDATION
        //if (!target) return; //NO TARGET //Handled in Validate function
        
        #region DEBUG
#if UNITY_EDITOR
        StringBuilder skillApplyLog = new StringBuilder("<b>| W E A P O N  S K I L L  D A M A G E  L O G |</b>");
#endif
        #endregion

        //APPLY VFX + FIX TARGET
        //base.Apply(caster, skillLevel); //Fixes the target and applies visual effects //DEPRECIATED

        //BASE SKILL DAMAGE
        modifiedSkillDamage.min += damage.Get(skillLevel);
        modifiedSkillDamage.max += damage.Get(skillLevel);

        // P L A Y E R
        if (caster.player)
        {
            //PlayerCharacter player = caster.player;
            //cooldown.baseValue = ((0.5f) + (1.5f * caster.player.combat.castSpeedMultiplier)); //TODO: Make sure removing this broke nothing else
            
            //WEAPON DAMAGE
            if (weaponConfig.addDamage)
            {
                //NON WEAPON SKILL
                if (caster.player.HasMainWeapon)
                {
                    if (!SyncWeaponDamage(caster.player, caster.player.mainWeapon, skillLevel)) { return; }
                }
                else if (caster.player.HasOffhandWeapon)
                {
                    if (!SyncWeaponDamage(caster.player, caster.player.offhandWeapon, skillLevel)) { return; }
                }
                else if (caster.player.IsUnarmed)
                {
                    if (caster.player.unarmedWeapon != null)
                    {
                        if (!SyncWeaponDamage(caster.player, caster.player.unarmedWeapon, skillLevel)) { return; }
                    }
                    else
                    {
                        //DEFAULT NON DX4D WEAPON
                    }
                }
            }

            //SIEGE WEAPON
            if (caster.player.HasSiegeWeapon && caster.player.IsCastingSiegeWeaponSkill)
            {
                if (!SyncWeaponDamage(caster.player, caster.player.siegeWeapon, skillLevel)) return;
                #region DEBUG
#if UNITY_EDITOR
                skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING SIEGE DAMAGE WITH " + "<" + caster.player.siegeWeapon.name.ToUpper() + ">"); //DEBUG
#endif
                #endregion
            }
            //COMBAT WEAPON
            else if (caster.player.HasMainWeapon)
            {
                //DUAL WIELD
                if (caster.player.IsDualWielding)
                {

                    #region DEBUG
#if UNITY_EDITOR
                    skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} IS DUAL WIELDING " + "<" + caster.player.mainWeapon.name.ToUpper() + ">" + "<" + caster.player.offhandWeapon.name.ToUpper() + ">"); //DEBUG
#endif
                    #endregion
                    if (caster.player.offhandWeapon.weaponSkills.Count > 0) nextSkill = caster.player.offhandWeapon.weaponSkills[0]; //FOLLOW UP SKILL
                    else { nextSkill = caster.player.defaultOffhandWeaponSkill; }
                    // casterAsPlayer.skills[casterAsPlayer.mainWeapon.weaponSkills.Count].data;
                }

                //MAIN WEAPON
                if (caster.player.IsCastingMainWeaponSkill)
                {
                    if (!SyncWeaponDamage(caster.player, caster.player.mainWeapon, skillLevel)) return;
                    #region DEBUG
#if UNITY_EDITOR

                    skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING MAINHAND DAMAGE WITH " + "<" + caster.player.mainWeapon.name.ToUpper() + ">"); //DEBUG
#endif
                    #endregion
                }
                //OFFHAND WEAPON
                else if (caster.player.HasOffhandWeapon && caster.player.IsCastingOffhandWeaponSkill)
                {
                    if (!SyncWeaponDamage(caster.player, caster.player.offhandWeapon, skillLevel)) return;
                    #region DEBUG
#if UNITY_EDITOR

                    skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING OFFHAND DAMAGE WITH " + "<" + caster.player.offhandWeapon.name.ToUpper() + ">"); //DEBUG
#endif
                    #endregion
                }
                //UNARMED WEAPON
                else if (caster.player.IsUnarmed && caster.player.IsCastingUnarmedSkill)
                {
                    if (!SyncWeaponDamage(caster.player, caster.player.unarmedWeapon, skillLevel)) return;
                    #region DEBUG
#if UNITY_EDITOR
                    skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING UNARMED DAMAGE WITH " + "<" + caster.player.unarmedWeapon.name.ToUpper() + ">"); //DEBUG
#endif
                    #endregion
                }
                else
                {
                    #region DEBUG
#if UNITY_EDITOR
                    //TODO: Does this ever happen???
                    skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING SOME WEIRD WEAPON DAMAGE...Let a DX4D Admin know you saw this message..."); //DEBUG
#endif
                    #endregion
                }
            }
            //NON COMBAT WEAPON (ummorpg weapon for example)
            else
            {
                modifiedSkillDamage = skillDamage.copy();

                //TODO: Evaluate this
                //USE CASTER INNATE DAMAGE (For Physical/Neutral Skills)
                if (caster.combat.imbueAttacks)
                {
                    if (modifiedSkillDamage.method == MethodOfDamage.Physical) modifiedSkillDamage.method = caster.combat.innateMethodOfDamage;//.damageMethod;
                    if (modifiedSkillDamage.element == Element.Neutral) modifiedSkillDamage.element = caster.combat.innateDamageElement;//.damageElement;
                }
                //modifiedSkillDamage.status = skillDamage.status;// skillStatusEffects;
                #region DEBUG
#if UNITY_EDITOR
                skillApplyLog.Append("\n{" + caster.name.ToUpper() + "} DEALING NON-COMBAT WEAPON DAMAGE\nNOTE: You should consider replacing this weapon with a DX4D Combat Weapon."); //DEBUG
#endif
                #endregion
            }

            //FOLLOW UP SKILL
            if (nextSkill != null)
            {
                caster.player.TryUseSkill(caster.GetSkillIndexByName(nextSkill.name));
                //nextSkill = null; //TODO: Make sure we did not need this
            }
        }
        else // N P C
        {
            modifiedSkillDamage = skillDamage.copy();

            //modifiedSkillDamage.bonus += caster.combat.attack + damage.Get(caster.level);
            //USE CASTER INNATE DAMAGE (For Physical/Neutral Skills)
            if (caster.combat.imbueAttacks)
            {
                if (modifiedSkillDamage.method == MethodOfDamage.Physical) modifiedSkillDamage.method = caster.combat.innateMethodOfDamage;//.damageMethod;
                if (modifiedSkillDamage.element == Element.Neutral) modifiedSkillDamage.element = caster.combat.innateDamageElement;//.damageElement;
            }

            //modifiedSkillDamage.status = skillDamage.status;// skillStatusEffects;
            #region DEBUG
#if UNITY_EDITOR
            //Debug.Log(bonusDamage + " DAMAGE FROM WEAPONS"); //DEBUG
            Debug.Log(caster.name.ToUpper() + " CASTING NORMAL SKILL " + name.ToUpper()); //DEBUG
#endif
            #endregion
        }


        // L A U N C H  P R O J E C T I L E

        if (launchedProjectile != null)
        {
            //if (!caster.rightHandEffectMount) { caster.rightHandEffectMount = caster.gameObject.transform; } //DEPRECIATED: replaced with launchOrigin
            GameObject go = Instantiate(launchedProjectile.gameObject, caster.launchOrigin.position, caster.launchOrigin.rotation);
            if (go != null)
            {
                LaunchedProjectile effect = go.GetComponent<LaunchedProjectile>();
                if (effect != null)
                {
                    effect.target = caster.target;
                    effect.caster = caster;

                    //effect.projectileDamage = modifiedSkillDamage;
                    effect.projectileDamage.status = modifiedSkillDamage.status;

                    //DAMAGE
                    effect.projectileDamage.min += modifiedSkillDamage.min;
                    effect.projectileDamage.max += modifiedSkillDamage.max;
                    effect.projectileDamage.bonus += modifiedSkillDamage.bonus;
                    effect.projectileDamage.multiplier *= modifiedSkillDamage.multiplier;

                    //NOTE: Physical Projectiles will inherit the damage method of the launcher
                    if (effect.projectileDamage.method == MethodOfDamage.Physical)
                    {
                        effect.projectileDamage.method = modifiedSkillDamage.method;
                    }

                    //NOTE: Neutral Projectiles will inherit the damage element of the launcher
                    if (effect.projectileDamage.element == Element.Neutral)
                    {
                        effect.projectileDamage.element = modifiedSkillDamage.element;
                    }

                    foreach (ScriptedDamage damage in modifiedSkillDamage.damageOverTime)
                    {
                        if (!effect.onStrikeDamage.Contains(damage)) effect.onStrikeDamage.Add(damage);
                    }

                    effect.projectileSpeed = launchedProjectileSpeed;
                }
                NetworkServer.Spawn(go); //TODO: Evaluate this - for now it launches any game object even if it won't do damage
            }
        }
        else
        {
            caster.DealCombatDamage(caster.target, modifiedSkillDamage);
            #region DEBUG
#if UNITY_EDITOR
            //TODO: Write this to chat damage log.
            Debug.Log(caster.name + " dealing " + name + " damage to " + caster.target.name);// + " with a roll of " + modifiedSkillDamage.total.ToString() + " base damage");
#endif
            #endregion
        }

        base.RefreshLastTarget(caster); //REVERT TO ORIGINAL TARGET
    }
}


//{ //DEPRECIATED
// target still around?
//    if (caster.target != null)
//    {
//        destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
//        return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
//    }
//    destination = caster.transform.position;
//    return false;
//}


/* //DEPRECIATED - Handled above now
        ScriptableDamageSkill ammoSkill = (weapon.ammunition[0].attachedSkill as ScriptableDamageSkill);
        if (ammoSkill != null)
        {
            // A M M U N I T I O N  D A M A G E

            //AMMO DAMAGE
            if (weaponConfig.addDamage)
            {
                modifiedSkillDamage.min += ammoSkill.damage.Get(skillLevel);
                modifiedSkillDamage.max += ammoSkill.damage.Get(skillLevel);

                modifiedSkillDamage.min += ammoSkill.skillDamage.min;
                modifiedSkillDamage.max += ammoSkill.skillDamage.max;

                modifiedSkillDamage.bonus += ammoSkill.skillDamage.bonus;
                modifiedSkillDamage.multiplier *= ammoSkill.skillDamage.multiplier;
            }
            //AMMO DAMAGE METHOD
            if (weaponConfig.useWeaponDamageMethod) { modifiedSkillDamage.method = ammoSkill.skillDamage.method; }
            //AMMO ELEMENT
            if (weaponConfig.useWeaponElement) { modifiedSkillDamage.element = ammoSkill.skillDamage.element; }
            //AMMO DOTs
            if (weaponConfig.addDOT)
            {
                modifiedSkillDamage.damageTickInterval = System.Math.Max(modifiedSkillDamage.damageTickInterval, ammoSkill.skillDamage.damageTickInterval);
                modifiedSkillDamage.damageOverTime.AddRange(ammoSkill.skillDamage.damageOverTime);
            }
            //AMMO STATUS EFFECTS
            if (weaponConfig.addStatusEffects)
            {
                modifiedSkillDamage.status.statusEffects.AddRange(ammoSkill.skillDamage.status.statusEffects);
            }
            //AMMO COOLDOWN
            if (weaponConfig.addCooldown)
            {
                addedCooldown += (ammoSkill.cooldown.baseValue * weapon.attackSpeedMultiplier);
            }

            //if (addsWeaponDamageMethod) { modifiedSkillDamage.method = skill.skillDamage.method; }
            //if (addsWeaponElement) { modifiedSkillDamage.element = skill.skillDamage.element; }
            //if (addsWeaponDOT) { modifiedSkillDamage.damageOverTime.AddRange(skill.skillDamage.damageOverTime); }


#region DEBUG
#if UNITY_EDITOR
            Debug.Log("ADDED: " + ammoSkill.ToString().ToUpper() + "<DAMAGE>" + "([" + modifiedSkillDamage.min + "D" + Mathf.RoundToInt(modifiedSkillDamage.max / ((modifiedSkillDamage.min > 0) ? modifiedSkillDamage.min : 1)) + "]*" + modifiedSkillDamage.multiplier + ")+" + modifiedSkillDamage.bonus + "=" + modifiedSkillDamage.total); //DEBUG
#endif
#endregion
        }*/


