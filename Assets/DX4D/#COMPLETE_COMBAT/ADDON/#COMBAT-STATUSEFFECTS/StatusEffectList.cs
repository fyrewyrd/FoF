using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "DX4D/STATUS/Status Effect List", order = 40)]
[Serializable] public class StatusEffectList// : ScriptableObject
{
    //[Header("Active Status Effects")]
    [SerializeField] public List<ScriptedStatusEffect> statusEffects = new List<ScriptedStatusEffect>();

    //CONSTRUCTOR
    public StatusEffectList() { } // : this() { }
    public StatusEffectList(List<ScriptedStatusEffect> effectList)
    {
        statusEffects = effectList;
    }
    public StatusEffectList copy()
    {
        StatusEffectList info = (StatusEffectList)this.MemberwiseClone();

        info.statusEffects = statusEffects;

        return info;
    }

    public void ActivateAll(StatusEffect statusEffect, CharacterSheet attacker, CharacterSheet defender)
    {
        foreach (ScriptedStatusEffect info in statusEffects)
        {
            if (info.statusEffect == statusEffect) { info.Activate(attacker, defender); }
        }
    }
    public void DeactivateAll(StatusEffect statusEffect)
    {
        foreach (ScriptedStatusEffect info in statusEffects)
        {
            if (info.statusEffect == statusEffect) { info.Deactivate(); }
        }
    }
}