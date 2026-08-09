using UnityEngine;
using System.Collections.Generic;

public abstract partial class ActiveSkill : ScriptableSkill
{
    //DEFAULT ATTACK RANGE
    [Header("ATTACK RANGE")]
    [Tooltip("Use this range when one is not specified.\ndefault:1.25f")]
    public const float defaultAttackRange = 1.25f;
    [Header("MOVE TO OPTIMAL RANGE")]
    [Tooltip("Move to the target when using this skill?")]
    [SerializeField] public bool approachTarget = true;
    [Header("AUTOMATIC TARGET SELECTION")]
    [Tooltip("Automatically select a nearby target when using this skill?")]
    [SerializeField] public bool autoTargeting = true;

    //[Tooltip("Automatically searches for a nearby Monster to target with this skill")]
    //[SerializeField] public bool autoTargeting = true;
    [Header("TARGETING SETUP")]
    [SerializeField] public bool autoTargetSelf = false;
    [SerializeField] public bool canTargetCorpses = false;
    CharacterSheet lastTarget;

    [Header("CASTING COST")]
    [SerializeField] public CastingCost costs;

    [Header("VISUAL EFFECTS")]
    public GameObject onCastStartVisualEffect;
    public GameObject onCastFinishVisualEffect;
    public GameObject onHitVisualEffect; //Triggered in Apply() in case the cast is aborted before application.
    
    public virtual bool CheckSelf(CharacterSheet caster, int skillLevel)
    {
        PlayerCharacter casterAsPlayer = null;
        if (caster is PlayerCharacter) casterAsPlayer = (caster as PlayerCharacter);

        //AUTOTARGET
        if (!caster.target && autoTargeting)
        {
            // find all monsters that are alive, sort by distance
            //GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster"); //DEPRECIATED - Gets ALL the monsters?
            List<CharacterSheet> objects = caster.ai.FindAggroTargets(caster.ai.detectionRadius);// GameObject.FindGameObjectsWithTag("Monster");


            //caster.player.SetIndicatorViaParent(sorted[0].transform); //TODO Selection Indicator
            caster.player.CmdSetTarget(caster.ai.FindClosestCharacter(objects).gameObject);
        }

        return (!caster.IsDead &&
            //equipment category
            caster.GetEquippedWeaponCategory().StartsWith(requiredWeaponCategory)
            //casting costs
            && caster.CanPayCastingCost(costs)
            );
    }

    public override void OnCastStarted(CharacterSheet castBy)
    {
        CharacterSheet caster = castBy;
        if (!caster) return; //VALIDATE

        if (onCastStartVisualEffect != null) caster.vfx.TriggerVisualEffect(caster.transform, onCastStartVisualEffect); //TRIGGER VFX
        
        //base.OnCastStarted(castBy);

        //PLAY CAST SOUNDS
        if (caster.audioSource != null && castSound != null)
            caster.audioSource.PlayOneShot(castSound);
    }

    public void RefreshLastTarget(CharacterSheet caster)
    {
        if (caster != null && lastTarget != null) caster.target = lastTarget;
    }

    public bool ValidateTarget( CharacterSheet caster, CharacterSheet target )
    {
        //VALIDATE CASTER AND TARGET
        if (!caster) { return false; } //NO CASTER

        //DEAD CASTER
        if (caster.IsDead)
        {
            caster.TargetShowTextPopup("you are dead"); return false;
        }
        //TARGET SELF
        if (autoTargetSelf)
        {
            if (target != null) lastTarget = target;
            target = caster;
        }
        //NO TARGET
        if (!target)
        {
            caster.TargetShowTextPopup("invalid target"); return false;
        }
        //DEAD TARGET
        if (target.IsDead)
        {
            if (!canTargetCorpses) caster.TargetShowTextPopup("your target is dead"); return false;
        }
        #region DEBUG
#if UNITY_EDITOR
        Debug.Log(caster.name + " can cast " + name + " on " + target.name);
#endif
        #endregion
        return true;
    }

    public override void OnCastFinished(CharacterSheet caster)
    {
        if (!caster) return;
        if (caster.vfx != null)
        {
            if (onCastFinishVisualEffect != null) caster.vfx.TriggerVisualEffect(caster.transform, onCastFinishVisualEffect); //TRUGGER VFX

            if (caster.target != null)
            {
                if ((canTargetCorpses || !caster.target.IsDead) && onHitVisualEffect != null) caster.vfx.TriggerVisualEffect(caster.target.transform, onHitVisualEffect);
            }
        }
    }

    //TOOLTIP
    public override string ToolTip(int level, bool showRequirements = false)
    {
        System.Text.StringBuilder tip = new System.Text.StringBuilder(toolTip);

        tip.Append("<b>{NAME} (lvl {LEVEL})</b>");
        tip.Append("\nRange {RANGE}");
        tip.Append("\nCast Duration {CASTTIME} - Cooldown {COOLDOWN}");

        tip.Append(Tooltip.CastingCostToolTip(costs));

        tip.Replace("{NAME}", name);
        tip.Replace("{LEVEL}", level.ToString());
        tip.Replace("{CASTTIME}", Utils.PrettySeconds(castTime.Get(level)));
        tip.Replace("{COOLDOWN}", Utils.PrettySeconds(cooldown.Get(level)));
        tip.Replace("{RANGE}", castRange.Get(level).ToString());

        // only show requirements if necessary
        if (showRequirements)
        {
            tip.Append("\n<b><i>Required Level: " + requiredLevel.Get(level) + "</i></b>\n" +
                       "<b><i>Required Skill Exp.: " + requiredSkillExperience.Get(level) + "</i></b>\n");
            if (predecessor != null)
                tip.Append("<b><i>Required Skill: " + predecessor.name + " Lv. " + predecessorLevel + " </i></b>\n");
        }

        return tip.ToString(); 
    }
}
