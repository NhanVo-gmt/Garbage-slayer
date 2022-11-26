using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectController : CoreComponent
{
    ObjectPooling poolingManager;
    Movement movement;
    
    protected override void Awake()
    {
        base.Awake();

        poolingManager = FindObjectOfType<ObjectPooling>();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }

#region Spawn Pooled Prefab

    public GameObject SpawnPooledPrefab(PooledObjectData data)
    {
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(data.pooledObject);
        SetUpSpawnPrefab(spawnedPrefab, data);
        SetPrefabPosition(spawnedPrefab, data);
        SetPrefabRotation(spawnedPrefab, data);

        return spawnedPrefab;
    }

    void SetUpSpawnPrefab(GameObject spawnedPrefab, PooledObjectData data)
    {
        spawnedPrefab.GetComponent<PooledObject>().Initialize(data.lifeTime);
    }

    void SetPrefabPosition(GameObject spawnedObject, PooledObjectData data)
    { 
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.position = new Vector2(-data.spawnPos.x, data.spawnPos.y);
        }
        else
        {
            spawnedObject.transform.position = data.spawnPos;
        }

        spawnedObject.transform.position += transform.position;
    }

    void SetPrefabRotation(GameObject spawnedObject, PooledObjectData data)
    {
        if (data.needPlayerDirection && movement.faceDirection == Vector2.right)
        {
            spawnedObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spawnedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

#endregion

#region Spawn Go

public GameObject SpawnGameObject(GameObject prefab, Vector2 position)
    {
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(prefab);
        SetGameObjectPosition(spawnedPrefab, position);

        return spawnedPrefab;
    }

    void SetGameObjectPosition(GameObject spawnedObject, Vector2 position)
    { 
        spawnedObject.transform.position = position;
    }

#endregion
}
