using System.Collections.Generic;
using UnityEngine;

public class ShootAnimationNode : ActionNode
{
    public enum ShotType
    {
        OneDirectionShot,
        AllDirectionShot
    }

    protected SpawnObjectController spawnObjectController { get => _spawnObjectController ??= treeComponent.core.GetCoreComponent<SpawnObjectController>(); }
    private SpawnObjectController _spawnObjectController;
    
    [SerializeField] List<GameObject> gameObjectList;
    [SerializeField] Vector2[] spawnPosition;
    [SerializeField] ShotType shotType;
    [SerializeField] float numberEachTime;

    
    Node.State currentNodeState;
    int shootId = Animator.StringToHash("Shoot");
    int attackId = Animator.StringToHash("Attack2");

    float shootRateTime = 0;
    float shootTime = 0;

    public override void CopyNode(ActionNode copyNode)
    {
        ShootAnimationNode node = copyNode as ShootAnimationNode;
        if (node)
        {
            description = node.description;
            gameObjectList = node.gameObjectList;
            spawnPosition = node.spawnPosition;
        }
    }

    protected override void PlayAnimation()
    {
        anim.Play(shootId);
        anim.Play(attackId);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        
        currentNodeState = State.RUNNING;
        shootRateTime = 0;
        shootTime = Time.time;
    }

    private void Shoot()
    {
        switch(shotType)
        {
            case ShotType.OneDirectionShot:
            {
                for (int i = 0; i < numberEachTime; i++)
                {
                    GameObject projectile = spawnObjectController.SpawnGameObject(gameObjectList[UnityEngine.Random.Range(0, gameObjectList.Count)], spawnPosition[0]);
                }
                break;
            }
            case ShotType.AllDirectionShot:
            {
                for (int i = 0; i < spawnPosition.Length; i++)
                {
                    GameObject projectile = spawnObjectController.SpawnGameObject(gameObjectList[UnityEngine.Random.Range(0, gameObjectList.Count)], spawnPosition[i]);
                }
                break;
            }
        }
        shootRateTime = Time.time;
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
        Shoot();
    }

    protected override void AnimationFinishTrigger()
    {
        currentNodeState = State.SUCCESS;
    }

#endregion
    

}
