using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class Followers : NetworkBehaviour
{
    public const long DEFAULT_MAX_MOUNTS = 1;// long.MaxValue;

    [Header(" [ MOUNTS ] ")] [Tooltip("Default = 1")]
    [SerializeField] protected LinearLong _mountsMax =
        new LinearLong { baseValue = DEFAULT_MAX_MOUNTS };
    public LinearLong mountsMax { get { return _mountsMax; } }


    [SerializeField] SyncListCharacter _mounts = new SyncListCharacter();
    public SyncListCharacter mount { get { return _mounts; } set { _mounts = value; } }
}

public partial class CharacterSheet : NetworkBehaviour
{
    // C U R R E N T
    public SyncListCharacter MOUNT
    {
        get { return follower.mount; }
        set { follower.mount = value; }
    }
    public CharacterSheet ACTIVEMOUNT
    {
        get { return follower.mount[0].GetComponent<CharacterSheet>(); }
        set { follower.mount[0] = value.gameObject; }
    }
}