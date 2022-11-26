using UnityEngine;

public class RandomVelocityNode : ActionNode
{
    [SerializeField] Vector2 velocityX;
    [SerializeField] Vector2 velocityY;

    Vector2 velocity;
    
    public override void CopyNode(ActionNode copyNode)
    {
        RandomVelocityNode node = copyNode as RandomVelocityNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        velocity = RandomVelocity();
        movement.SetVelocityY(velocity.y);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        Move();
        
        return State.RUNNING;
    }
    
    void Move() 
    {
        if (!treeComponent.data.isFlying)
        {
            movement.SetVelocityX(velocity.x);
        }
    }

    Vector2 RandomVelocity() 
    {
        return new Vector2(Random.Range(velocityX.x, velocityX.y), Random.Range(velocityY.x, velocityY.y));
    }
}
