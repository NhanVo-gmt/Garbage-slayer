using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawnerArray;
    [SerializeField] float timeToDecreaseRate = 4;
    [SerializeField] float timeToDecreaseRate2 = 10;
    [SerializeField] float timeToStopSpawnEnemy = 18;
    [SerializeField] GameObject boss;
    [SerializeField] Health healthBoss;
    [SerializeField] float timeToSpawnBoss = 16;

    float startTime = 0;
    bool decreaseRate = false; 
    bool decreaseRate2 = false; 
    bool spawnBoss = false; 
    bool stopSpawnEnemy = false; 

    void Start() 
    {
        startTime = Time.time;

        healthBoss.onDie += ToLevel2;
    }

    private void ToLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update() 
    {
        if (startTime + timeToDecreaseRate <= Time.time && !decreaseRate)
        {
            DecreaseRate(1);
            decreaseRate = true;
        }

        if (startTime + timeToDecreaseRate2 <= Time.time && !decreaseRate2)
        {
            DecreaseRate(1.5f);
            decreaseRate2 = true;
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

    private void DecreaseRate(float decreaseRate)
    {
        foreach(EnemySpawner enemySpawner in enemySpawnerArray)
        {
            enemySpawner.DecreaseSpawnRate(decreaseRate);
        }
    }
}
