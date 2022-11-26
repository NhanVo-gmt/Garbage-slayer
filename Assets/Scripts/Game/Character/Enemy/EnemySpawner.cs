using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ObjectPooling poolingManager;

    [SerializeField] GameObject[] spawnedEnemy;
    [SerializeField] float spawnRate;

    void Start() 
    {
        poolingManager = FindObjectOfType<ObjectPooling>();
        
        InvokeRepeating("SpawnPooledPrefab", 0f, spawnRate);
    }
    
    public GameObject SpawnPooledPrefab()
    {   
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(spawnedEnemy[GetRandomIndex()]);
        SetPrefabPosition(spawnedPrefab);

        return spawnedPrefab;
    }

    void Update() 
    {

    }

    int GetRandomIndex()
    {
        return Random.Range(0, spawnedEnemy.Length);
    }


    void SetPrefabPosition(GameObject spawnedObject)
    { 
        spawnedObject.transform.position = transform.position;
    }

}
