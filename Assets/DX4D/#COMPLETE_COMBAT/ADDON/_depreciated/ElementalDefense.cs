/* //DEPRECIATED
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // E L E M E N T A L  D E F E N S E
    [Header("ELEMENTAL DEFENSE")]
    
    // B A S E
    //resist
    [SerializeField]
    protected LinearFloat _baseElementalVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float baseElementalVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBaseElementalVulnerability);
            return _baseElementalVulnerability.Get(level) + buffBonus;
        }
        set { _baseElementalVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _baseElementalDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int baseElementalDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBaseElementalDamageReduction);
            return _baseElementalDamageReduction.Get(level) + buffBonus;
        }
        set { _baseElementalDamageReduction = new LinearInt { baseValue = value }; }
    }

    // F I R E
    //resist
    [SerializeField]
    protected LinearFloat _fireVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float fireVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsFireResistMultiplier);
            return _fireVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _fireVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _fireDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int fireDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsFireDamageReduction);
            return _fireDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _fireDamageReduction = new LinearInt { baseValue = value }; }
    }

    // I C E
    //resist
    [SerializeField]
    protected LinearFloat _iceVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float iceVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsIceResistMultiplier);
            return _iceVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _iceVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _iceDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int iceDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsIceDamageReduction);
            return _iceDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _iceDamageReduction = new LinearInt { baseValue = value }; }
    }


    // L I G H T N I N G
    //resist
    [SerializeField]
    protected LinearFloat _lightningVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float lightningVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsLightningResistMultiplier);
            return _lightningVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _lightningVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _lightningDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int lightningDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsLightningDamageReduction);
            return _lightningDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _lightningDamageReduction = new LinearInt { baseValue = value }; }
    }

    // W A T E R
    //resist
    [SerializeField]
    protected LinearFloat _waterVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float waterVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsWaterResistMultiplier);
            return _waterVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _waterVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _waterDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int waterDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsWaterDamageReduction);
            return _waterDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _waterDamageReduction = new LinearInt { baseValue = value }; }
    }


    // A I R
    //resist
    [SerializeField]
    protected LinearFloat _airVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float airVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsAirResistMultiplier);
            return _airVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _airVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _airDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int airDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsAirDamageReduction);
            return _airDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _airDamageReduction = new LinearInt { baseValue = value }; }
    }

    // E A R T H
    //resist
    [SerializeField]
    protected LinearFloat _earthVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float earthVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsEarthResistMultiplier);
            return _earthVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _earthVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _earthDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int earthDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsEarthDamageReduction);
            return _earthDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _earthDamageReduction = new LinearInt { baseValue = value }; }
    }


    // D A R K
    //resist
    [SerializeField]
    protected LinearFloat _darkVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float darkVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsDarkResistMultiplier);
            return _darkVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _darkVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _darkDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int darkDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsDarkDamageReduction);
            return _darkDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _darkDamageReduction = new LinearInt { baseValue = value }; }
    }

    // H O L Y
    //resist
    [SerializeField]
    protected LinearFloat _holyVulnerability = new LinearFloat { baseValue = 1.0f };
    public virtual float holyVulnerability
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsHolyResistMultiplier);
            return _holyVulnerability.Get(level) + buffBonus * baseElementalVulnerability;
        }
        set { _holyVulnerability = new LinearFloat { baseValue = value }; }
    }
    //damage reduction
    [SerializeField]
    protected LinearInt _holyDamageReduction = new LinearInt { baseValue = 0 };
    public virtual int holyDamageReduction
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsHolyDamageReduction);
            return _holyDamageReduction.Get(level) + buffBonus + baseElementalDamageReduction;
        }
        set { _holyDamageReduction = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    // B A S E
    public float buffsBaseElementalVulnerability { get { return data.buffsElementalVulnerability.Get(level); } }
    public int buffsBaseElementalDamageReduction { get { return data.buffsElementalDamageReduction.Get(level); } }

    // F I R E
    public int buffsFireResistMultiplier { get { return data.buffsFireResistMultiplier.Get(level); } }
    public int buffsFireDamageReduction { get { return data.buffsFireDamageReduction.Get(level); } }

    // I C E
    public float buffsIceResistMultiplier { get { return data.buffsIceResistMultiplier.Get(level); } }
    public int buffsIceDamageReduction { get { return data.buffsIceDamageReduction.Get(level); } }


    // L I G H T N I N G
    public float buffsLightningResistMultiplier { get { return data.buffsLightningResistMultiplier.Get(level); } }
    public int buffsLightningDamageReduction { get { return data.buffsLightningDamageReduction.Get(level); } }

    // W A T E R
    public float buffsWaterResistMultiplier { get { return data.buffsWaterResistMultiplier.Get(level); } }
    public int buffsWaterDamageReduction { get { return data.buffsWaterDamageReduction.Get(level); } }


    // A I R
    public float buffsAirResistMultiplier { get { return data.buffsAirResistMultiplier.Get(level); } }
    public int buffsAirDamageReduction { get { return data.buffsAirDamageReduction.Get(level); } }

    // E A R T H
    public float buffsEarthResistMultiplier { get { return data.buffsEarthResistMultiplier.Get(level); } }
    public int buffsEarthDamageReduction { get { return data.buffsEarthDamageReduction.Get(level); } }


    // D A R K
    public float buffsDarkResistMultiplier { get { return data.buffsDarkResistMultiplier.Get(level); } }
    public int buffsDarkDamageReduction { get { return data.buffsDarkDamageReduction.Get(level); } }

    // H O L Y
    public float buffsHolyResistMultiplier { get { return data.buffsHolyResistMultiplier.Get(level); } }
    public int buffsHolyDamageReduction { get { return data.buffsHolyDamageReduction.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // B A S E
    public LinearFloat buffsElementalVulnerability;
    public LinearInt buffsElementalDamageReduction;

    // F I R E
    public LinearInt buffsFireResistMultiplier;
    public LinearInt buffsFireDamageReduction;

    // I C E
    public LinearFloat buffsIceResistMultiplier;
    public LinearInt buffsIceDamageReduction;


    // L I G H T N I N G
    public LinearFloat buffsLightningResistMultiplier;
    public LinearInt buffsLightningDamageReduction;

    // W A T E R
    public LinearFloat buffsWaterResistMultiplier;
    public LinearInt buffsWaterDamageReduction;


    // A I R
    public LinearFloat buffsAirResistMultiplier;
    public LinearInt buffsAirDamageReduction;

    // E A R T H
    public LinearFloat buffsEarthResistMultiplier;
    public LinearInt buffsEarthDamageReduction;


    // D A R K
    public LinearFloat buffsDarkResistMultiplier;
    public LinearInt buffsDarkDamageReduction;

    // H O L Y
    public LinearFloat buffsHolyResistMultiplier;
    public LinearInt buffsHolyDamageReduction;
}
*/
/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // d e f e n s i v e
    tip.Replace("{BUFFSMAGICDEFENSEMODIFIER}", buffsMagicDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDEFENSEMODIFIER}", buffsElementalDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDEFENSEMODIFIER}", buffsBloodDamageReductionModifier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDEFENSEMODIFIER}", buffsSpiritDamageReductionModifier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // F I R E
    tip.Replace("{BUFFSFIRERESIST}", buffsFireResistMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSFIREDAMAGEREDUCTION}", buffsFireDamageReduction.Get(skillLevel).ToString());

    // I C E
    tip.Replace("{BUFFSICERESIST}", buffsIceResistMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSICEDAMAGEREDUCTION}", buffsIceDamageReduction.Get(skillLevel).ToString());

    return tip.ToString();
}
*/
