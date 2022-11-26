using System;
using UnityEngine;

public class ShootNode : ActionNode
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical,
        Diagonal,
        TowardsPlayer
    }

    public enum MoveEffect
    {
        None,
        Homing
    }

    protected SpawnObjectController spawnObjectController { get => _spawnObjectController ??= treeComponent.core.GetCoreComponent<SpawnObjectController>(); }
    private SpawnObjectController _spawnObjectController;
    
    [SerializeField] Projectile projectile;
    [SerializeField] MoveDirection direction;
    [SerializeField] MoveEffect effect;

    
    Node.State currentNodeState;
    int shootId = Animator.StringToHash("Shoot");

    
    public override void CopyNode(ActionNode copyNode)
    {
        ShootNode node = copyNode as ShootNode;
        if (node)
        {
            description = node.description;
            projectile = node.projectile;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(shootId);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        currentNodeState = State.RUNNING;
    }

    private void Shoot()
    {
        Projectile projectile = spawnObjectController.SpawnPooledPrefab(treeComponent.data.rangeAttackData.projectileData).GetComponent<Projectile>();
        projectile.Initialize(treeComponent.data.rangeAttackData.projectileData, Vector2.left); //todo
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return currentNodeState;
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
