using UnityEngine;

public class FlyToPlayerNode : ActionNode
{
    [SerializeField] float velocity;
    
    public override void CopyNode(ActionNode copyNode)
    {
        FlyToPlayerNode node = copyNode as FlyToPlayerNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        FlyToPlayer();
    }

    void FlyToPlayer() 
    {
        Vector2 direction = player.transform.position - treeComponent.transform.position;
        direction = direction.normalized;

        movement.SetVelocity(velocity * direction);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        return State.RUNNING;
    }
    

}
