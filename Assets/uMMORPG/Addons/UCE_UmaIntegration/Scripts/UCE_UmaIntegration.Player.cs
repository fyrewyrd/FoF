// =======================================================================================
// Maintained by bobatea#9400 on Discord
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............:  
  
// * Leave a star on my Github Repo.....: https://github.com/breehuynh/Bree-mmorpg-tools
// * Instructions.......................: https://indie-mmo.net/knowledge-base/
// =======================================================================================


using Mirror;
using System.Collections;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public partial class Player : Entity
{
    [SyncVar]
    public string umaDna = "";

    private void OnStartClient_UmaIntegration()
    {
        Debug.Log("[UMA] OnStartClient " + name + " local=" + isLocalPlayer + " dnaLen=" + (umaDna ?? "").Length);
        StartCoroutine(ApplyUmaWhenReady());
    }

    private IEnumerator ApplyUmaWhenReady()
    {
        float t = 0f;
        DynamicCharacterAvatar avatar = null;

        while (avatar == null && t < 5f)
        {
            avatar = GetComponentInChildren<DynamicCharacterAvatar>();
            t += Time.deltaTime;
            yield return null;
        }

        if (avatar == null)
        {
            Debug.LogWarning("[UMA] no DynamicCharacterAvatar on " + name);
            yield break;
        }

        t = 0f;
        while (string.IsNullOrEmpty(umaDna) && t < 5f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        Debug.Log("[UMA] applying dnaLen=" + (umaDna ?? "").Length + " raceBefore=" + avatar.activeRace.name);
        RefreshUma();
        Debug.Log("[UMA] raceAfter=" + avatar.activeRace.name);
    }
    public void ProcessBones(Transform transform)
    {
        foreach (Transform t in transform)
        {
            RecursivelyRemoveChildBones(t);
            GameObject.Destroy(t.gameObject);
        }
    }

    private void RecursivelyRemoveChildBones(Transform transform)
    {
        GetComponentInChildren<DynamicCharacterAvatar>().umaData.skeleton.RemoveBone(UMAUtils.StringToHash(transform.name));
        foreach (Transform t in transform)
        {
            Debug.Log("RecursivelyRemoveChildBones(" + t + ".");
            RecursivelyRemoveChildBones(t);
        }
    }

    public void UpdateUma()
    {
        RefreshUma();
    }

    public void RefreshUma()
    {
        if (umaDna == "") return;

        DynamicCharacterAvatar avatar = GetComponentInChildren<DynamicCharacterAvatar>();

        if (GetComponentInChildren<DynamicCharacterAvatar>() == null) return;

        string decompressed = CompressUMA.Compressor.DecompressDna(umaDna);

        avatar.ClearSlots();
        avatar.LoadFromRecipeString(decompressed);

        for (int i = 0; i < equipment.Count; i++)
        {
            ItemSlot slot = equipment[i];
            EquipmentInfo info = equipmentInfo[i];

            //  valid item?
            if (slot.amount > 0)
            {
                EquipmentItem itemData = (EquipmentItem)slot.item.data;

                UMATextRecipe maleRecipe = itemData.maleUmaRecipe;
                UMATextRecipe femaleRecipe = itemData.femaleUmaRecipe;

                if (avatar == null) return;

                if (maleRecipe != null)
                    avatar.SetSlot(maleRecipe);

                if (femaleRecipe != null)
                    avatar.SetSlot(femaleRecipe);
            }
        }
    }
    public void PackUmaDna()
    {
        if (!isServer) return;

        DynamicCharacterAvatar avatar = GetComponentInChildren<DynamicCharacterAvatar>();
        if (avatar == null) return;

        string recipe = avatar.GetCurrentRecipe();
        if (string.IsNullOrEmpty(recipe)) return;

        umaDna = CompressUMA.Compressor.CompressDna(recipe);
        Debug.Log($"[UMA] Packed {name} race={avatar.activeRace.name} dnaLen={umaDna.Length}");
    }
    private IEnumerator WaitForDcs()
    {
        yield return new WaitWhile(() => FindObjectOfType<DynamicCharacterSystem>() == null);
        yield return new WaitWhile(() => GetComponentInChildren<DynamicCharacterAvatar>() == null);
        StartCoroutine(WaitForDna());
    }

    private IEnumerator WaitForDna()
    {
        yield return new WaitWhile(() => umaDna == "");
        RefreshUma();
    }
}

