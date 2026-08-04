using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //VFX
    [ClientRpc] public void RpcShowVisualEffect(GameObject visualEffectPrefab)
    {
        HandleVisualEffectDelayed(visualEffectPrefab, 0);
    }
    [ClientRpc] public void RpcShowVisualEffects(GameObject[] visualEffectPrefabs, float interval)
    {
        for (int i = 0; i < visualEffectPrefabs.Length; i++)
        {
            HandleVisualEffectDelayed(visualEffectPrefabs[i], (interval * i));
        }
    }
}
