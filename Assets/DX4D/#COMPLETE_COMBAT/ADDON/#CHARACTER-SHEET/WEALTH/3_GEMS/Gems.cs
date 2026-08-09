using Mirror;
using UnityEngine;

public partial class Wealth : NetworkBehaviour
{
    public const long DEFAULT_MAX_GEMS = 999999;// long.MaxValue;

    [Header("GEMS")] [Tooltip("Default = 999999")]
    [SerializeField] protected LinearLong _gemsMax =
        new LinearLong { baseValue = DEFAULT_MAX_GEMS };
    public LinearLong gemsMax { get { return _gemsMax; } }


    [SyncVar, SerializeField] long _gems = 0;
    internal long gems
    {
        get { return _gems; }
        set
        {
            if (value > long.MaxValue) _gems = long.MaxValue;
            else _gems = System.Math.Max(value, 0);
        }
    }

    public int gemsGainRate = 100;
    public int gemsLossRate = 100;

    [SerializeField] [EnumButtons]  public PassiveTrigger
        gemsGainTriggers =
        PassiveTrigger.None;
    [SerializeField] [EnumButtons] public PassiveTrigger
        gemsLossTriggers =
        PassiveTrigger.None;
}

public partial class CharacterSheet : NetworkBehaviour
{
    // C U R R E N T
    public long GEMS
    {
        get { return wealth.gems; }
        set { wealth.gems = value; }
    }
}