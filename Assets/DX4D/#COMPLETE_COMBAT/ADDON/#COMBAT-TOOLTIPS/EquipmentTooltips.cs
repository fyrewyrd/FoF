using System.Text;

// W E A P O N
public static partial class Tooltip
{
    public static string OffenseToolTip(CombatWeapon weapon)
    {
        if (!weapon) return string.Empty;

        StringBuilder info = new StringBuilder("{OFFENSE}");
        
        info.Replace("{OFFENSE}", "{RANGEINFO}{WEAPONDATA}{AMMOINFO}{DAMAGEINFO}{DOTINFO}{EFFECTINFO}");

        // W E A P O N  D A T A
        info.Replace("{WEAPONDATA}", "\n" + "{EQUIPSLOT} {EQUIPHAND}" + "\n");
        info.Replace("{RANGEINFO}", weapon.attackRange < CombatWeapon.closeRangeCutoff ? "" : "\n" + "Range: {RANGE}");
        info.Replace("{AMMOINFO}", weapon.ammunition.Count < 1 ? "" : "\n" + "Ammo Capacity: {AMMO}");
        info.Replace("{DAMAGEINFO}", (weapon.damage.max > 0) ? "\n" + "{DAMAGEBONUS}{ELEMENT}{DAMAGETYPE} damage" : "");
        info.Replace("{DOTINFO}", (weapon.damage.damageOverTime == null || weapon.damage.damageOverTime.Count < 1) ? "" : "\n\nDOT Damage:" + DamageOverTimeTooltip(weapon) );
        info.Replace("{EFFECTINFO}", "{STATUSEFFECT}");

        //RANGE
        info.Replace("{RANGE}", weapon.attackRange.ToString());
        //AMMO
        info.Replace("{AMMO}", "{AMMOLOADED}" + "/" + "{CAPACITY}");
        info.Replace("{CAPACITY}", weapon.maxAmmo.ToString());
        info.Replace("{AMMOLOADED}", weapon.ammunition.Count.ToString());

        //DAMAGE
        info.Replace("{DAMAGEBONUS}", (weapon.damage.fixedDamage) ? (weapon.damage.max.ToString()) : (weapon.damage.min + "-" + weapon.damage.max) );
        //METHOD
        info.Replace("{DAMAGETYPE}", " " + weapon.damage.method.ToString());
        //ELEMENT
        info.Replace("{ELEMENT}", (weapon.damage.element != Element.Neutral) ? " " + "<color=" + DX4D.Tools.GetHex.FromElement(weapon.damage.element) + ">" + weapon.damage.element.ToString() + "</color>" : "");

        //STATUS EFFECTS
        if (weapon.damage.status != null && weapon.damage.status.statusEffects != null && weapon.damage.status.statusEffects.Count > 0)
        {
            info.Replace("{STATUSEFFECT}", weapon.damage.status.statusEffects.ToString());
        }
        else
        {
            info.Replace("{STATUSEFFECT}", "");
        }
        //info.Replace("{STATUSEFFECT}", (dealsStatusEffect == StatusEffect.None) ? "" : ("\n" + "Adds Status: " + dealsStatusEffect.ToString()));
        //DAMAGE EFFECTS
        //info.Replace("{DAMAGEEFFECT}", (onHitDamageEffect == null) ? "" : ("\n" + "Adds Damage: " + onHitDamageEffect.ToString()));

        //EQUIP SLOT
        info.Replace("{EQUIPSLOT}", weapon.weaponCategory.ToString());
        info.Replace("Weapon", "");
        info.Replace("OneHanded", "");
        info.Replace("TwoHanded", "");
        info.Replace("Unarmed", "");
        info.Replace("Siege", "");
        //info.Replace("Ammo", "");

        //info.Replace("{CATEGORY}", category.ToString());
        //info.Replace("{NAME}", name.ToString());

        info.Replace("{EQUIPHAND}", weapon.weaponHand.ToString());
        info.Replace("OneHanded", " - One Handed");
        info.Replace("TwoHanded", " - Two Handed");
        info.Replace("Unarmed", " - Unarmed");
        info.Replace("Siege", " - Siege");
        //info.Replace("Unarmed", "");

        return info.ToString();
    }
    private static string DamageOverTimeTooltip(CombatWeapon weapon)
    {
        if (!weapon) return string.Empty;

        StringBuilder info = new StringBuilder();
        for (int i = 0; i < weapon.damage.damageOverTime.Count; i++)
        {
            info.Append((weapon.damage.damageOverTime[i].damage.total > 0) ? "\n" + weapon.damage.damageOverTime[i].damage.total.ToString() + " " : "");
            info.Append((weapon.damage.damageOverTime[i].damage.method != MethodOfDamage.NoDamage) ? weapon.damage.damageOverTime[i].damage.method.ToString() + " " : "");
            
            info.Append((weapon.damage.damageOverTime[i].damage.element != Element.Neutral)
                ? "<color=" + DX4D.Tools.GetHex.FromElement(weapon.damage.damageOverTime[i].damage.element) + ">"
                + weapon.damage.damageOverTime[i].damage.element.ToString() + ""
                + "</color>"
                : "");
            info.Append((weapon.damage.damageOverTime[i].damage.total > 0) ? " Damage" : "");
        }
        return info.ToString();
    }
}
// A R M O R
public static partial class Tooltip
{
    public static string DefenseTooltip(int armor)//Defenses defenses)
    {
        if (armor < 1) return string.Empty;

        StringBuilder info = new StringBuilder("{DEFENSE}");

        // D E F E N S E
        info.Replace("{DEFENSE}", (armor < 1) ? "" : "\nDefense: " + armor.ToString());
        
        return info.ToString();
    }
    public static string ResistsTooltip(Resistances gearResists)
    {
        if (!gearResists) return string.Empty;

        StringBuilder info = new StringBuilder("{RESISTS}");

        info.Replace("{RESISTS}",
            "{RESISTS}{RESISTSELEMENT}" +
            "{NEGATES}{NEGATESELEMENT}" +
            "{ABSORBS}{ABSORBSELEMENT}" +
            "{REFLECTS}{REFLECTSELEMENT}" +
            "{WEAKTO}{WEAKTOELEMENT}" +
            "{VERYWEAKTO}{VERYWEAKTOELEMENT}"
            );

        //RESIST
        info.Replace("{RESISTS}", (gearResists.resistDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Resists " + gearResists.resistDamage.ToString() + " Damage"));
        info.Replace("{RESISTSELEMENT}", (gearResists.resistElement == Element.Neutral) ? "" : ("\n" + "Resists " + gearResists.resistElement.ToString() + " Damage"));

        //NEGATE
        info.Replace("{NEGATES}", (gearResists.negateDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Ignores " + gearResists.negateDamage.ToString() + " Damage"));
        info.Replace("{NEGATESELEMENT}", (gearResists.negateElement == Element.Neutral) ? "" : ("\n" + "Ignores " + gearResists.negateElement.ToString() + " Damage"));

        //ABSORB
        info.Replace("{ABSORBS}", (gearResists.absorbDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Absorbs " + gearResists.absorbDamage.ToString() + " Damage"));
        info.Replace("{ABSORBSELEMENT}", (gearResists.absorbElement == Element.Neutral) ? "" : ("\n" + "Absorbs " + gearResists.absorbElement.ToString() + " Damage"));

        //REFLECT
        info.Replace("{REFLECTS}", (gearResists.reflectDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Reflects " + gearResists.reflectDamage.ToString() + " Damage"));
        info.Replace("{REFLECTSELEMENT}", (gearResists.reflectElement == Element.Neutral) ? "" : ("\n" + "Reflects " + gearResists.reflectElement.ToString() + " Damage"));
        
        return info.ToString();
    }
    public static string WeaknessTooltip(Weaknesses weakness)
    {
        if (!weakness) return string.Empty;

        StringBuilder info = new StringBuilder("{WEAKNESS}");
        
        info.Replace("{WEAKNESS}",
            "{WEAKTO}{WEAKTOELEMENT}" +
            "{VERYWEAKTO}{VERYWEAKTOELEMENT}"
            );

        //WEAK TO
        info.Replace("{WEAKTO}", (weakness.weakToDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Weak to " + weakness.weakToDamage.ToString() + " Damage"));
        info.Replace("{WEAKTOELEMENT}", (weakness.weakToElement == Element.Neutral) ? "" : ("\n" + "Weak to " + weakness.weakToElement.ToString() + " Damage"));

        //VERY WEAK TO
        info.Replace("{VERYWEAKTO}", (weakness.veryWeakToDamage == MethodOfDamage.NoDamage) ? "" : ("\n" + "Very Weak to " + weakness.veryWeakToDamage.ToString() + " Damage"));
        info.Replace("{VERYWEAKTOELEMENT}", (weakness.veryWeakToElement == Element.Neutral) ? "" : ("\n" + "Very Weak to " + weakness.veryWeakToElement.ToString() + " Damage"));

        return info.ToString();
    }
}
// A C C E S S O R Y
public static partial class Tooltip
{
    public static string OffenseToolTip(CombatAccessory accessory)
    {
        if (!accessory) return string.Empty;

        StringBuilder info = new StringBuilder("{OFFENSE}");

        // O F F E N S E
        info.Replace("{OFFENSE}", "{DAMAGEINFO}");// {EFFECTINFO}");

        //DAMAGE
        info.Replace("{DAMAGEINFO}", (accessory.damage.total < 1) ? "" : "\n" + "{DAMAGEBONUS} {ELEMENT} {DAMAGETYPE} Damage");
        info.Replace("{DAMAGEBONUS}", accessory.damage.total.ToString());
        info.Replace("{ELEMENT}", accessory.damage.element.ToString());
        info.Replace("{DAMAGETYPE}", accessory.damage.method.ToString());

        //EFFECT
        //info.Replace("{EFFECTINFO}", "{STATUSEFFECT}");
        if (accessory.status != null && accessory.status.Count > 0)
        {
            foreach (ScriptedStatusEffect statusEffect in accessory.status)
            {
                if (statusEffect != null) info.Append(statusEffect.ToString());
            }
        }
        //info.Replace("{STATUSEFFECT}", (onHitStatusEffect == StatusEffect.None) ? "" : ("\n" + "Adds Status: " + onHitStatusEffect.ToString())); //DEPRECIATED
        //info.Replace("{DAMAGEEFFECT}", (onHitDamageEffect == null) ? "" : ("\n" + "Adds Damage: " + onHitDamageEffect.ToString())); //DEPRECIATED

        return info.ToString();
    }
}

