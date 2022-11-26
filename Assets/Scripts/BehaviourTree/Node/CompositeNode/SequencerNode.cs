using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    int currentIndex;


    protected override void OnStart()
    {
        currentIndex = 0;
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
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
