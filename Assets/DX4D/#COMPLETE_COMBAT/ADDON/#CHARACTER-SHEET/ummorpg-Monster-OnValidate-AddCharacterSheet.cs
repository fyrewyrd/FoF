using Mirror;
using UnityEngine;

public partial class Monster : Entity
{
    // This flag prevents the warning from spamming every frame in the Editor
    private bool validated = false;

    private void OnValidate()
    {
        // Never run validation code during Play mode (prevents NullReference with Mirror)
        if (Application.isPlaying)
            return;

        // Safe guard: only run if we actually have a NetworkIdentity
        if (GetComponent<NetworkIdentity>() == null)
            return;

        // Your original logic (without isClient)
        if (!validated)
        {
            if (character != null)
            {
                validated = true;
                return;
            }

            // Should trigger an AddComponent call in the editor just by this query
            if (character == null)
            {
                Debug.LogWarning("CRITICAL WARNING: " + name + ": Could not add CharacterSheet component...you might need to add it manually.");
            }
        }
    }
}