using UnityEngine;

[CreateAssetMenu(menuName = "GFF Addons/Item Upgrade/Gem Item", order = 999)]
public class UpgradeGemItem : ScriptableItem
{
    [Header("GFF Upgrade addon")]
    public int gemType;
    public float gemChanceIncrease;
}
