using Mirror;
using System.Collections.Generic;

public partial class PlayerCharacter : CharacterSheet
{
    // R E S E T  A C C E S S O R I E S
    [Server] public void ResetAccessories()
    {
        if (equippedAccessories == null) equippedAccessories = new List<CombatAccessory>();
        else equippedAccessories.Clear();
    }
}
