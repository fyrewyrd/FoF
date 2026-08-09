using System;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName = "DX4D/STATUS/Status Effect", order = 40)]
[Serializable] public class ScriptedStatusEffect : ScriptableObject
{
    //TARGETS
    [Header("TARGETING")]
    [SerializeField, HideInInspector]
    CharacterSheet caster;
    [SerializeField, HideInInspector]
    CharacterSheet target;

    [Header("TIMING")]
    [SerializeField] public bool onApply = true;
    [SerializeField] public bool onExpired = false;
    [SerializeField] public bool onTick = false;

    [Header("STATUS EFFECT TAG")]
    [SerializeField] public StatusEffect statusEffect;

    [Header("CHANCE TO APPLY")]
    [SerializeField] public StatusProbability probability;

    [Header("DURATION")]
    [SerializeField] public StatusDuration duration;
    [SerializeField, HideInInspector] public double expiration;
    
    [SerializeField, HideInInspector] int level = 0;

    public ScriptedStatusEffect() : this(StatusEffect.None, 1.0f, 3.0f) { }
    public ScriptedStatusEffect(StatusEffect status, float statusProbability, float statusDuration)
    {
        statusEffect = status;
        probability = new StatusProbability(statusProbability);
        duration = new StatusDuration(statusDuration);
        expiration = 0;
    }

    [Serializable] public struct StatusProbability
    {
        //public StatusProbability() : this(0.0f) { }
        public StatusProbability(float applicationChance) { chance = applicationChance; }

        [SerializeField] public float chance;
    }

    [Serializable] public struct StatusDuration
    {
        //public StatusDuration() : this(1.0f) { }
        public StatusDuration(float baseDuration = 1.0f, float bonusPerLevel = 0.01f)
        {
            duration.baseValue = baseDuration;
            duration.bonusPerLevel = bonusPerLevel;
        }

        [SerializeField] public LinearFloat duration;
        [SerializeField] public float GetDuration(int level) { return (duration.baseValue + (duration.bonusPerLevel * level)); }
    }
    public void Activate(CharacterSheet attacker, CharacterSheet defender)
    {
        if (attacker == null || defender == null) return; //No caster or target

        caster = attacker;
        target = defender;

        level = caster.level;
        expiration = NetworkTime.time + duration.GetDuration(level);

        OnApplied();
    }
    public void Deactivate()
    {
        level = 0;
        expiration = NetworkTime.time;
        OnExpired();
    }

    public virtual void OnApplied() { }
    public virtual void OnExpired() { }
    public virtual void OnTick() { }

    public override string ToString()
    {
        string convertedString = "";
        if (probability.chance > 0.0f) convertedString += "\n" + (probability.chance * 100) + "% chance to " + statusEffect.ToString() + " for " + duration.GetDuration(level) + " seconds";
        return convertedString;
    }
}
