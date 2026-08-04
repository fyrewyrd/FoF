using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    ///Fires the first round in the weapon's ammoRounds list [0]
    [Server] public bool FireWeapon(CombatWeapon weapon)
    {
        return FireWeapon(weapon, 0);
        //weapon.ammunition.RemoveAt(0);
    }
    [Server] public bool FireWeapon(CombatWeapon weapon, int roundIndex)
    {
        return (weapon != null
            && weapon.ammunition != null
            && weapon.ammunition.Count > (roundIndex)
            && weapon.ammunition[roundIndex] != null
            && weapon.ammunition[roundIndex].FireFromWeapon(this, weapon, roundIndex)
            );
    }
    //[Server] public void FireOffhandWeapon()
    //{
    //    if (HasOffhandWeapon && mainWeapon.requiresAmmo && offhandWeapon.HasAmmo) offhandWeapon.ammunition[0].FireFromWeapon( this, offhandWeapon );
    //}
}
