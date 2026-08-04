using Mirror;
using UnityEngine;

public partial class Gear : NetworkBehaviour
{
    public const long DEFAULT_MAX_EQUIPMENT = 20;// long.MaxValue;

    [Header(" [EQUIP SLOTS] ")]
    public EquipmentInfo[] slots = {
        new EquipmentInfo{requiredCategory="WeaponSiege", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="WeaponUnarmed", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Weapon", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Weapon", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Accessory", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Head", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Chest", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Legs", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Shield", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Shoulders", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Hands", location=null, defaultItem=new ScriptableItemAndAmount()},
        new EquipmentInfo{requiredCategory="Feet", location=null, defaultItem=new ScriptableItemAndAmount()}
    };

    [Header(" [EQUIPMENT CAPACITY] ")] [Tooltip("Default = 20")]
    [SerializeField] protected LinearLong _equipmentMax =
        new LinearLong { baseValue = DEFAULT_MAX_EQUIPMENT };
    public LinearLong equipmentMax { get { return _equipmentMax; } }
    
    public SyncListItemSlot equipment = new SyncListItemSlot();
}

public partial class CharacterSheet : NetworkBehaviour
{
    public SyncListItemSlot EQUIPMENT
    {
        get { return gear.equipment; }
        set { gear.equipment = value; }
    }

    // equipment ///////////////////////////////////////////////////////////////
    public int GetEquipmentIndexByName(string itemName)
    {
        return EQUIPMENT.FindIndex(slot => slot.amount > 0 && slot.item.name == itemName);
    }

    // helper function to find the equipped weapon index
    // -> works for all entity types. returns -1 if no weapon equipped.
    public int GetWeaponIndex()
    {
        return EQUIPMENT.FindIndex(slot => slot.amount > 0 &&
                                           slot.item.data is WeaponItem);
    }

    // get currently equipped weapon category to check if skills can be casted
    // with this weapon. returns "" if none.
    public string GetEquippedWeaponCategory()
    {
        // find the weapon slot
        int index = GetWeaponIndex();
        return index != -1 ? ((WeaponItem)EQUIPMENT[index].item.data).category : "";
    }
}