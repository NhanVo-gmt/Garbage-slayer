using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbilityState
{
    public JumpState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    void Jump() 
    {
        player.inputManager.UseJumpInput();
        movement.SetVelocityY(data.jumpData.velocity);

        soundManager.PlayJumpClip();

        SpawnVFX();
    }

    private void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.jumpData.jumpVFX);
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (collisionSenses.isGround && movement.GetVelocity().y < 0.1f)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            stateMachine.ChangeState(player.inAirState);
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
