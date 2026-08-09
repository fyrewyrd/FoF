using Mirror;
using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    [Header("WEAPON VISUAL FX")]
    [SerializeField] public bool showWeaponVFX = false;
    [SerializeField] public ParticleSystem mainWeaponVFX;
    [SerializeField] public ParticleSystem offhandWeaponVFX;

    /*
    private void OnValidate()
    {
        if (!showWeaponVFX &&
            (mainWeaponVFX != null || offhandWeaponVFX != null))
                { showWeaponVFX = true; }
    }
    */

    private void LateUpdate_SyncVisualEffects()
    {
        SyncVisualEffectsToCharacter();
    }

    // S Y N C
    //VISUAL EFFECTS
    public void SyncVisualEffectsToCharacter()
    {
        if (!showWeaponVFX) return;

        if (HasSiegeWeapon)
        {
            //STOP ATTACHED PARTICLE SYSTEMS
            if (rightHandEffect != null)
            {
                rightHandEffect.gameObject.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                rightHandEffect = null;
                //Debug.Log(name.ToUpper() + "RIGHT HAND PARTICLES REMOVED"); //DEBUG
            }
            //SHOW VFX
            if (siegeWeapon && siegeWeapon.constantVisualEffect != null) rightHandEffect = leftHandEffect = siegeWeapon.constantVisualEffect;
        }
        else if (!HasMainWeapon)
        {
            //STOP ATTACHED PARTICLE SYSTEMS
            if (rightHandEffect != null)
            {
                rightHandEffect.gameObject.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                rightHandEffect = null;
                //Debug.Log(name.ToUpper() + "RIGHT HAND PARTICLES REMOVED"); //DEBUG
            }
            //SHOW VFX
            if(unarmedWeapon && unarmedWeapon.constantVisualEffect != null) rightHandEffect = leftHandEffect = unarmedWeapon.constantVisualEffect;

        }
        else// if (HasMainWeapon)
        {
            //SHOW VFX
            if (rightHandEffect != null) return;
            rightHandEffect = mainWeapon.constantVisualEffect;
        }

        if (!IsDualWielding || !HasOffhandWeapon)
        {
            //STOP ATTACHED PARTICLE SYSTEMS
            if (leftHandEffect != null)
            {
                //Debug.Log(name.ToUpper() + "LEFT HAND PARTICLES REMOVED"); //DEBUG
                leftHandEffect.gameObject.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                leftHandEffect = null;
            }
        }
        else if (IsDualWielding && !HasSiegeWeapon)
        {
            //SHOW VFX
            if (leftHandEffect != null) return;
            leftHandEffect = offhandWeapon.constantVisualEffect;
        }
        
        //VISUAL EFFECTS
        if (rightHandEffect != null) vfx.MountVisualEffect(rightHandEffectMount, rightHandEffect);
        if (leftHandEffect != null) vfx.MountVisualEffect(leftHandEffectMount, leftHandEffect);
    }
}
