using UnityEngine;

public class RandomRotationNode : ActionNode
{
    [SerializeField] float rotateSpeed;
    
    public override void CopyNode(ActionNode copyNode)
    {
        RandomRotationNode node = copyNode as RandomRotationNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        RandomRotateSpeed();
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        Rotate();
        
        return State.RUNNING;
    }

    void RandomRotateSpeed()
    {
        rotateSpeed = Random.Range(0, rotateSpeed);
    }
    
    void Rotate() 
    {
        treeComponent.transform.Rotate(0, 0, treeComponent.transform.rotation.z + rotateSpeed);
    }

}
