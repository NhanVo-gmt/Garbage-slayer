using UnityEngine;

public class PatrolNode : ActionNode
{
    [SerializeField] Vector2[] pointWays;
    [SerializeField] float velocity;

    int currentIndex = -1;
    
    int moveId = Animator.StringToHash("Move");

    public override void CopyNode(ActionNode copyNode)
    {
        PatrolNode node = copyNode as PatrolNode;
        if (node != null)
        {
            description = node.description;
            pointWays = node.pointWays;
            velocity = node.velocity;
        }
    }


    protected override void OnStart()
    {
        base.OnStart();

        if (currentIndex == -1)
        {
            currentIndex = 0;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(moveId);
    }

    void MoveToNextPoint()
    {
        if (!treeComponent.data.isFlying)
        {
            if (treeComponent.transform.position.x < pointWays[currentIndex].x)
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
            Vector2 direction = (pointWays[currentIndex] - (Vector2)treeComponent.transform.position).normalized;
            movement.SetVelocity(direction * velocity);
        }
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
        SetNextIndex();
    }

    void SetNextIndex()
    {
        currentIndex++;
        if (currentIndex >= pointWays.Length)
        {
            currentIndex = 0;
        }
    } 

    protected override State OnUpdate()
    {
        MoveToNextPoint();
        
        if (!treeComponent.data.isFlying)
        {   
            if (Mathf.Abs(treeComponent.transform.position.x - pointWays[currentIndex].x) < 0.5f)
            {
                return State.SUCCESS;
            }
        }
        else if (Mathf.Abs(Vector2.SqrMagnitude((Vector2)treeComponent.transform.position - pointWays[currentIndex])) < 0.5f)
        {
            return State.SUCCESS;
        }
        return State.RUNNING;
    }

#if UNITY_EDITOR
    public override void DrawGizmos() 
    {
        BehaviourTreeDrawingGizmos.DrawPatrolLine(pointWays);
    } 

#endif
}
