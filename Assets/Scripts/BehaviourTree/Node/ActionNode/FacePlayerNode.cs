using System;
using UnityEngine;

public class FacePlayerNode : ActionNode
{
    public override void CopyNode(ActionNode copyNode)
    {
        FacePlayerNode node = copyNode as FacePlayerNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        FacePlayer();
    }

    private void FacePlayer()
    {
        if (player.transform.position.x < treeComponent.transform.position.x)
        {
            movement.ChangeDirection(-1);
        }
        else
        {
            movement.ChangeDirection(1);
        }
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
