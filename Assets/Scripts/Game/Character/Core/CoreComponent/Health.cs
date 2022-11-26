using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : CoreComponent
{
    [SerializeField] int health; //todo set private

    public Action onTakeDamage;
    public Action onDie;
    public Action<int> onUpdateHealth;

    HealthData data;
    RecoveryController recoveryController;

    private bool isDie = false;

    #region Set up
    
    public void SetHealth(HealthData data)
    {
        health = data.health;
        this.data = data; 
        onUpdateHealth?.Invoke(health);
    }

    #endregion
    
    protected override void Awake() 
    {
        base.Awake();

        recoveryController = GetComponent<RecoveryController>();
    }
    
    public void TakeDamage(int damage)
    {
        if (health <= 0 || IsInvulnerable()) return;

        health -= damage;

        onUpdateHealth?.Invoke(health);

        if (health > 0)
        {
            TakeDamage();
        }
        else
        {
            Die();
        }
    }

    bool IsInvulnerable() 
    {
        return recoveryController != null && recoveryController.IsInInvulnerabiltyTime();
    }

    void TakeDamage()
    {
        onTakeDamage?.Invoke();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        isDie = true;
        onDie?.Invoke();

        ResetHealth();
    }

    void ResetHealth()
    {
        health = data.health;
    }
}
