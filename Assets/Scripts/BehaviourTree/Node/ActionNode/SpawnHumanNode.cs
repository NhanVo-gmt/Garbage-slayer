using System;
using UnityEngine;

public class SpawnHumanNode : ActionNode
{
    [SerializeField] GameObject prefab;
    [SerializeField] int spawnNumber;
    [SerializeField] Vector2 spawnPos;

    protected SpawnObjectController spawnObjectController { get => _spawnObjectController ??= treeComponent.core.GetCoreComponent<SpawnObjectController>(); }
    private SpawnObjectController _spawnObjectController;

    int attack1Id = Animator.StringToHash("Attack1");

    Node.State currentNodeState;
    
    public override void CopyNode(ActionNode copyNode)
    {
        SpawnHumanNode node = copyNode as SpawnHumanNode;
        if (node)
        {
            description = node.description;
        }
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        currentNodeState = State.RUNNING;
    }

    protected override void PlayAnimation()
    {
        base.PlayAnimation();

        anim.Play(attack1Id);
    }

    private void SpawnHuman()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            spawnObjectController.SpawnGameObject(prefab, GetRandomPos());
        }
    }
    
    Vector2 GetRandomPos()
    {
        return new Vector2(UnityEngine.Random.Range(spawnPos.x - 5, spawnPos.x + 5), spawnPos.y);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        return currentNodeState;
    }
    
#region Animation Event

    protected override void AnimationTrigger()
    {
        SpawnHuman();
    }

    protected override void AnimationFinishTrigger()
    {
        currentNodeState = State.SUCCESS;
    }

#endregion
}
