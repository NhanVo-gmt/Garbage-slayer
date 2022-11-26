using UnityEngine;

public class ColliderSettingNode : ActionNode
{
    [SerializeField] int setActive;

    protected Combat combat { get => _combat ??= treeComponent.core.GetCoreComponent<Combat>(); }
    private Combat _combat;
    
    public override void CopyNode(ActionNode copyNode)
    {
        ColliderSettingNode node = copyNode as ColliderSettingNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        if (setActive == 1)
        {
            combat.EnableCollider();
        }
        else
        {
            combat.DisableCollider();
        }
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
