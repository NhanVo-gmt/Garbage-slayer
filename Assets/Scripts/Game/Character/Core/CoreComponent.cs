using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake() 
    {
        core = GetComponentInParent<Core>();

        AddCoreComponentToList();
    }

    void AddCoreComponentToList()
    {
        core.AddCoreComponent(this);
    }

    public virtual void PhysicsUpdate() {}

    public virtual void LogicsUpdate() {}
}
