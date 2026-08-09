public partial class PlayerCharacter : CharacterSheet
{
    // helper function to remove 'n' items from the equipment
    public bool EquipmentRemove(ScriptableItem item, int amount)
    {
        for (int i = 0; i < EQUIPMENT.Count; ++i)
        {
            ItemSlot slot = EQUIPMENT[i];
            // note: .Equals because name AND dynamic variables matter (petLevel etc.)
            if (slot.amount > 0 && slot.item.data.Equals(item))
            {
                // take as many as possible
                amount -= slot.DecreaseAmount(amount);
                EQUIPMENT[i] = slot;

                // are we done?
                if (amount == 0) return true;
            }
        }

        // if we got here, then we didn't remove enough items
        return false;
    }

}
