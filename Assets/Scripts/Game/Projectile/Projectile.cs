using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileData data;
    Vector2 direction;

    Rigidbody2D rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(ProjectileData data, Vector2 direction)
    {
        this.data = data;
        this.direction = direction;
    }

    private void Update() {
        Move();
    }

    private void Move()
    {
        rb.velocity = direction * data.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, direction); //hardcode
        }
    }
}
