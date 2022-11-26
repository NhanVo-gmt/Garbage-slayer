using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> children = new List<Node>();

    protected override void OnStop()
    {
        
    }

    public override void Abort()
    {
        foreach(Node node in children)
        {
            node.Abort();
        }
    }

    public override Node Clone()
    {
        CompositeNode compositeNode = Instantiate(this);
        compositeNode.children = children.ConvertAll(n => n.Clone());
        return compositeNode;
    }
}
