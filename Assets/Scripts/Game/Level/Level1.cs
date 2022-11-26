using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawnerArray;
    [SerializeField] float timeToDecreaseRate = 4;
    [SerializeField] float timeToStopSpawnEnemy = 10;
    [SerializeField] GameObject boss;
    [SerializeField] float timeToSpawnBoss = 8;

    float startTime = 0;
    bool decreaseRate = false; 
    bool spawnBoss = false; 
    bool stopSpawnEnemy = false; 

    void Start() 
    {
        startTime = Time.time;
    }

    void Update() 
    {
        if (startTime + timeToDecreaseRate <= Time.time && !decreaseRate)
        {
            DecreaseRate();
        }

        if (startTime + timeToSpawnBoss <= Time.time && !spawnBoss)
        {
            SpawnBoss();
        }

        if (startTime + timeToStopSpawnEnemy <= Time.time && !stopSpawnEnemy)
        {
            StopSpawnEnemy();
        }
    }

    private void StopSpawnEnemy()
    {
        foreach(EnemySpawner enemySpawner in enemySpawnerArray)
        {
            enemySpawner.CancelInvoke();
            enemySpawner.gameObject.SetActive(false);
        }
        stopSpawnEnemy = true;
    }

    private void SpawnBoss()
    {
        boss.SetActive(true);
        spawnBoss = true;
    }

    private void DecreaseRate()
    {
        foreach(EnemySpawner enemySpawner in enemySpawnerArray)
        {
            enemySpawner.spawnRate -= 1f;
        }

        decreaseRate = true;
    }
}
