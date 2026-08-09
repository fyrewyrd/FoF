using Mirror;
using UnityEngine;

public partial class Movement : NetworkBehaviour
{
    [Header("**not implemented** [MOVEMENT KEYS] ")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode sneakKey = KeyCode.LeftControl;
    public KeyCode camoKey = KeyCode.RightControl;
}
