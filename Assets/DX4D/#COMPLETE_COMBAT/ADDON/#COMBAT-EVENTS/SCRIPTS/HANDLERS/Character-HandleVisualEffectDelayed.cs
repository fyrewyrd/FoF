using Mirror;
using UnityEngine;
using System.Collections;

public partial class CharacterSheet : NetworkBehaviour
{
    // D E L A Y E D  T E X T  P O P U P
    [Client] IEnumerator HandleVisualEffectDelayed(GameObject visualEffectPrefab, float delay)
    {
        if (delay <= 0) delay = 0.1f; //DISALLOW "MACHINE GUN" INSTANT TEXT

        yield return new WaitForSeconds(delay);

        HandleVisualEffect(visualEffectPrefab);
    }
}
