using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNode : CompositeNode
{
    enum SuccessType
    {
        OneNode,
        AllNode,
    }
    
    [SerializeField] SuccessType successType;
    State[] childStateArray;
    
    protected override void OnStart()
    {
        InitializeChildState();
    }

    void InitializeChildState()
    {
        if (childStateArray == null)
        {
            childStateArray = new State[children.Count];
        }

        for (int i = 0; i < childStateArray.Length; i++)
        {
            childStateArray[i] = State.RUNNING;
        }

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateArray[i] == State.SUCCESS) 
            {
                if (successType == SuccessType.OneNode)
                {
                    Abort();
                    return State.SUCCESS;
                }
                continue;
            }

            childStateArray[i] = children[i].Update();
            if (childStateArray[i] == State.FAILURE)
            {
                Abort();
                return State.FAILURE;
            }
        }

        if (AllChildSuccess())
        {
            return State.SUCCESS;
        }

        return State.RUNNING;
    }
    
    bool AllChildSuccess()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (childStateArray[i] != State.SUCCESS) return false;
        }

        return true;
    }
}
