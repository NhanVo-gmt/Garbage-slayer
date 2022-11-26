using UnityEngine;

public class AttackNode : ActionNode
{    
    Node.State currentNodeState;
    
    int attackId = Animator.StringToHash("Attack");
    
    public override void CopyNode(ActionNode copyNode)
    {
        AttackNode node = copyNode as AttackNode;
        if (node)
        {
            description = node.description;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(attackId);
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        currentNodeState = State.RUNNING;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        return currentNodeState;
    }
    
    void Attack() 
    {
        
    }

#region Animation Event

    protected override void AnimationTrigger()
    {
        Attack();
    }

    protected override void AnimationFinishTrigger()
    {
        currentNodeState = State.SUCCESS;
    }

#endregion
}
