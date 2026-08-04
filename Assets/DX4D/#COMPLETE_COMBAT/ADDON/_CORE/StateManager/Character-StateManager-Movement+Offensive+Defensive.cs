using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Command] public void Cmd() { Set(); }
    //[ClientRpc] public void Rpc() { Cmd(); }
    //[Server] public void Set() { }

    //It is very important that these are SyncVars...otherwise certain things like resists will not work in Server mode
    [SyncVar] private MoveState _moveState = MoveState.IDLE;
    public MoveState ActiveMoveState {
        get { return _moveState; }
        set
        {
            if(_moveState != value)
            {
                _moveState = value;
                TargetShowTextPopup(_moveState.ToString());
            }
        }
    }
    [SyncVar] private OffensiveState _offensiveState = OffensiveState.Idle;
    public OffensiveState ActiveOffensiveState
    {
        get { return _offensiveState; }
        set
        {
            if (_offensiveState != value)
            {
                _offensiveState = value;
                //RpcShowCombatStatePopup(_offensiveState.ToString());
            }
        }
    }
    [SyncVar] private DefensiveState _defensiveState = DefensiveState.Idle;
    public DefensiveState ActiveDefensiveState
    {
        get { return _defensiveState; }
        set
        {
            if (_defensiveState != value)
            {
                _defensiveState = value;
                //RpcShowCombatStatePopup(_defensiveState.ToString());
            }
        }
    }
}
