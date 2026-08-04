using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    // R E S E T  W E A P O N S
    [Server] public void ResetWeapons()
    {
        mainWeapon = null;
        offhandWeapon = null;
        unarmedWeapon = null;
        siegeWeapon = null;
    }
}