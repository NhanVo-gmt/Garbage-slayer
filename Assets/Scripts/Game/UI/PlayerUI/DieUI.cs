using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
{
    [SerializeField] GameObject dieUI;

    Health health;


    void Start() 
    {
        dieUI.SetActive(false);

        health = FindObjectOfType<Player>().GetCoreComponent<Health>();

        health.onDie += SpawnDieUI;
    }


    private void SpawnDieUI()
    {
        Time.timeScale = 0f;
        dieUI.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
