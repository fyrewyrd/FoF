/*
using Mirror;
using System.Linq;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // E L E M E N T A L  D A M A G E
    [Header("ELEMENTAL DAMAGE")]

    // B A S E
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _baseElementalDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float baseElementalDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsBaseElementalDamageMultiplier);
            return _baseElementalDamageMultiplier.Get(level) + buffBonus;
        }
        set { _baseElementalDamageMultiplier = new LinearFloat { baseValue = value }; }
    }

    //DamageBonus
    [SerializeField]
    protected LinearInt _baseElementalDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int baseElementalDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsBaseElementalDamageBonus);
            return _baseElementalDamageBonus.Get(level) + buffBonus;
        }
        set { _baseElementalDamageBonus = new LinearInt { baseValue = value }; }
    }


    // F I R E
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _fireDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float fireDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsFireDamageMultiplier);
            return _fireDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _fireDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _fireDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int fireDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsFireDamageBonus);
            return _fireDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _fireDamageBonus = new LinearInt { baseValue = value }; }
    }

    // I C E
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _iceDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float iceDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsIceDamageMultiplier);
            return _iceDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _iceDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _iceDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int iceDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsIceDamageBonus);
            return _iceDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _iceDamageBonus = new LinearInt { baseValue = value }; }
    }


    // L I G H T N I N G
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _lightningDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float lightningDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsLightningDamageMultiplier);
            return _lightningDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _lightningDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _lightningDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int lightningDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsLightningDamageBonus);
            return _lightningDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _lightningDamageBonus = new LinearInt { baseValue = value }; }
    }

    // W A T E R
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _waterDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float waterDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsWaterDamageMultiplier);
            return _waterDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _waterDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _waterDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int waterDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsWaterDamageBonus);
            return _waterDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _waterDamageBonus = new LinearInt { baseValue = value }; }
    }


    // A I R
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _airDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float airDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsAirDamageMultiplier);
            return _airDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _airDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _airDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int airDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsAirDamageBonus);
            return _airDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _airDamageBonus = new LinearInt { baseValue = value }; }
    }

    // E A R T H
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _earthDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float earthDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsEarthDamageMultiplier);
            return _earthDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _earthDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _earthDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int earthDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsEarthDamageBonus);
            return _earthDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _earthDamageBonus = new LinearInt { baseValue = value }; }
    }


    // D A R K
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _darkDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float darkDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsDarkDamageMultiplier);
            return _darkDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _darkDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _darkDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int darkDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsDarkDamageBonus);
            return _darkDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _darkDamageBonus = new LinearInt { baseValue = value }; }
    }

    // H O L Y
    //DamageMultiplier
    [SerializeField]
    protected LinearFloat _holyDamageMultiplier = new LinearFloat { baseValue = 1.0f };
    public virtual float holyDamageMultiplier
    {
        get
        {
            float buffBonus = buffs.Sum(buff => buff.buffsHolyDamageMultiplier);
            return _holyDamageMultiplier.Get(level) + buffBonus * baseElementalDamageMultiplier;
        }
        set { _holyDamageMultiplier = new LinearFloat { baseValue = value }; }
    }
    //DamageBonus
    [SerializeField]
    protected LinearInt _holyDamageBonus = new LinearInt { baseValue = 0 };
    public virtual int holyDamageBonus
    {
        get
        {
            int buffBonus = buffs.Sum(buff => buff.buffsHolyDamageBonus);
            return _holyDamageBonus.Get(level) + buffBonus + baseElementalDamageBonus;
        }
        set { _holyDamageBonus = new LinearInt { baseValue = value }; }
    }
}

public partial struct Buff
{
    // B A S E
    public float buffsBaseElementalDamageMultiplier { get { return data.buffsBaseElementalDamageMultiplier.Get(level); } }
    public int buffsBaseElementalDamageBonus { get { return data.buffsBaseElementalDamageBonus.Get(level); } }


    // F I R E
    public float buffsFireDamageMultiplier { get { return data.buffsFireDamageMultiplier.Get(level); } }
    public int buffsFireDamageBonus { get { return data.buffsFireDamageBonus.Get(level); } }

    // I C E
    public float buffsIceDamageMultiplier { get { return data.buffsIceDamageMultiplier.Get(level); } }
    public int buffsIceDamageBonus { get { return data.buffsIceDamageBonus.Get(level); } }


    // L I G H T N I N G
    public float buffsLightningDamageMultiplier { get { return data.buffsLightningDamageMultiplier.Get(level); } }
    public int buffsLightningDamageBonus { get { return data.buffsLightningDamageBonus.Get(level); } }

    // W A T E R
    public float buffsWaterDamageMultiplier { get { return data.buffsWaterDamageMultiplier.Get(level); } }
    public int buffsWaterDamageBonus { get { return data.buffsWaterDamageBonus.Get(level); } }


    // A I R
    public float buffsAirDamageMultiplier { get { return data.buffsAirDamageMultiplier.Get(level); } }
    public int buffsAirDamageBonus { get { return data.buffsAirDamageBonus.Get(level); } }

    // E A R T H
    public float buffsEarthDamageMultiplier { get { return data.buffsEarthDamageMultiplier.Get(level); } }
    public int buffsEarthDamageBonus { get { return data.buffsEarthDamageBonus.Get(level); } }


    // D A R K
    public float buffsDarkDamageMultiplier { get { return data.buffsDarkDamageMultiplier.Get(level); } }
    public int buffsDarkDamageBonus { get { return data.buffsDarkDamageBonus.Get(level); } }

    // H O L Y
    public float buffsHolyDamageMultiplier { get { return data.buffsHolyDamageMultiplier.Get(level); } }
    public int buffsHolyDamageBonus { get { return data.buffsHolyDamageBonus.Get(level); } }
}

public abstract partial class BuffSkill : BonusSkill
{
    // B A S E
    public LinearFloat buffsBaseElementalDamageMultiplier;
    public LinearInt buffsBaseElementalDamageBonus;


    // F I R E
    public LinearFloat buffsFireDamageMultiplier;
    public LinearInt buffsFireDamageBonus;

    // I C E
    public LinearFloat buffsIceDamageMultiplier;
    public LinearInt buffsIceDamageBonus;


    // L I G H T N I N G
    public LinearFloat buffsLightningDamageMultiplier;
    public LinearInt buffsLightningDamageBonus;

    // W A T E R
    public LinearFloat buffsWaterDamageMultiplier;
    public LinearInt buffsWaterDamageBonus;


    // A I R
    public LinearFloat buffsAirDamageMultiplier;
    public LinearInt buffsAirDamageBonus;

    // E A R T H
    public LinearFloat buffsEarthDamageMultiplier;
    public LinearInt buffsEarthDamageBonus;


    // D A R K
    public LinearFloat buffsDarkDamageMultiplier;
    public LinearInt buffsDarkDamageBonus;

    // H O L Y
    public LinearFloat buffsHolyDamageMultiplier;
    public LinearInt buffsHolyDamageBonus;
}
*/
/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // o f f e n s i v e
    tip.Replace("{BUFFSMAGICDAMAGEMODIFIER}", buffsMagicDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSELEMENTALDAMAGEMODIFIER}", buffsElementalDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSBLOODDAMAGEMODIFIER}", buffsBloodDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSSPIRITDAMAGEMODIFIER}", buffsSpiritDamageMultiplier.Get(skillLevel).ToString());

    return tip.ToString();
}
*/

/* TODO
// tooltip
public override string ToolTip(int skillLevel, bool showRequirements = false)
{
    StringBuilder tip = new StringBuilder(base.ToolTip(skillLevel, showRequirements));

    // F I R E
    tip.Replace("{BUFFSFIREDamageMultiplier}", buffsFireDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSFIREDAMAGEREDUCTION}", buffsFireDamageReduction.Get(skillLevel).ToString());

    // I C E
    tip.Replace("{BUFFSICEDamageMultiplier}", buffsIceDamageMultiplier.Get(skillLevel).ToString());
    tip.Replace("{BUFFSICEDAMAGEREDUCTION}", buffsIceDamageReduction.Get(skillLevel).ToString());

    return tip.ToString();
}
*/
