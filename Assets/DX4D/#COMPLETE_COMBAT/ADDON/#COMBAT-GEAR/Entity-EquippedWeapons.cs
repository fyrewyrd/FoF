using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    [Header("EQUIPPED WEAPONS")]
    //MAINHAND
    [SerializeField] CombatWeapon _mainWeapon = null;
    public CombatWeapon mainWeapon
    {
        //get { return ( !_mainWeapon && !_offhandWeapon) ? _unarmedWeapon : _mainWeapon; }
        get { return _mainWeapon; }
        set { _mainWeapon = value; }
    }
    //OFFHAND
    [SerializeField] CombatWeapon _offhandWeapon = null;
    public CombatWeapon offhandWeapon
    {
        //get { return (!_mainWeapon && !_offhandWeapon) ? _unarmedWeapon : _offhandWeapon; }
        get { return _offhandWeapon; }
        set { _offhandWeapon = value; }
    }
    //UNARMED
    [SerializeField] CombatWeapon _unarmedWeapon = null;
    public CombatWeapon unarmedWeapon
    {
        get { return _unarmedWeapon; }
        set { _unarmedWeapon = value; }
    }
    //SIEGE
    [SerializeField] CombatWeapon _siegeWeapon = null;
    public CombatWeapon siegeWeapon
    {
        get { return _siegeWeapon; }
        set { _siegeWeapon = value; }
    }
}
