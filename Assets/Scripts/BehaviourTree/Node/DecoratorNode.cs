using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : Node
{
    public Node child;

    public override Node Clone()
    {
        DecoratorNode decoratorNode = Instantiate(this);
        decoratorNode.child = child.Clone();
        return decoratorNode;
    }
}
