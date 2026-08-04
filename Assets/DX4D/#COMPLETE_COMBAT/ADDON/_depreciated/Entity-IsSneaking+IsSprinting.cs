/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [SerializeField] public bool isCamouflaged = false;
    [SerializeField] public bool isSneaking = false;
    [SerializeField] public bool isSprinting = false;

    private void FixedUpdate()
    {
        //moveState == MoveState.SPRINTING || 
        if (Input.GetKey(sprintKey) && !UIUtils.AnyInputActive()) { isSprinting = true; } else { isSprinting = false; }

        //moveState == MoveState.SNEAKING ||
        if (Input.GetKey(sneakKey) && !UIUtils.AnyInputActive()) { isSneaking = true; } else { isSneaking = false; }

        //moveState == MoveState.CAMO || 
        if (Input.GetKey(camoKey) && !UIUtils.AnyInputActive()) { isCamouflaged = true; } else { isCamouflaged = false; }
    }
}
*/
