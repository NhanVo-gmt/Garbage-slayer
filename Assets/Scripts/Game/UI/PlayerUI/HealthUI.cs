using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] HealthImage imagePrefab;

    List<HealthImage> healthImageList = new List<HealthImage>();

    [SerializeField] Health health; //todo set private
    int currentHealth = 1;

    void Start() 
    {
        SetUpHealthUI();
    }

    void SetUpHealthUI()
    {
        health = FindObjectOfType<Player>().GetCoreComponent<Health>();

        SpawnHealthImage();
        
        health.onUpdateHealth += UpdateHealthUI;
    }

    private void SpawnHealthImage()
    {
        for (int i = 0; i < FindObjectOfType<GameSettings>().maxHealth; i++)
        {
            HealthImage image = Instantiate(imagePrefab, transform);
            image.Disable();
            healthImageList.Add(image);
        }
    }

    void OnDisable() 
    {
        health.onUpdateHealth -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int newHealth)
    {
        if (currentHealth > newHealth)
        {
            for (int i = newHealth; i < currentHealth; i++)
            {
                healthImageList[i].Disable();
            }
        }
        else
        {
            for (int i = currentHealth; i <= newHealth; i++)
            {
                healthImageList[i - 1].Enable();
            }
        }

        currentHealth = newHealth;
    }
}
