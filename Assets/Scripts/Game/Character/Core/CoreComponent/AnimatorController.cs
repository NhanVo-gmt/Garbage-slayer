using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : CoreComponent
{

    public Action onAnimationTrigger;
    public Action onAnimationFinishTrigger;

    Health health;

    Animator anim;
    BlinkingEffect blinkingEffect;
    FlashingEffect flashingEffect;

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
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

    private void OnDisable() {
        RemoveEvent();
    }

    void RemoveEvent()
    {
        health.onTakeDamage -= StartBlinking;
        health.onTakeDamage -= StartFlashing;
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
}
