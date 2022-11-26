using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    PooledObject pooledObject;

    void Start() 
    {
        pooledObject = GetComponent<PooledObject>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Combat>(out Combat combat))
            {
                combat.SlowDown();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Combat>(out Combat combat))
            {
                combat.MoveNormal();
            }
        }
    }

    public void Release()
    {
        pooledObject.Release();
    }
}
