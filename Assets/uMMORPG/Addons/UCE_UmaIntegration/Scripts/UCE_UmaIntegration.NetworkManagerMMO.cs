// =======================================================================================
// Maintained by bobatea#9400 on Discord
// Usable for both personal and commercial projects, but no sharing or re-sale
// =======================================================================================

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkManagerMMO : NetworkManager
{
    public void LoadPreview(GameObject prefab, Transform location, int selectionIndex, CharactersAvailableMsg.CharacterPreview character)
    {
        GameObject preview = Instantiate(prefab.gameObject, location.position, location.rotation);
        preview.transform.parent = location;
        Player player = preview.GetComponent<Player>();

        player.name = character.name;
        player.umaDna = character.umaDna ?? "";

        // Safe UMA loading - skip if DNA is empty
        var avatar = preview.GetComponentInChildren<UMA.CharacterSystem.DynamicCharacterAvatar>();
        if (avatar != null && !string.IsNullOrEmpty(character.umaDna))
        {
            try
            {
                avatar.LoadFromRecipeString(CompressUMA.Compressor.DecompressDna(character.umaDna));
                Debug.Log($"[Preview] Loaded UMA DNA for {character.name}");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[Preview] Failed to load UMA DNA for {character.name}: {e.Message}. Using default model.");
            }
        }
        else
        {
            Debug.Log($"[Preview] No UMA DNA or avatar found for {character.name} - using base model");
        }

        // Equipment
        for (int i = 0; i < character.equipment.Length; ++i)
        {
            ItemSlot slot = character.equipment[i];
            player.equipment.Add(slot);
            if (slot.amount > 0)
            {
                player.RefreshLocation(i);
                player.UpdateUma();
            }
        }

        // Add selection component
        preview.AddComponent<SelectableCharacter>();
        preview.GetComponent<SelectableCharacter>().index = selectionIndex;
    }

// ===================================================================
// CHARACTER CREATION - UMA Compatible
// ===================================================================
    private Player CreateCharacter(GameObject classPrefab, string characterName, string account, string dna, int classIndex)
    {
        Player player = Instantiate(classPrefab).GetComponent<Player>();

        player.name = characterName;
        player.account = account;
        player.className = classPrefab.name;
        player.umaDna = dna;                    // Important for UMA

        // Position using classIndex (from your selectionLocations array)
        player.transform.position = GetStartPositionFor(classIndex).position;

        // Default inventory
        for (int i = 0; i < player.inventorySize; ++i)
        {
            player.inventory.Add(i < player.defaultItems.Length 
                ? new ItemSlot(new Item(player.defaultItems[i].item), player.defaultItems[i].amount) 
                : new ItemSlot());
        }

        // Default equipment
        for (int i = 0; i < player.equipmentInfo.Length; ++i)
        {
            EquipmentInfo info = player.equipmentInfo[i];
            player.equipment.Add(info.defaultItem.item != null 
                ? new ItemSlot(new Item(info.defaultItem.item), info.defaultItem.amount) 
                : new ItemSlot());
        }

        player.health = player.healthMax;
        player.mana = player.manaMax;

        return player;
    }
}