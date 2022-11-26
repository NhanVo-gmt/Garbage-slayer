using UnityEngine;

public class RandomVelocityNode : ActionNode
{
    [SerializeField] float velocity;
    
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
            movement.SetVelocityX(velocity);
        }
    }

    float RandomVelocity() 
    {
        return Random.Range(-velocity, velocity);
    }
}
