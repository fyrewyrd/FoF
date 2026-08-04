using System;
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/PROJECTILE/Projectile VFX", order = 53)]
[Serializable] public class ScriptedProjectileVFX : ScriptableObject
{
    [Header("PROJECTILE JITTER")]
    public bool applyJitterOnStrike = true;
    public float jitterAmount = 0.1f;
    [Range(0.01f, 0.99f)] public float jitterIntensity = 0.5f;

    [Header("CASTER VISUAL EFFECTS")]
    public GameObject onFiredVFX;

    [Header("TARGET VISUAL EFFECTS")]
    public GameObject onStrikeVFX;
    public GameObject onDestroyedVFX;
    public GameObject AOETargetVFX;

    [Header("PROJECTILE VISUAL EFFECTS")]
    public GameObject projectileMountedVFX;

}
