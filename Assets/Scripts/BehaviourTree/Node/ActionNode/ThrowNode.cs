using System;
using System.Collections.Generic;
using UnityEngine;

public class ThrowNode : ActionNode
{
    protected SpawnObjectController spawnObjectController { get => _spawnObjectController ??= treeComponent.core.GetCoreComponent<SpawnObjectController>(); }
    private SpawnObjectController _spawnObjectController;
    
    [SerializeField] List<GameObject> gameObjectList;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] bool needPlayerPosition = false;
    
    State currentNodeState;

    int attackId = Animator.StringToHash("Attack");
    
    public override void CopyNode(ActionNode copyNode)
    {
        ThrowNode node = copyNode as ThrowNode;
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

        anim.Play(attackId);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override State OnUpdate()
    {
        return currentNodeState;
    }

    private void Shoot()
    {
        if (needPlayerPosition)
        {
            spawnPosition = (Vector2)treeComponent.transform.position + spawnPosition;
        }
        GameObject projectile = spawnObjectController.SpawnGameObject(gameObjectList[UnityEngine.Random.Range(0, gameObjectList.Count)], spawnPosition);
    }
    
#region Animation Event

    protected override void AnimationTrigger()
    {
        Shoot();
    }

    protected override void AnimationFinishTrigger()
    {
        currentNodeState = State.SUCCESS;
    }

#endregion
}
