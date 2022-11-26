using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawnerArray;
    [SerializeField] float timeToBiker = 4;
    [SerializeField] GameObject biker;
    [SerializeField] float timeToHulk = 8;
    [SerializeField] GameObject hulk;
    [SerializeField] float timeToStopSpawnEnemy = 12;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject boss;
    [SerializeField] Health healthBoss;

    float startTime = 0;
    bool spawnBiker = false; 
    bool spawnHulk = false; 
    bool spawnBoss = false; 
    bool stopSpawnEnemy = false; 

    void Start() 
    {
        startTime = Time.time;

        healthBoss.onDie += ToEnding;
    }

    private void ToEnding()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update() 
    {
        if (startTime + timeToBiker <= Time.time && !spawnBiker)
        {
            SpawnBiker();
            spawnBiker = true;
        }

        if (startTime + timeToHulk <= Time.time && !spawnHulk)
        {
            SpawnHulk();
            spawnHulk = true;
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

        hulk.SetActive(false);
        biker.SetActive(false);
        floor.SetActive(false);
    }

    private void SpawnBoss()
    {
        boss.SetActive(true);
        spawnBoss = true;
    }

    private void SpawnBiker()
    {
        biker.SetActive(true);
    }

    private void SpawnHulk()
    {
        hulk.SetActive(true);
    }
}
