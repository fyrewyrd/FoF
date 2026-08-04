using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable] public class DamageInfo
{
    // D A M A G E  V A L U E S
    [Header("Base Damage Properties")]
    [SerializeField] public DamageValues damage;
    //DAMAGE VALUE ACCESSORS
    /// <summary>The total amount of damage. (After random calculations + bonus * multiplier etc)</summary>
    public int total { get { return damage.total; } }
    /// <summary>The base amount of damage. (Without bonus and multipliers)</summary>
    public int amount { get { return damage.amount; } set { damage.amount = value; } }
    public int min { get { return damage.min; } set { damage.min = value; } }
    public int max { get { return damage.max; } set { damage.max = value; } }
    public int bonus { get { return damage.bonus; } set { damage.bonus = value; } }
    public float multiplier { get { return damage.multiplier; } set { damage.multiplier = value; } }
    public MethodOfDamage method { get { return damage.method; } set { damage.method = value; } }
    public Element element { get { return damage.element; } set { damage.element = value; } }
    public bool fixedDamage { get { return damage.fixedDamage; } set { damage.fixedDamage = value; } }

    // D A M A G E  P R O P E R T I E S
    [Header("Special Damage Properties")]
    [SerializeField] public DamageProperties properties;

    // T A C T I C A L  P R O P E R T I E S
    [Header("Tactical Damage Properties")]
    [SerializeField] public CombatTactics tactics;
    
    // S T A T U S  E F F E C T S
    [Header("Status Effect Properties")]
    [SerializeField] public StatusEffectList status;
    //public List<ScriptedStatusEffect> statusEffects { get { return status.statusEffects; } }//set { status.statusEffects = value; } }

    //DAMAGE OVER TIME TICKS
    [Header(" D A M A G E   O V E R   T I M E ")]
    [Tooltip("The amount of time (in seconds) between the application of each added damage in the DoT list. (Like a DoT)")]
    public float damageTickInterval = 1.0f;
    //DAMAGE OVER TIME
    [Header("-DoT damage-")]
    [Tooltip("A list of scripted damages representing each 'tick' of damage in the DoT")]
    [SerializeField] public List<ScriptedDamage> damageOverTime = new List<ScriptedDamage>();
    
    //public Damage(Damage damage) : this(damage.amount, damage.bonus, damage.multiplier, damage.method, damage.element) { }
    public DamageInfo() : this(0, 0, true, MethodOfDamage.Physical, Element.Neutral, 0, 1.0f) { }
    public DamageInfo(int minimumDamage, int maximumDamage, bool fixedDmg = true, MethodOfDamage damageMethod = MethodOfDamage.Physical, Element damageElement = Element.Neutral, int damageBonus = 0, float damageMultiplier = 1.0f, bool notaggressive = false, bool notmodified = false, bool notreflectable = false, bool leechable = false, bool notabsorbable = false, bool notresistable = false, bool notavoidable = false, bool notblockable = false, bool notdirectional = false)
    {
        damage = new DamageValues(minimumDamage, maximumDamage, damageBonus, damageMultiplier, damageMethod, damageElement, fixedDmg);
        properties = new DamageProperties(notaggressive, notmodified, notreflectable, leechable, notabsorbable, notresistable, notavoidable, notblockable, notdirectional);
        tactics = new CombatTactics();
        status = new StatusEffectList();
        //damage.fixedDamage = (minimumDamage == maximumDamage);
        //damage.min = minimumDamage;
        //damage.max = maximumDamage;
        //damage.bonus = damageBonus;
        //damage.multiplier = damageMultiplier;

        //damage.method = damageMethod;
        //damage.element = damageElement;
    }
    /// <summary>
    /// Create a "Deep Copy" of this object...changes to the copy's values will not be reflected in the original.
    /// </summary>
    public DamageInfo copy()
    {
        DamageInfo info = (DamageInfo)this.MemberwiseClone();

        //info._amount = _amount;
        //info.fixedDamage = fixedDamage;
        //info.min = min;
        //info.max = max;
        //info.bonus = bonus;
        //info.multiplier = multiplier;

        //info.method = method;
        //info.element = element;

        info.damage = damage.copy();
        info.properties = properties.copy();
        info.tactics = tactics.copy();
        info.status = status.copy();

        info.damageTickInterval = damageTickInterval;
        info.damageOverTime = new List<ScriptedDamage>(damageOverTime);

        return info;
    }

    #region DAMAGE VALUES
    [Serializable] public class DamageValues
    {
        [Header("Inherit Properties")]
        [SerializeField] public MethodOfDamage method = MethodOfDamage.Physical;
        [SerializeField] public Element element = Element.Neutral;

        [Header(" D A M A G E ")]
        [Tooltip("Fixed damage only uses the maxDamage value")]
        [SerializeField] public bool fixedDamage = true;
        //MAX
        [Header("-MAX-")]
        [Tooltip("The highest amount of possible base damage. When fixed damage is turned on, this is your base damage.\nFor Dice rolls this number represents the size of the dice times the number of dice rolls.\n(example) Rolling 5D20 would be min/max 5/100 or 2D4 would be 2/8")]
        [SerializeField] int _max = 0;
        public int max
        {
            get { return _max; }
            set { if (_max != value) { _max = value; _amount = 0; } } //NOTE: When this gets reset, amount is set to zero so that it will re-randomize next grab
        }
        //MIN
        [Header("-min-")]
        [Tooltip("The lowest amount of possible base damage. Only functional when fixed damage is turned off.\nFor Dice rolls this number represents the number of dice to roll.")]
        [SerializeField] int _min = 0;
        public int min
        {
            get { return _min; }
            set { if (_min != value) { _min = value; _amount = 0; } } //NOTE: When this gets reset, amount is set to zero so that it will re-randomize next grab
        }
        //BONUS
        [Header("-Bonus-")]
        [Tooltip("The bonus that is added after the base damage is scaled.\nFor Dice rolls this number represents the amount added after rolling the dice.")]
        [SerializeField] public int bonus = 0;
        //SCALE
        [Header("-Multiplier-")]
        [Tooltip("The scale of the base damage...1 is normal damage, 2 would be double damage, etc\nFor Dice rolls this number can be used to represent the number of dice to roll.")]
        [SerializeField] public float multiplier = 1.0f;

        // D A M A G E  A M O U N T S
        //SET
        //public void Set(int newAmount) { _amount = newAmount; }
        //AMOUNT
        private int _amount = 0;
        public int amount
        {
            get
            {
                if (fixedDamage) _amount = max;
                else if (_amount <= 0 && max > 0) { _amount = UnityEngine.Random.Range(min, max); }
                return _amount;
            }
            set { _amount = value; }
        }
        //DAMAGE TOTAL
        public int total
        {
            get
            {
                if (amount + bonus < 0) return 0;
                return (int)((amount * multiplier) + bonus);
                //int totalAmount = (int)((amount * multiplier) + bonus);
                //return totalAmount;
            }
        }


        public DamageValues() : this(0, 0, 0, 1.0f, MethodOfDamage.Physical, Element.Neutral, true) { }
        public DamageValues(
            int dmgMin = 0, int dmgMax = 0, int dmgBonus = 0, float dmgMultiplier = 1.0f, MethodOfDamage dmgMethod = MethodOfDamage.Physical, Element dmgElement = Element.Neutral, bool dmgFixed = false)
        {
            element = dmgElement;
            method = dmgMethod;
            min = dmgMin;
            max = dmgMax;
            bonus = dmgBonus;
            multiplier = dmgMultiplier;
            fixedDamage = dmgFixed;
        }
        public DamageValues copy()
        {
            DamageValues info = (DamageValues)this.MemberwiseClone();

            info.method = method;
            info.element = element;
            info.min = min;
            info.max = max;
            info.bonus = bonus;
            info.multiplier = multiplier;
            info.fixedDamage = fixedDamage;

            info._amount = _amount; //NOTE: _amount gets set last because the set methods of the other stats recalculate the amount.

            return info;
        }
    }
    #endregion

    #region DAMAGE PROPERTIES
    [Serializable] public class DamageProperties
    {
        /// <summary>Aggro is not triggered by this damage.</summary>
        [Tooltip("Aggro is not triggered by this damage.")]
        [SerializeField] public bool nonAggressive = false;

        /// <summary>Damage values are not modified by stats of either the defender or the attacker, will not trigger critical hits etc.</summary>
        [Tooltip("Damage values are not modified by stats of either the defender or the attacker, will not trigger critical hits etc.")]
        [SerializeField] public bool directDamage = false;

        /// <summary>Ignores the reflect status of the defender.</summary>
        [Tooltip("Ignores the reflect status of the defender.")]
        [SerializeField] public bool penetrateReflect = false;

        /// <summary>Damage done will also heal the caster.</summary>
        [Tooltip("Damage done will also heal the caster.")]
        [SerializeField] public bool lifeLeech = false;

        /// <summary>Ignores the damage absorbing status of the defender.</summary>
        [Tooltip("Ignores the damage absorbing status of the defender.")]
        [SerializeField] public bool nonAbsorbable = false;

        /// <summary>Ignores the defender's damage resistance.</summary>
        [Tooltip("Ignores the defender's resistance.")]
        [SerializeField] public bool nonResistable = false;

        /// <summary>This attack cannot be dodged.</summary>
        [Tooltip("This attack cannot be dodged.")]
        [SerializeField] public bool unavoidable = false;

        /// <summary>This attack cannot be blocked.</summary>
        [Tooltip("This attack cannot be blocked.")]
        [SerializeField] public bool unblockable = false;

        /// <summary>Sneak Attacks (Backstab, Flanking, and Overwhelm) will not trigger from this damage.</summary>
        [Tooltip("Sneak Attacks (Backstab, Flanking, and Overwhelm) will not trigger from this damage.")]
        [SerializeField] public bool nonSneakAttack = false;

        //CONSTRUCTOR
        public DamageProperties() : this(false, false, false, false, false, false, false, false) { }
        public DamageProperties(
            bool noAggro, bool directDamage,
            bool noReflect = false, bool leech = false,
            bool noAbsorb = false, bool noResist = false,
            bool noDodge = false, bool noBlock = false, bool noBackstab = false)
        {
            nonAggressive = noAggro;
            this.directDamage = directDamage;
            penetrateReflect = noReflect;
            lifeLeech = leech;
            nonAbsorbable = noAbsorb;
            nonResistable = noResist;
            unavoidable = noDodge;
            unblockable = noBlock;
            nonSneakAttack = noBackstab;
        }
        //COPY
        public DamageProperties copy()
        {
            DamageProperties info = (DamageProperties)this.MemberwiseClone();

            info.nonAggressive = nonAggressive;
            info.directDamage = directDamage;
            info.penetrateReflect = penetrateReflect;
            info.lifeLeech = lifeLeech;
            info.nonAbsorbable = nonAbsorbable;
            info.nonResistable = nonResistable;
            info.unavoidable = unavoidable;
            info.unblockable = unblockable;
            info.nonSneakAttack = nonSneakAttack;

            return info;
        }
    }
    #endregion

    public override string ToString()
    {
        string convertedString = "";

        convertedString += (damage.fixedDamage) ? (damage.max.ToString() + " <") : (damage.min + " - " + damage.max) + " <";
        convertedString += damage.method.ToString() + " ";
        convertedString += damage.element.ToString() + " damage> ";


        if (properties.nonAggressive) convertedString += "|passive|";
        if (properties.directDamage) convertedString += "|direct|";
        if (properties.penetrateReflect) convertedString += "|penetrating|";
        if (properties.lifeLeech) convertedString += "|leeching|";
        if (properties.nonAbsorbable) convertedString += "|unabsorbable|";
        if (properties.nonResistable) convertedString += "|irresistable|";
        if (properties.unavoidable) convertedString += "|unavoidable|";
        if (properties.unblockable) convertedString += "|unblockable|";
        if (!properties.nonSneakAttack) convertedString += "|stealthy|";
        return convertedString;
    }
}
