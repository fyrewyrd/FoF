using UnityEngine;

public partial class EquipmentItem : UsableItem
{
    [Header(" [ WEAPON MODEL ] ")]
    [Tooltip("Hide/Show the weapon when it is equipped.\n(does not change already equipped gear until it is reequipped)")]
    [SerializeField] public bool showWeaponModel = true;
    [Tooltip("Offsets the position of the equipment model\n(does not change already equipped gear until it is reequipped)")]
    [SerializeField] public Vector3 weaponModelOffset = Vector3.zero;
    //[SerializeField] public Quaternion weaponModelRotation = Quaternion.identity; //TODO? This is data heavy so I left it out for now
    [Header(" [ PLAYER RESKIN ] ")]
    [Tooltip("Gives the equipped character a new skin when this gear is equipped.\n(does not change already equipped gear until it is reequipped)")]
    [SerializeField] public Material changeToPlayerSkin;
}