using UnityEngine;

public class InverterNode : DecoratorNode
{

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        State state = child.Update();
        if (state == State.SUCCESS)
        {
            return State.FAILURE;
        }
        else if (state == State.FAILURE)
        {
            return State.SUCCESS;
        }
        
        return state;
    }
    

}
