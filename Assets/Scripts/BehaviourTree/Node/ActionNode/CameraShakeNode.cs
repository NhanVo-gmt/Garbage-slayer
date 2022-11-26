using UnityEngine;

public class CameraShakeNode : ActionNode
{
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeAmount;
    [SerializeField] float frequency;

    
    public override void CopyNode(ActionNode copyNode)
    {
        CameraShakeNode node = copyNode as CameraShakeNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        CinemachineShake.Shake(shakeDuration, shakeAmount, frequency);
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
