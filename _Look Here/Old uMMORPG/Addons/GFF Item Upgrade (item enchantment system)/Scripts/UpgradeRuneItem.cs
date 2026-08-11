using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GFF Addons/Item Upgrade/Rune Item", order = 999)]
public class UpgradeRuneItem : ScriptableItem
{
    [Header("GFF Upgrade addon")]
    public runeType runeType;
    public string runeName;
    public string textInfo = "";
    public int[] effect = new int[UIUpgrade.maxUpgrade];

    public static UpgradeRuneItem GetRuneFromDictByType(runeType type)
    {
        foreach (var item in runeDict)
        {
            if (item.Value.runeType == type) return item.Value;
        }

        return null;
    }

    // caching /////////////////////////////////////////////////////////////////
    // we can only use Resources.Load in the main thread. we can't use it when
    // declaring static variables. so we have to use it as soon as 'dict' is
    // accessed for the first time from the main thread.
    // -> we save the hash so the dynamic item part doesn't have to contain and
    //    sync the whole name over the network
    static Dictionary<int, UpgradeRuneItem> cache;
    public static Dictionary<int, UpgradeRuneItem> runeDict
    {
        get
        {
            // not loaded yet?
            if (cache == null)
            {
                // get all ScriptableItems in resources
                UpgradeRuneItem[] items = Resources.LoadAll<UpgradeRuneItem>("");

                // check for duplicates, then add to cache
                List<string> duplicates = items.ToList().FindDuplicates(item => item.name);
                if (duplicates.Count == 0)
                {
                    cache = items.ToDictionary(item => item.name.GetStableHashCode(), item => item);
                }
                else
                {
                    foreach (string duplicate in duplicates)
                        Debug.LogError("Resources folder contains multiple ScriptableItems with the name " + duplicate + ". If you are using subfolders like 'Warrior/Ring' and 'Archer/Ring', then rename them to 'Warrior/(Warrior)Ring' and 'Archer/(Archer)Ring' instead.");
                }
            }
            return cache;
        }
    }
}
