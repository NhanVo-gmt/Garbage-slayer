using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool NeedToResetPlayerPosition;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Trap, Vector2.up); //hardcode
        }
    }
}
