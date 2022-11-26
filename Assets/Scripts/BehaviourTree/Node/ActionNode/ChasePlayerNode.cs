using UnityEngine;

public class ChasePlayerNode : ActionNode
{
    [SerializeField] float velocity;
    [SerializeField] float stoppingDistance;

    int moveId = Animator.StringToHash("Move");
    
    public override void CopyNode(ActionNode copyNode)
    {
        ChasePlayerNode node = copyNode as ChasePlayerNode;
        if (node != null)
        {
            description = node.description;
            velocity = node.velocity;
            stoppingDistance = node.stoppingDistance;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(moveId);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
    }

    protected override State OnUpdate()
    {
        if (IsAtStoppingDistance())
        {
            return State.SUCCESS;
        }

        ChasePlayer();
        return State.RUNNING;
    }


    void ChasePlayer()
    {
        if (!treeComponent.data.isFlying)
        {
            if (player.transform.position.x > treeComponent.transform.position.x)
            {
                movement.SetVelocityX(velocity);
            }
            else
            {
                movement.SetVelocityX(-velocity);
            }
        }
        else
        {
            Vector2 direction = (player.transform.position - treeComponent.transform.position).normalized;
            movement.SetVelocity(direction * velocity);
        }
    }

    bool IsAtStoppingDistance()
    {
        if (Mathf.Abs(Vector2.SqrMagnitude(treeComponent.transform.position - player.transform.position)) < stoppingDistance * stoppingDistance)
        {
            return true;
        }

        return false;
    }


    public override void Abort()
    {
        base.Abort();

        movement.SetVelocityZero();
    }

#if UNITY_EDITOR
    public override void DrawGizmos()
    {
        BehaviourTreeDrawingGizmos.DrawWireSphere(stoppingDistance);
    }

#endif
}
