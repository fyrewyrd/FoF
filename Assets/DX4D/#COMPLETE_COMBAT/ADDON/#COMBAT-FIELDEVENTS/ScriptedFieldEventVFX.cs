using UnityEngine;
//using System.Collections.Generic;

[CreateAssetMenu(menuName = "DX4D/FIELD EVENT/Field Event VFX", order = 62)]
public class ScriptedFieldEventVFX : ScriptableObject
{
    [Header("VISUAL EFFECTS")]
    [Tooltip("Once this field is created, the onCreatedVFX is spawned."
        + "\nNOTE: This field can contain any game object, you are not limited to VFX only."
        + "\n\nALERT: It is HIGHLY recommended to add a DestroyAfter component to any VFX to prevent memory leaks.")]
    public GameObject onCreatedVFX;

    [Tooltip("Once this field expires, the onDestroyedVFX is spawned."
        + "\nNOTE: This field can contain any game object, you are not limited to VFX only."
        + "\n\nALERT: It is HIGHLY recommended to add a DestroyAfter component to any VFX to prevent memory leaks.")]
    public GameObject onDestroyedVFX;
}

//[CreateAssetMenu(menuName = "DX4D/DAMAGE/Damage Field", order = 33)]
//public class ScriptedSpawnField : ScriptedEffectField
//{
//    public override void Trigger(CharacterSheet owner, List<CharacterSheet> targets)
//    {
//base.Trigger(owner, targets);
//    }
//}