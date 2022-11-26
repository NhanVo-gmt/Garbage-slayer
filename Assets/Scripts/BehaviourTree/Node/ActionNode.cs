using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Movement movement { get => _movement ??= treeComponent.core.GetCoreComponent<Movement>(); }
    private Movement _movement;

    protected AnimatorController anim { get => _anim ??= treeComponent.core.GetCoreComponent<AnimatorController>(); }
    private AnimatorController _anim;

    protected CollisionChecker collisionChecker { get => _collisionChecker ??= treeComponent.core.GetCoreComponent<CollisionChecker>(); }
    private CollisionChecker _collisionChecker;

    protected override void OnStart()
    {
        PlayAnimation();
        AddAnimationEvent();
    }

    protected override void OnStop()
    {
        RemoveAnimationEvent();
    }

    protected virtual void PlayAnimation()
    {
        if (anim == null) return;
    }
    
    public abstract void CopyNode(ActionNode copyNode);

#region Animation Event

    protected void AddAnimationEvent()
    {
        anim.onAnimationTrigger += AnimationTrigger;
        anim.onAnimationFinishTrigger += AnimationFinishTrigger;
    }

    protected void RemoveAnimationEvent()
    {
        anim.onAnimationTrigger -= AnimationTrigger;
        anim.onAnimationFinishTrigger -= AnimationFinishTrigger;
    }

    protected virtual void AnimationTrigger(){}
    protected virtual void AnimationFinishTrigger(){}

#endregion
}
