using Mirror;
using UnityEngine;

public partial class Backpack : NetworkBehaviour
{
    public const long DEFAULT_MAX_INVENTORY = 20;// long.MaxValue;

    [Header(" [ INVENTORY ] ")] [Tooltip("Default = 20")]
    [SerializeField] protected LinearLong _inventoryMax =
        new LinearLong { baseValue = DEFAULT_MAX_INVENTORY };
    public LinearLong inventoryMax { get { return _inventoryMax; } }
    
    public SyncListItemSlot inventory = new SyncListItemSlot();
}

public partial class CharacterSheet : NetworkBehaviour
{
    public SyncListItemSlot INVENTORY
    {
        get { return items.inventory; }
        set { items.inventory = value; }
    }
    
    public int GetInventoryIndexByName(string itemName)
    {
        for (int i = 0; i < INVENTORY.Count; ++i)
        {
            ItemSlot slot = INVENTORY[i];
            if (slot.amount > 0 && slot.item.name == itemName)
                return i;
        }
        return -1;
    }
    
    /// <summary>The number of empty slots in the inventory.</summary>
    public int InventorySlotsFree
    {
        get {
            // count manually. Linq is HEAVY(!) on GC and performance
            int free = 0;
            foreach (ItemSlot slot in INVENTORY)
                if (slot.amount == 0)
                    ++free;
            return free;
        }
    }

    // helper function to calculate the total amount of an item type in inventory
    // note: .Equals because name AND dynamic variables matter (petLevel etc.)
    public int InventoryCount(Item item)
    {
        // count manually. Linq is HEAVY(!) on GC and performance
        int amount = 0;
        foreach (ItemSlot slot in INVENTORY)
            if (slot.amount > 0 && slot.item.Equals(item))
                amount += slot.amount;
        return amount;
    }

    // helper function to remove 'n' items from the inventory
    public bool InventoryRemove(Item item, int amount)
    {
        for (int i = 0; i < INVENTORY.Count; ++i)
        {
            ItemSlot slot = INVENTORY[i];
            // note: .Equals because name AND dynamic variables matter (petLevel etc.)
            if (slot.amount > 0 && slot.item.Equals(item))
            {
                // take as many as possible
                amount -= slot.DecreaseAmount(amount);
                INVENTORY[i] = slot;

                // are we done?
                if (amount == 0) return true;
            }
        }

        // if we got here, then we didn't remove enough items
        return false;
    }

    // helper function to check if the inventory has space for 'n' items of type
    // -> the easiest solution would be to check for enough free item slots
    // -> it's better to try to add it onto existing stacks of the same type
    //    first though
    // -> it could easily take more than one slot too
    // note: this checks for one item type once. we can't use this function to
    //       check if we can add 10 potions and then 10 potions again (e.g. when
    //       doing player to player trading), because it will be the same result
    public bool InventoryCanAdd(Item item, int amount)
    {
        // go through each slot
        for (int i = 0; i < INVENTORY.Count; ++i)
        {
            // empty? then subtract maxstack
            if (INVENTORY[i].amount == 0)
                amount -= item.maxStack;
            // not empty. same type too? then subtract free amount (max-amount)
            // note: .Equals because name AND dynamic variables matter (petLevel etc.)
            else if (INVENTORY[i].item.Equals(item))
                amount -= (INVENTORY[i].item.maxStack - INVENTORY[i].amount);

            // were we able to fit the whole amount already?
            if (amount <= 0) return true;
        }

        // if we got here than amount was never <= 0
        return false;
    }

    // helper function to put 'n' items of a type into the inventory, while
    // trying to put them onto existing item stacks first
    // -> this is better than always adding items to the first free slot
    // -> function will only add them if there is enough space for all of them
    public bool InventoryAdd(Item item, int amount)
    {
        // we only want to add them if there is enough space for all of them, so
        // let's double check
        if (InventoryCanAdd(item, amount))
        {
            // add to same item stacks first (if any)
            // (otherwise we add to first empty even if there is an existing
            //  stack afterwards)
            for (int i = 0; i < INVENTORY.Count; ++i)
            {
                // not empty and same type? then add free amount (max-amount)
                // note: .Equals because name AND dynamic variables matter (petLevel etc.)
                if (INVENTORY[i].amount > 0 && INVENTORY[i].item.Equals(item))
                {
                    ItemSlot temp = INVENTORY[i];
                    amount -= temp.IncreaseAmount(amount);
                    INVENTORY[i] = temp;
                }

                // were we able to fit the whole amount already? then stop loop
                if (amount <= 0) return true;
            }

            // add to empty slots (if any)
            for (int i = 0; i < INVENTORY.Count; ++i)
            {
                // empty? then fill slot with as many as possible
                if (INVENTORY[i].amount == 0)
                {
                    int add = Mathf.Min(amount, item.maxStack);
                    INVENTORY[i] = new ItemSlot(item, add);
                    amount -= add;
                }

                // were we able to fit the whole amount already? then stop loop
                if (amount <= 0) return true;
            }
            // we should have been able to add all of them
            if (amount != 0) Debug.LogError("Cannot add " + amount + " " + item.name + " to " + name);
        }
        return false;
    }
}