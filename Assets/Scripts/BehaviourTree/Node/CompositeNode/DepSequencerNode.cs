using UnityEngine;

public class DepSequencerNode : CompositeNode
{
    [SerializeField] int currentIndex = -1;


    protected override void OnStart()
    {
        currentIndex = 1;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    public override void Abort()
    {
        base.Abort();
    }

    protected override State OnUpdate()
    {
        State conditionState = children[0].Update();
        if (conditionState == State.FAILURE)
        {
            Abort();
            return State.FAILURE;
        }
        else if (conditionState == State.SUCCESS)
        {
            return RunChildState();
        }


        return State.SUCCESS;
    }
    
    State RunChildState()
    {
        State childState = children[currentIndex].Update();

        switch(childState)
        {
            case State.RUNNING:
                return State.RUNNING;
            case State.FAILURE:
                return State.FAILURE;
            case State.SUCCESS:
                currentIndex++;
                break;
        }

        return currentIndex == children.Count ? State.SUCCESS : State.RUNNING;
    }
}
