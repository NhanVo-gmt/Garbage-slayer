using UnityEngine;

public class MoveNode : ActionNode
{
    enum MoveType
    {
        None,
        ForwardToPlayer,
        BackwardFromPlayer
    }

    enum MoveDirection
    {
        None,
        Upward,
        Downward,
        Leftward,
        Rightward
    }

    enum StopType
    {
        None,
        Frictional
    }
    
    [SerializeField] Vector2 velocity;
    [SerializeField] float moveTime;
    [SerializeField] MoveType moveType;
    [SerializeField] MoveDirection moveDirectionType;
    [SerializeField] StopType stopType;
    [SerializeField] LayerMask stopLayerMask;
    [SerializeField] bool changeFaceDirection = true;

    float startTime;
    Vector2 moveDirection;

    int moveId = Animator.StringToHash("Move");
    
    public override void CopyNode(ActionNode copyNode)
    {
        MoveNode node = copyNode as MoveNode;
        if (node)
        {
            description = node.description;
            velocity = node.velocity;
            moveTime = node.moveTime;
            moveType = node.moveType;
            stopType = node.stopType;
            changeFaceDirection = node.changeFaceDirection;
        }
    }

    protected override void PlayAnimation()
    {
        base.PlayAnimation();
        
        anim.Play(moveId);
    }
    
    protected override void OnStart()
    {
        startTime = 0;

        moveDirection = GetLastDirectionFromType();
    }

    Vector2 GetLastDirectionFromType()
    {
        Vector2 direction = Vector2.zero;
        
        switch(moveType)
        {
            case MoveType.ForwardToPlayer:
                direction = player.transform.position - treeComponent.transform.position;
                break;
            case MoveType.BackwardFromPlayer:
                direction = -(player.transform.position - treeComponent.transform.position);
                break;
            default:
                break;
        }

        direction = direction.normalized;

        switch(moveDirectionType)
        {
            case MoveDirection.Upward:
                direction.y = 1;
                break;
            case MoveDirection.Downward:
                direction.y = -1;
                break;
            case MoveDirection.Leftward:
                direction.x = -1;
                break;
            case MoveDirection.Rightward:
                direction.x = 1;
                break;
            default:
                break;
        }

        return direction;
    }

    void Move() 
    {
        if (!treeComponent.data.isFlying)
        {
            movement.SetVelocityX(velocity.x * movement.faceDirection.x, changeFaceDirection);
        }
        else
        {
            movement.SetVelocity(velocity * moveDirection, changeFaceDirection);
        }
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
    }

    protected override State OnUpdate()
    {
        Move();
        
        startTime += Time.deltaTime;
        if (startTime >= moveTime || HitLayerMask())
        {
            return State.SUCCESS;
        }
        
        return State.RUNNING;
    }
    
    bool HitLayerMask()
    {
        if (stopLayerMask == LayerMask.GetMask("Ground"))
        {
            if (collisionChecker.isGround)
            {
                return true;
            }
        }

        return false;
    }
}
