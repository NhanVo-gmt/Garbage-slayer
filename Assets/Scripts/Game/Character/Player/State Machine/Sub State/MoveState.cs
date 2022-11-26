using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundState
{
    public MoveState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();

        soundManager.PlayMoveClip();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (player.inputManager.movementInput.magnitude == 0)
        {
            movement.SetVelocityZero();
            
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        Move();
    }

    void Move() 
    {
        movement.SetVelocityX(player.inputManager.movementInput.x * data.moveData.velocity);
    }
}
