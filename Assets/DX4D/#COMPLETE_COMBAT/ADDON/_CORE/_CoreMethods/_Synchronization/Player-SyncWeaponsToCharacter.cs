using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    // S Y N C  W E A P O N
    [Server] public void SyncWeaponsToCharacter()
    {
        if (HasSiegeWeapon)
        {
            SetDamage(siegeWeapon.damage.method, siegeWeapon.damage.element);
        }
        else if (HasMainWeapon)
        {
            SetDamage(mainWeapon.damage.method, mainWeapon.damage.element);
        }
        else if (HasOffhandWeapon)
        {
            SetDamage(offhandWeapon.damage.method, offhandWeapon.damage.element);
        }
        else if (IsUnarmed) //UNARMED & NON DX4D WEAPONS
        {
            if (unarmedWeapon != null)
            {
                SetDamage(unarmedWeapon.damage.method, unarmedWeapon.damage.element);
            }
        }
        else //FALLBACK TO DEFAULT DAMAGE
        {
            ResetDamage();
        }
    }
}