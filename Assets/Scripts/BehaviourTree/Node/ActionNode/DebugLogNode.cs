using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;

    public override void CopyNode(ActionNode copyNode)
    {
        DebugLogNode node = copyNode as DebugLogNode;
        if (node != null)
        {
            message = node.message;
        }
    }
    
    protected override void OnStart()
    {
        Debug.Log($"OnStart{message}");
    }

    protected override void OnStop()
    {
        Debug.Log($"OnStop{message}");
    }

    protected override State OnUpdate()
    {
        Debug.Log($"OnUpdate{message}");
        return State.SUCCESS;
    }

}
