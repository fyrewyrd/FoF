using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    // R E S E T  E N T I T Y  D A M A G E
    /// <summary>Resets this Entity's damage to its innate damage types.</summary>
    [Server] public void ResetDamage()
    {
        SetDamage(combat.innateMethodOfDamage, combat.innateDamageElement);
    }
}
