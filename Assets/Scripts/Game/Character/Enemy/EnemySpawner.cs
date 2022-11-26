using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ObjectPooling poolingManager;

    [Header("Spawn")]
    [SerializeField] float lengthX;
    [SerializeField] float lengthY;

    [Header("Enemy")]
    [SerializeField] GameObject[] spawnedEnemy;
    [SerializeField] float spawnRate;
    [SerializeField] float spawnTime = 0f;

    void Start() 
    {
        poolingManager = FindObjectOfType<ObjectPooling>();
        
        InvokeRepeating("SpawnPooledPrefab", spawnTime, spawnRate);
    }
    
    public GameObject SpawnPooledPrefab()
    {   
        if (poolingManager == null)
        {
            poolingManager = FindObjectOfType<ObjectPooling>();
        }
        GameObject spawnedPrefab = poolingManager.GetObjectFromPool(spawnedEnemy[GetRandomIndex()]);
        
        SetPrefabPosition(spawnedPrefab);

        return spawnedPrefab;
    }

    int GetRandomIndex()
    {
        return Random.Range(0, spawnedEnemy.Length);
    }


    void SetPrefabPosition(GameObject spawnedObject)
    { 
        spawnedObject.transform.position = GetRandomPosition();
    }

    Vector2 GetRandomPosition()
    {
        Vector2 pos = new Vector2(Random.Range(-lengthX, lengthX), Random.Range(-lengthY, lengthY));
        pos += (Vector2)transform.position;
        return pos;
    }

    void SetParent(GameObject spawnedObject)
    {
        spawnedObject.transform.SetParent(transform);
    }

    public void DecreaseSpawnRate(float decreaseRate)
    {
        spawnRate -= decreaseRate;
        CancelInvoke();
        InvokeRepeating("SpawnPooledPrefab", 0f, spawnRate);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;

        Vector2 startPos = new Vector2(transform.position.x - lengthX, transform.position.y - lengthY);
        Vector2 endPos = new Vector2(transform.position.x + lengthX, transform.position.y + lengthY);
        Gizmos.DrawLine(startPos, endPos);
    }

    public void CancelSpawn()
    {
        CancelInvoke();
    }
}
