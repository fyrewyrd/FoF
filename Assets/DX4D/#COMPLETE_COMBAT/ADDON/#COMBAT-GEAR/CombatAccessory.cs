using System.Text;
using System.Collections.Generic;
using UnityEngine;
using DX4D;

namespace DX4D
{
    public static partial class ConfigTooltip
    {
        public const string headLabel = "Hat";
        public const string shouldersLabel = "Cape";
        public const string chestLabel = "Necklace";
        public const string legsLabel = "Belt";
        public const string feetLabel = "Boots";
        public const string handsLabel = "Ring";
        public const string shieldLabel = "Trinket";
    }
}

[CreateAssetMenu(menuName = "DX4D/GEAR/Combat Accessory", order = 11)]
public partial class CombatAccessory : CombatArmor
{
    [Header("ACCESSORY CATEGORY")]
    [SerializeField] public AccessoryEquipLocation accessoryLocation = AccessoryEquipLocation.RightRing;

    [Header("DAMAGE CONFIG")]
    [SerializeField] public DamageInfo damage = new DamageInfo();

    [Header("STATUS EFFECT CONFIG")]
    [SerializeField] public List<ScriptedStatusEffect> status;
    //[SerializeField] public Element dealsElementalDamageType = Element.Neutral; //DEPRECIATED
    //[SerializeField] public MethodOfDamage dealsDamageType = MethodOfDamage.NoDamage; //DEPRECIATED
    //[SerializeField] public StatusEffect onHitStatusEffect = StatusEffect.None; //DEPRECIATED
    //[SerializeField] public DamageEvent onHitDamageEffect; //DEPRECIATED

    // T O O L T I P
    public override string ToolTip()
    {
        StringBuilder info = new StringBuilder(base.ToolTip());
        if (automaticTooltip)
        {
            info.Append((Tooltip.OffenseToolTip(this)));// + DefenseTooltip()));
            category.Replace("Chest", ConfigTooltip.chestLabel);
            category.Replace("Shoulders", ConfigTooltip.shouldersLabel);
            category.Replace("Head", ConfigTooltip.headLabel);
            category.Replace("Hands", ConfigTooltip.handsLabel);
            category.Replace("Legs", ConfigTooltip.legsLabel);
            category.Replace("Feet", ConfigTooltip.feetLabel);
            category.Replace("Shield", ConfigTooltip.shieldLabel);
        }
        info.Replace("{CATEGORY}", "Accessory" + category.ToString());

        return info.ToString();
    }
}
