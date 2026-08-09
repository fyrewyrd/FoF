public partial class PlayerCharacter : CharacterSheet
{
    //SIEGE WEAPON
    public bool HasSiegeWeapon { get { return (siegeWeapon != null); } }
    //MAIN WEAPON
    public bool HasMainWeapon { get { return (mainWeapon != null); } }
    //public bool HasNoWeapon { get { return (!mainWeapon); } }
    //OFFHAND WEAPON
    public bool HasOffhandWeapon { get { return (offhandWeapon != null); } }
    //public bool HasNoOffhandWeapon { get { return (!offhandWeapon); } }
    //AMMO
    //public bool HasAmmo { get { return (!HasNoAmmo); } }
    //public bool HasNoAmmo { get { return (!ammo || ammo.weaponHand != WeaponHand.Ammo); } }
    //THROWN
    //public bool HasThrownWeapon { get { return (!HasNoThrownWeapon); } }
    //public bool HasNoThrownWeapon { get { return (!ammo || ammo.weaponHand != WeaponHand.Thrown); } }
    
    //UNARMED
    public bool IsUnarmed
    {
        get { return ( !_mainWeapon || mainWeapon.weaponHand == WeaponHand.Unarmed)
                && ( !_offhandWeapon || offhandWeapon.weaponHand == WeaponHand.Unarmed)
                && ( !_siegeWeapon || siegeWeapon.weaponHand == WeaponHand.Unarmed)
                ; }
    }
    //ONE HANDED
    public bool HasOneHandedWeapon
    {
        get { return (HasMainWeapon && mainWeapon.weaponHand == WeaponHand.LeftHanded) || (HasOffhandWeapon && offhandWeapon.weaponHand == WeaponHand.RightHanded); }
    }
    //TWO HANDED
    public bool HasTwoHandedWeapon
    {
        get { return ( (HasMainWeapon && mainWeapon.weaponHand == WeaponHand.TwoHanded) || (HasOffhandWeapon && offhandWeapon.weaponHand == WeaponHand.TwoHanded)); }
    }
    //DUAL WIELD
    public bool IsDualWielding { get {
            if (!combat.CanDualWield) return false;
            if (HasTwoHandedWeapon && !combat.CanDualWieldLargeWeapons) return false;
            return (HasOffhandWeapon);
        } }

    // I S  W E A R I N G  E N C H A N T E D  W E A P O N
    //public bool IsUsingWeapon { get { return (equippedWeapons.Count > 0); } }
    // I S  N O T  W E A R I N G  E N C H A N T E D  W E A P O N
    //public bool IsNotUsingWeapon { get { return (!IsUsingWeapon); } }
}
