using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class RecoveryController : CoreComponent
{
    HitData hitData;

    Health health;
    Combat combat;
    
    private bool isInvulnerable = false;
    float hitTime;
    bool canRecover = true;

    public void SetHitData(HitData hitData)
    {
        this.hitData = hitData;
    }

    protected override void Awake() 
    {
        base.Awake();
    }

    void Start() 
    {
        health = core.GetCoreComponent<Health>();
        combat = core.GetCoreComponent<Combat>();
        AddHealthEvent();
    }

    void AddHealthEvent()
    {
        health.onTakeDamage += TakeDamage;
        health.onDie += Die;
    }

    void RemoveHealthEvent()
    {
        health.onTakeDamage -= TakeDamage;
        health.onDie -= Die;
    }

    private void OnDisable() {
        RemoveHealthEvent();
    }
    
    void Update() 
    {
        if (hitData == null) return;

        Recovering();
    }
    
    void Recovering()
    {
        if (!IsInInvulnerabiltyTime() || !canRecover) return;

        if (hitTime + hitData.invulnerableTime < Time.time)
        {
            isInvulnerable = false;
            combat.EnableCollider();
        }
    }
    
    void TakeDamage()
    {
        hitTime = Time.time;
        isInvulnerable = true;

        combat.DisableCollider();
    }

    private void Die()
    {
        canRecover = false;

        combat.DisableCollider();
    }
    
    public bool IsInInvulnerabiltyTime()
    {
        return isInvulnerable;
    }
}
