using Mirror;
using UnityEngine;

public partial class Wealth : NetworkBehaviour
{
    public const long DEFAULT_MAX_GOLD = 999999999999;// long.MaxValue;

    [Header("GOLD")][Tooltip("Default = 999999999999")]
    [SerializeField] protected LinearLong _goldMax =
        new LinearLong { baseValue = DEFAULT_MAX_GOLD };
    public LinearLong goldMax { get { return _goldMax; } }
    [SyncVar, SerializeField] long _gold = 0;
    internal long gold
    {
        get { return _gold; }
        set
        {
            if (value > long.MaxValue) _gold = long.MaxValue;
            else _gold = System.Math.Max(value, 0);
        }
    }

    public int goldGainRate = 0;
    public int goldLossRate = 0;

    [SerializeField] [EnumButtons]  public PassiveTrigger
        goldGainTriggers =
        PassiveTrigger.None;
    [SerializeField] [EnumButtons] public PassiveTrigger
        goldLossTriggers =
        PassiveTrigger.None;
}


public partial class CharacterSheet : NetworkBehaviour
{
    // C U R R E N T
    public long GOLD
    {
        get { return wealth.gold; }
        set { wealth.gold = value; }
    }
}