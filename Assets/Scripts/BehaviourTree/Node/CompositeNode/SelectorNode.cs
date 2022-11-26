using UnityEngine;

public class SelectorNode : CompositeNode
{
    [SerializeField] int currentIndex = -1;
    
    protected override void OnStart()
    {
        currentIndex = 0;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        State state = children[currentIndex].Update();
        
        if (state == State.RUNNING)
        {
            return State.RUNNING;
        }
        else if (state == State.SUCCESS)
        {
            return State.SUCCESS;
        }
        else
        {
            return ProceedNextChild();
        }
    }

    private State ProceedNextChild()
    {
        currentIndex++;
        if (currentIndex >= children.Count)
        {
            return State.FAILURE;
        }

        return State.RUNNING;
    }
}
