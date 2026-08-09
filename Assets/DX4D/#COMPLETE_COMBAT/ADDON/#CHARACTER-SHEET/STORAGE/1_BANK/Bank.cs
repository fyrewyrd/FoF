using Mirror;
using UnityEngine;

public partial class Storage : NetworkBehaviour
{
    public const long DEFAULT_MAX_BANK = 50;// long.MaxValue;

    [Header(" [ BANK ] ")] [Tooltip("Default = 50")]
    [SerializeField] protected LinearLong _bankMax =
        new LinearLong { baseValue = DEFAULT_MAX_BANK };
    public LinearLong bankMax { get { return _bankMax; } }
    
    public SyncListItemSlot bank = new SyncListItemSlot();
}

public partial class CharacterSheet : NetworkBehaviour
{
    public SyncListItemSlot BANK
    {
        get { return storage.bank; }
        set { storage.bank = value; }
    }
}