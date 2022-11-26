using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectData : ScriptableObject
{
    [Header("Pooled Object")]
    public GameObject pooledObject;
    public bool needPlayerDirection;
    public Vector2 spawnPos;
    public float lifeTime;
}
