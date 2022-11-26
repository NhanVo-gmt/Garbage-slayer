using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Node : ScriptableObject
{
    public enum State 
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    [Header("State component")]
    public State state = State.RUNNING;
    public bool started = false;
    public string guid;

    [Header("Core Component")]
    public BehaviourTreeComponent treeComponent;
    public Player player;

    [Header("Editor component")]
    public Vector2 position;
    [TextArea(5, 5)] public string description;

    public State Update() 
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state == State.SUCCESS || state == State.FAILURE)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual Node Clone() 
    {
        return Instantiate(this);
    }

    public virtual void Abort()
    {
        started = false;
        OnStop();
        state = State.FAILURE;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate(); 

#region Draw Gizmos

    public virtual void DrawGizmos() 
    {

    } 

#endregion
}
