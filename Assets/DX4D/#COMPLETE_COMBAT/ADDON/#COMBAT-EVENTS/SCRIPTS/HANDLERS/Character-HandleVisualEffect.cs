using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //VFX
    [Client] public void HandleVisualEffect(GameObject visualEffectPrefab)
    {
        if (visualEffectPrefab != null) vfx.TriggerVisualEffect(transform, visualEffectPrefab);
    }
}
