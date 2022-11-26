using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    [SerializeField] float duration = 1;
    float startTime;

    int idleId = Animator.StringToHash("Idle");

    public override void CopyNode(ActionNode copyNode)
    {
        WaitNode node = copyNode as WaitNode;
        if (node != null)
        {
            description = node.description;
            duration = node.duration;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        startTime = Time.time;
    }

    protected override void PlayAnimation()
    {
        anim.Play(idleId);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.SUCCESS;
        }

        return State.RUNNING;
    }
}
