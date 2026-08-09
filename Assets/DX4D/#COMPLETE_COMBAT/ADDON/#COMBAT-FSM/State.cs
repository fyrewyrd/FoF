using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header(" [ ACTIVE STATE (FSM) ] ")]
    [SyncVar, SerializeField] internal ActiveState state = ActiveState.IDLE;
}
