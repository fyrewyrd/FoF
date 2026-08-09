//#define ummorpg

using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/GEAR/Combat Armor", order = 12)]
public partial class CombatArmor : CombatGear
{
    [Header(" [ EQUIP CATEGORY ] ")]
    public ArmorLocation armorLocation = ArmorLocation.Chest;

    [Header(" [ DEFENSIVE VALUE ] ")]
    [SerializeField] int _armor = 1;
    public int armor { get { return (_armor
#if ummorpg
                + defenseBonus
#endif
                ); } }

    [Header(" [ RESISTANCES ] ")]
    [Tooltip("Resistance to various types of damage.")]
    [SerializeField] public Resistances resists;

    [Header(" [ WEAKNESSES ] ")]
    [Tooltip("Weaknesses to various types of damage.")]
    [SerializeField] public Weaknesses weakness;

    //[Header("DAMAGE METHOD RESISTS")]
    //public MethodOfDamage resistsDamageType = MethodOfDamage.NoDamage;
    //public MethodOfDamage negatesDamageType = MethodOfDamage.NoDamage;
    //public MethodOfDamage absorbsDamageType = MethodOfDamage.NoDamage;
    //public MethodOfDamage reflectsDamageType = MethodOfDamage.NoDamage;
    //public MethodOfDamage weakToDamageType = MethodOfDamage.NoDamage;
    //public MethodOfDamage veryWeakToDamageType = MethodOfDamage.NoDamage;

    //[Header("ELEMENTAL RESISTS")]
    //public Element resistsElement = Element.Neutral;
    //public Element negatesElement = Element.Neutral;
    //public Element absorbsElement = Element.Neutral;
    //public Element reflectsElement = Element.Neutral;
    //public Element weakToElement = Element.Neutral;
    //public Element veryWeakToElement = Element.Neutral;

    // T O O L T I P
    public override string ToolTip()
    {
        StringBuilder info = new StringBuilder(base.ToolTip());
        if (automaticTooltip)
        {
            info.Append(Tooltip.DefenseTooltip(armor));
            info.Append(Tooltip.ResistsTooltip(resists));
            info.Append(Tooltip.WeaknessTooltip(weakness));
        }
        info.Replace("{CATEGORY}", armorLocation.ToString());
        return info.ToString();
    }
}
    /*
    // tooltip
    public override string ToolTip()
    {
        StringBuilder info = new StringBuilder(base.ToolTip());
        

        info.Replace("{BASICRESISTINFO}",
            "{RESISTS}" +
            "{NEGATES}" +
            "{ABSORBS}" +
            "{REFLECTS}" +
            "{WEAKTO}" +
            "{VERYWEAKTO}"
            );
        info.Replace("{ELEMENTALRESISTINFO}",
            "{RESISTSELEMENT}" +
            "{NEGATESELEMENT}" +
            "{ABSORBSELEMENT}" +
            "{REFLECTSELEMENT}" +
            "{WEAKTOELEMENT}" +
            "{VERYWEAKTOELEMENT}"
            );

        info.Replace("{EQUIPSLOT}", armorLocation.ToString());

        if (true)
        {
            info.Replace("Head", "Headgear");
            info.Replace("Shoulders", "Shoulder Armor");
            info.Replace("Chest", "Chest Armor");
            info.Replace("Legs", "Leg Armor");
            info.Replace("Feet", "Footwear");
            info.Replace("Hands", "Gloves");
        }

        info.Replace("{CATEGORY}", category.ToString());
        info.Replace("{NAME}", name.ToString());

        info.Replace("{BASICRESISTINFO}",
            "{RESISTS}" +
            "{NEGATES}" +
            "{ABSORBS}" +
            "{REFLECTS}" +
            "{WEAKTO}" +
            "{VERYWEAKTO}"
            );
        info.Replace("{ELEMENTALRESISTINFO}",
            "{RESISTSELEMENT}" +
            "{NEGATESELEMENT}" +
            "{ABSORBSELEMENT}" +
            "{REFLECTSELEMENT}" +
            "{WEAKTOELEMENT}" +
            "{VERYWEAKTOELEMENT}"
            );

        //tip.Replace("{EQUIPSLOT}", armorLocation.ToString());
        if (true)
        {
            info.Replace("Head", "Headgear");
            info.Replace("Shoulders", "Shoulder Armor");
            info.Replace("Chest", "Chest Armor");
            info.Replace("Legs", "Leg Armor");
            info.Replace("Feet", "Footwear");
            info.Replace("Hands", "Gloves");
        }
        
        // D A M A G E  M E T H O D  R E S I S T S
        info.Replace("{RESISTS}", (resistsDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Resists " + resistsDamageType.ToString() + " Damage"));
        info.Replace("{NEGATES}", (negatesDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Ignores " + negatesDamageType.ToString() + " Damage"));
        info.Replace("{ABSORBS}", (absorbsDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Absorbs " + absorbsDamageType.ToString() + " Damage"));
        info.Replace("{REFLECTS}", (reflectsDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Reflects " + reflectsDamageType.ToString() + " Damage"));
        info.Replace("{WEAKTO}", (weakToDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Weak to " + weakToDamageType.ToString() + " Damage"));
        info.Replace("{VERYWEAKTO}", (veryWeakToDamageType == MethodOfDamage.NoDamage) ? "" : ("\n" + "Very Weak to " + veryWeakToDamageType.ToString() + " Damage"));

        // E L E M E N T A L  R E S I S T S
        info.Replace("{NEGATESELEMENT}", (negatesElement == Element.Neutral) ? "" : ("\n" + "Ignores " + negatesElement.ToString() + " Damage"));
        info.Replace("{RESISTSELEMENT}", (resistsElement == Element.Neutral) ? "" : ("\n" + "Resists " + resistsElement.ToString() + " Damage"));
        info.Replace("{ABSORBSELEMENT}", (absorbsElement == Element.Neutral) ? "" : ("\n" + "Absorbs " + absorbsElement.ToString() + " Damage"));
        info.Replace("{REFLECTSELEMENT}", (reflectsElement == Element.Neutral) ? "" : ("\n" + "Reflects " + reflectsElement.ToString() + " Damage"));
        info.Replace("{WEAKTOELEMENT}", (weakToElement == Element.Neutral) ? "" : ("\n" + "Weak to " + weakToElement.ToString() + " Damage"));
        info.Replace("{VERYWEAKTOELEMENT}", (veryWeakToElement == Element.Neutral) ? "" : ("\n" + "Very Weak to " + veryWeakToElement.ToString() + " Damage"));

        //NOT WEAPON
        //info.Replace("{OFFENSE}", "");
        return info.ToString();
    }
}
*/
