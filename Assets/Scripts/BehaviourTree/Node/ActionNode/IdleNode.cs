using UnityEngine;

public class IdleNode : ActionNode
{
    public override void CopyNode(ActionNode copyNode)
    {
        
    }
    
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
