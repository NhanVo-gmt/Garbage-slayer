using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState {get; private set;}

    public bool canChangeState {get; private set;}

    public void Initialize(State firstState) 
    {
        currentState = firstState;
        currentState.Enter();

        EnableChangeState();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void EnableChangeState()
    {
        canChangeState = true;
    }

    public void DisableChangeState()
    {
        canChangeState = false;
    }

    public void Update()
    {
        currentState.LogicsUpdate();
    }

    public void FixedUpdate() 
    {
        currentState.PhysicsUpdate();
    }
}
