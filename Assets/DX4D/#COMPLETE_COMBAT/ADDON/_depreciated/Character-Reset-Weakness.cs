/* //DEPRECIATED
using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // R E S E T  E N T I T Y  R E S I S T S
    /// <summary>SERVER</summary>
    [Server] public void ResetWeakness()
    {
        weakness.weakToDamage = MethodOfDamage.NoDamage;
        weakness.veryWeakToDamage = MethodOfDamage.NoDamage;
        weakness.weakToElement = Element.Neutral;
        weakness.veryWeakToElement = Element.Neutral;
    }
}
*/