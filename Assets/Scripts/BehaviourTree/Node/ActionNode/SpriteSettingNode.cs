using UnityEngine;

public class SpriteSettingNode : ActionNode
{
    [SerializeField] string layerName;
    [SerializeField] int order;
    [SerializeField] Color color;
    
    public override void CopyNode(ActionNode copyNode)
    {
        SpriteSettingNode node = copyNode as SpriteSettingNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        anim.SetColor(color);
        anim.SetSorting(layerName, order);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        return State.SUCCESS;
    }
    

}
