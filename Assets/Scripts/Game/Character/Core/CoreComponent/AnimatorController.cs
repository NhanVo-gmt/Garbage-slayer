using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : CoreComponent
{

    public Action onAnimationTrigger;
    public Action onAnimationFinishTrigger;

    Health health;
    SpriteRenderer sprite;

    Animator anim;
    BlinkingEffect blinkingEffect;
    FlashingEffect flashingEffect;

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        blinkingEffect = GetComponent<BlinkingEffect>();
        flashingEffect = GetComponent<FlashingEffect>();
    }

    void Start() 
    {
        health = core.GetCoreComponent<Health>();
        AddEvent();
    }

    void AddEvent()
    {
        health.onTakeDamage += StartBlinking;
        health.onTakeDamage += StartFlashing;
    }

    
#region Animation
    
    public void Play(int id) 
    {
        anim.Play(id);
    }

    public void AnimationTrigger()
    {
        onAnimationTrigger?.Invoke();
    }

    public void AnimationFinishTrigger()
    {
        onAnimationFinishTrigger?.Invoke();
    }

#endregion

#region Effect

    public void StartBlinking()
    {
        if (blinkingEffect == null) return;

        blinkingEffect.StartBlinking();
    }

    public void StartFlashing()
    {
        if (flashingEffect == null) return;

        flashingEffect.StartFlashing();
    }

#endregion

#region Spritre

    public void SetColor(Color newColor)
    {
        sprite.color = newColor;
    }

    public void SetNormalColor()
    {
        sprite.color = Color.white;
    }

    public void SetSorting(string layerName, int order)
    {
        sprite.sortingLayerName = layerName;
        sprite.sortingOrder = order;
    }

    public void SetToInstance()
    {
        sprite.sortingLayerName = "Instance";
        sprite.sortingOrder = -1;
    }

#endregion

}
