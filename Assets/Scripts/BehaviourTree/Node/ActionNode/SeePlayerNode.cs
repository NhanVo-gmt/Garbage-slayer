using UnityEngine;

public class SeePlayerNode : ActionNode
{
    [SerializeField] float seeRange;

    [SerializeField] RangeType rangeType;

    enum RangeType
    {
        See,
        Attack
    }

    public override void CopyNode(ActionNode copyNode)
    {
        SeePlayerNode node = copyNode as SeePlayerNode;
        if (node != null)
        {
            description = node.description;
            seeRange = node.seeRange;
            rangeType = node.rangeType;
        }
    }

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Mathf.Abs(Vector2.SqrMagnitude(treeComponent.transform.position - player.transform.position)) < seeRange * seeRange)
        {
            return State.SUCCESS;
        }
        return State.FAILURE;
    }

#if UNITY_EDITOR
    public override void DrawGizmos()
    {
        BehaviourTreeDrawingGizmos.DrawWireSphere(seeRange);
    }
#endif
}
