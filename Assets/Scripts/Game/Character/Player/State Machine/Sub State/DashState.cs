using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AbilityState
{

    float lastActiveTime;
    
    public DashState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityZero();
        movement.SetGravityZero();

        player.inputManager.UseDashInput();

        lastActiveTime = Time.time;

        movement.SetVelocityX(movement.faceDirection.x * data.dashData.initialVelocity);

        SpawnVFX();

        combat.DisableCollider();
    }

    private void SpawnVFX()
    {
        vfx.SpawnPooledPrefab(data.dashData.vfx);
    }

    public override void Exit() 
    {
        movement.SetVelocityZero();
        movement.SetGravityNormal();

        combat.EnableCollider();
        
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if (player.inputManager.movementInput.x * movement.faceDirection.x == -1)
        {
            if (collisionSenses.isGround)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
            }
        }
        else if (player.inputManager.jumpInput && collisionSenses.isGround)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (player.inputManager.meleeAttackInput && player.meleeAttackState.CanAttack())
        {
            stateMachine.ChangeState(player.meleeAttackState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public bool CanDash()
    {
        return Time.time > lastActiveTime + data.dashData.cooldown;
    }
    
}

