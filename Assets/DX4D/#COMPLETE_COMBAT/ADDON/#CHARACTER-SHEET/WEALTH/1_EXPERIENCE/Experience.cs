using Mirror;
using UnityEngine;

public partial class Wealth : NetworkBehaviour
{
    public const long DEFAULT_MAX_EXP = long.MaxValue;

    [Header("EXPERIENCE")][Tooltip("Default = long.MaxValue;")]
    [SerializeField] protected LinearLong _expMax =
        new LinearLong { baseValue = DEFAULT_MAX_EXP };
    public LinearLong expMax { get { return _expMax; } }
    [SyncVar, SerializeField] long _exp = 0;
    internal long exp
    {
        get { return _exp; }
        set
        {
            if (value > long.MaxValue) _exp = long.MaxValue;
            else _exp = System.Math.Max(value, 0);
        }
    }

    public int expGainRate = 0;
    public int expLossRate = 0;

    [SerializeField] [EnumButtons]  public PassiveTrigger
        expGainTriggers =
        PassiveTrigger.None;
    [SerializeField] [EnumButtons] public PassiveTrigger
        expLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // C U R R E N T
    public long EXP
    {
        get { return wealth.exp; }
        set { wealth.exp = value; }
    }
}