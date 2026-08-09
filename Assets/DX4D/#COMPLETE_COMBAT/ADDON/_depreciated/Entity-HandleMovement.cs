/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    public const float sprintSpeed = 8.0f;
    public const float normalSpeed = 5.0f;
    public const float sneakSpeed = 3.5f;
    public const float overloadedSpeed = 2.5f;

    //[SerializeField] bool canMove = true;
    //[SerializeField] bool isResting = false;
    //GameObject skin;

    [Server] public virtual void HandleMovement()
    {
        if (IsDead) return;
        
        //Reveal hidden players
        //if (!invincible && !isSneaking && IsHidden()) { RpcSetHidden(false); }// Show(); } //DEPRECIATED
        if (state == "IDLE")
        {
            HandleTrigger(PassiveTrigger.OnIdle);
            //GainStamina(staminaGainRate);

            /* //DEPRECIATED
            //RpcGainStamina(staminaGainRate);

            if (ActiveMoveState != MoveState.IDLE)
            {
                if (agent.speed != normalSpeed) RpcSetMoveSpeed(normalSpeed);
                ActiveMoveState = MoveState.IDLE;
            }
        }
        else if (state == "MOVING")
        {
            HandleTrigger(PassiveTrigger.OnMoving);
            //DepleteStamina(staminaMovementCost);

            if (onlyLoseStaminaWhenSprinting)
            {
                if (isSprinting)
                {
                    DepleteStamina(staminaMovementCost);
                }
            }
            else
            {
                DepleteStamina(staminaMovementCost);
            }

            //DEPRECIATED
            if (LowStamina)
            {
                if (agent.speed != sprintSpeed) RpcSetMoveSpeed(overloadedSpeed);
                    ActiveMoveState = MoveState.EXHAUSTED;
            }
            else if (isSneaking)
            {
                if (ActiveMoveState != MoveState.SNEAKING)
                {
                    if (agent.speed != sneakSpeed) RpcSetMoveSpeed(sneakSpeed);
                    if (!IsHidden()) { RpcSetHidden(true); }// Hide(); }
                    ActiveMoveState = MoveState.SNEAKING;
                }
            }
            else if (isSprinting)
            {
                if (ActiveMoveState != MoveState.SPRINTING)
                {
                    if(agent.speed != sprintSpeed) RpcSetMoveSpeed(sprintSpeed);
                    ActiveMoveState = MoveState.SPRINTING;
                }
            }
            else
            {
                if (ActiveMoveState != MoveState.WALKING)
                {
                    if (agent.speed != normalSpeed) RpcSetMoveSpeed(normalSpeed);
                    ActiveMoveState = MoveState.WALKING;
                }
            }

            // SPEED CHANGES
            moveSpeedMultiplier = DX4D.Tools.PercentToMagnitude((int)ActiveMoveState);//moveSpeedMultiplier

            //Sneak();
            //Sprint();
            //SpeedUp(); 

            //if (moveSpeedDecreased) { SlowDown(); }
            //if (moveSpeedIncreased) { SpeedUp(); }
        }
        //if (state == "IDLE") { GainStamina(staminaGainRate); }
        //else if (state == "MOVING") { DepleteStamina(staminaMovementCost); }

        //UnityEngine.Debug.Log("STAMINA: " + stamina.ToString()); //DEBUG
    }
}

    */
