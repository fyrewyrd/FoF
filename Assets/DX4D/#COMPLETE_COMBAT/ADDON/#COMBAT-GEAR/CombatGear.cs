using System.Text;
using UnityEngine;

public abstract partial class CombatGear : EquipmentItem
{
    //VFX
    [Header(" [ VISUAL EFFECTS ] ")]
    [SerializeField] public GameObject constantVisualEffect;

    //DESCRIPTION
    [Header(" [ DESCRIPTION ] ")]
    [SerializeField] [TextArea(1, 15)] public string description;

    //CRAFTSMANSHIP
    [Header(" [ CRAFTSMANSHIP ] ")]
    [SerializeField] public int level = 1;
    [SerializeField] public GearRarity gearRarity = GearRarity.Common;
    [SerializeField] public CraftingMaterialType material = CraftingMaterialType.Metal;

    //AUTOMATION SETUP
    [Header(" [ AUTOMATION ] ")]
    [Tooltip("Automatically set the Category variable?\n(disable for compatability with addons like Dagger Combat)")]
    [SerializeField] public bool automaticCategory = true;
    [Tooltip("Automatically generate this item's name?")]
    [SerializeField] public bool automaticName = false;
    [Tooltip("Automatically generate tooltips for this item?")]
    [SerializeField] public bool automaticTooltip = true;

    //TOOLTIP SETUP
    [Tooltip("Display item details in the tooltip for this item?")]
    [SerializeField] public bool showItemDetails = true;

    //DURABILITY
    [Header(" [ DURABILITY ] ")]
    [SerializeField] DurabilityManager durabilityManager;
    [SerializeField] int _breakability = 1;
    internal int breakabilityNow
    {
        get
        {
            Player p = Local.player;
            if (!p) return _breakability;

            if (!durabilityManager) durabilityManager = p.GetComponent<DurabilityManager>();
            if (!durabilityManager) return _breakability;

            return durabilityManager.decayRate;
        }
        set
        {
            Player p = Local.player;
            if (!p) { _breakability = value; return; }

            if (!durabilityManager) durabilityManager = p.GetComponent<DurabilityManager>();
            if (!durabilityManager) { _breakability = value; return; }

            _breakability = durabilityManager.current = value;
        }
    }
    public int breakability { get { return _breakability; } set { _breakability = value; } }


    [SerializeField] int _durability = 100;
    internal int durabilityNow
    {
        get
        {
            Player p = Local.player;
            if (!p) return _durability;

            if (!durabilityManager) durabilityManager = p.GetComponent<DurabilityManager>();
            if (!durabilityManager) return _durability;

            return durabilityManager.current;
        }
        set
        {
            Player p = Local.player;
            if (!p) { _durability = value; return; }

            if (!durabilityManager) durabilityManager = p.GetComponent<DurabilityManager>();
            if (!durabilityManager) { _durability = value; return; }

            //SYNC
            _breakability = durabilityManager.decayRate;
            _maxDurability = durabilityManager.max;
            _durability = durabilityManager.current = value;
        }
    }
    public int durability { get { return _durability; } set { _durability = value; } }

    public void DamageDurability()
    {
        _durability = durabilityNow -= breakabilityNow;
    }

    [SerializeField] int _maxDurability = 100;
    internal int maxDurabilityNow
    {
        get
        {
            Player p = Local.player;
            if (!p) return _maxDurability;

            if (!durabilityManager) durabilityManager = p.GetComponent<DurabilityManager>();
            if (!durabilityManager) return _maxDurability;

            return durabilityManager.max;
        }
    }
    public int maxDurability { get { return _maxDurability; } set { _maxDurability = value; } }

    // V A L I D A T E
    public void OnValidate()
    {
        if (automaticCategory)
        {
            if (this is CombatWeapon)
            {
                CombatWeapon weapon = (this as CombatWeapon);
                category = weapon.weaponHand.ToString() + weapon.weaponCategory.ToString();
            }
            else if (this is CombatArmor)
            {
                category = (this as CombatArmor).armorLocation.ToString();
            }
            else if (this is CombatAccessory)
            {
                category = "Accessory" + (this as CombatAccessory).accessoryLocation.ToString();
            }
        }
    }
}
