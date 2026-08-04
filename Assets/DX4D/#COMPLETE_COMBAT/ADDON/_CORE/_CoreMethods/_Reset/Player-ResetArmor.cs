using Mirror;
using System.Collections.Generic;

public partial class PlayerCharacter : CharacterSheet
{
    // R E S E T  A R M O R
    [Server] public void ResetArmor()
    {
        if (equippedArmors == null) equippedArmors = new List<CombatArmor>();
        else equippedArmors.Clear();
    }
}
