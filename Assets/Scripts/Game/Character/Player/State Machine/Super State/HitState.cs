using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    protected Movement movement { get => _movement ??= core.GetCoreComponent<Movement>(); }
    private Movement _movement;
    
    public HitState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        soundManager.PlayHitClip();
        
        movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();

        movement.SetVelocityZero();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(player.idleState);
    }
}
