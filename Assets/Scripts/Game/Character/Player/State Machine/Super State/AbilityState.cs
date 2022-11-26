using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : State
{
    protected Movement movement { get => _movement ??= core.GetCoreComponent<Movement>(); }
    private Movement _movement;

    protected Combat combat { get => _combat ??= core.GetCoreComponent<Combat>(); }
    private Combat _combat;
    
    public AbilityState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.DisableChangeState();
    }


    public override void Exit()
    {
        base.Exit();

        stateMachine.EnableChangeState();
    }


    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (collisionSenses.isGround)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }
}
