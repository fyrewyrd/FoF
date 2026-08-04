#define DEPRECIATED

#if !DEPRECIATED
using Mirror;
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "DX4D/STATUS/Scripted Status Effect", order = 999)]
public class ScriptedStatusEffect : ScriptableObject
{
    private void OnEnable()
    {
        isApplied = true;
    }
    
    private void OnDisable()
    {
        isApplied = false;
    }

    [SerializeField] bool isApplied = false;
    [SerializeField] public StatusEffect effectType;
    [SerializeField] public float effectIntensity = 1.0f;
    [SerializeField] public double effectDuration = 10.0f;
}
#endif

/*
public partial class Entity
{
    //[SerializeField] List<StatusEffect> activeStatusEffects = new List<StatusEffect>();
    //CHECK
    //[Server] public bool HasStatus(StatusEffect statusEffect) { return activeStatusEffects.Contains(statusEffect); }

    public bool HasNoStatusEffects
    {
        get
        {
            return (activeStatusEffects == null || activeStatusEffects.Count < 1);
        }
    }
    //ADD/REMOVE

    //[Server] public bool Addtatus(StatusEffect statusEffect)
    //{
    //    return AddStatusEffect(statusEffect);
    //}
    //[Server] public bool RemoveStatus(StatusEffect statusEffect)
    //{
    //    return RemoveStatusEffect(statusEffect);
    //}

    [Server] public bool ApplyStatusEffect(StatusEffect statusEffect)
    {
        if (!activeStatusEffects.Contains(statusEffect)) {
            activeStatusEffects.Add(statusEffect);
        }
        return activeStatusEffects.Contains(statusEffect);
    }
    [Server] public bool ClearStatusEffect(StatusEffect statusEffect)
    {
        if (!activeStatusEffects.Contains(statusEffect)) return false;
        return activeStatusEffects.Remove(statusEffect);
    }
    [Server] public void ClearAllStatusEffects()
    {
        if (activeStatusEffects.Count > 0) activeStatusEffects.Clear();
    }
}
*/
