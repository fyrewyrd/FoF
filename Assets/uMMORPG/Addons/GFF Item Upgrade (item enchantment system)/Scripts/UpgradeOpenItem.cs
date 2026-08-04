using UnityEngine;

[CreateAssetMenu(menuName = "GFF Addons/Item Upgrade/Open Item", order = 999)]
public class UpgradeOpenItem : UsableItem
{
    // usage
    public override void Use(Player player, int inventoryIndex)
    {
        // decrease amount
        ItemSlot slot = player.inventory[inventoryIndex];
        slot.DecreaseAmount(1);
        player.inventory[inventoryIndex] = slot;
    }

    // [Client] OnUse Rpc callback for effects, sounds, etc.
    // -> can't pass slotIndex because .Use might clear it before getting here already
    public override void OnUsed(Player player)
    {
        if (player.isLocalPlayer && player.isClient)
        {
            UIUpgrade.singleton.Show();
        }
    }
}
