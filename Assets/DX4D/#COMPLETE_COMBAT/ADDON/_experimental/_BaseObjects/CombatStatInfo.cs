using System;
using UnityEngine;

[Serializable]
public struct CombatStatInfo
{
    #region C O N S T R U C T O R S
    public CombatStatInfo(MethodOfDamage dmgMethod, Element dmgElement) : this(dmgMethod, dmgElement, 1.0f, 0, 1.0f, 0) { }
    public CombatStatInfo(MethodOfDamage dmgMethod, Element dmgElement, float dmgMultiplier, int dmgBonus, float dmgVulnerability, int dmgReduction)
    {
        //ELEMENT
        method = dmgMethod;
        element = dmgElement;
        //DAMAGE
        damageMultiplier = dmgMultiplier;
        damageBonus = dmgBonus;
        //DEFENSE
        vulnerability = dmgVulnerability;
        damageReduction = dmgReduction;
    }
    #endregion

    public MethodOfDamage method;
    public Element element;

    [Tooltip("1.0f represents normal damage, 0.5f would be half damage, and 2.0f would be double damage")]
    [Range(0.01f, 10.0f)] public float damageMultiplier;
    public int damageBonus;

    [Tooltip("1.0f represents normal damage, 0.5f would be half damage, and 2.0f would be double damage")]
    [Range(0.01f, 10.0f)] public float vulnerability;
    public int damageReduction;
}
