/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    //MOVE SPEED
    [ClientRpc] public void RpcSetMoveSpeed(float speed) { CmdSetMoveSpeed(speed); }
    [Command] public void CmdSetMoveSpeed(float speed) { SetMoveSpeed(speed); }
    [Server] public void SetMoveSpeed(float speed) { agent.speed = speed; }

    //SET HIDDEN
    [ClientRpc] public void RpcSetHidden(bool hidden) { CmdSetHidden(hidden); }
    [Command] public void CmdSetHidden(bool hidden) { SetHidden(hidden); }
    [Server] public void SetHidden(bool hidden) { if (hidden) { Hide(); } else { Show(); } }
}
*/
