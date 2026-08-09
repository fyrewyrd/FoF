using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class Followers : NetworkBehaviour
{
    public const long DEFAULT_MAX_PETS = 5;// long.MaxValue;

    [Header("PETS")] [Tooltip("Default = 5")]
    [SerializeField] protected LinearLong _petsMax =
        new LinearLong { baseValue = DEFAULT_MAX_PETS };
    public LinearLong petsMax { get { return _petsMax; } }


    [SerializeField] SyncListCharacter _pets = new SyncListCharacter();
    public SyncListCharacter pet { get { return _pets; } set { _pets = value; } }
}

public partial class CharacterSheet : NetworkBehaviour
{
    // C U R R E N T
    public SyncListCharacter PET
    {
        get { return follower.pet; }
        set { follower.pet = value; }
    }
    public CharacterSheet ACTIVEPET
    {
        get { return follower.pet[0].GetComponent<CharacterSheet>(); }
        set { follower.pet[0] = value.gameObject; }
    }
}