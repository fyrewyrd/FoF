public partial class Monster : Entity
{
    // loot ////////////////////////////////////////////////////////////////////
    // other scripts need to know if it still has valid loot (to show UI etc.)
    public bool HasLoot()
    {
        // any gold or valid items?
        if (gold > 0)
            return true;

        // check slots manually. Linq is HEAVY(!) on GC and performance
        foreach (ItemSlot slot in inventory)
            if (slot.amount > 0)
                return true;

        return false;
    }
}