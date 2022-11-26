using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    [SerializeField] GameObject settingUI;

    Health health;


    void Start() 
    {
        settingUI.SetActive(false);
    }

    public void ToggleUI()
    {
        if (settingUI.activeSelf == true)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }


    public void OpenUI()
    {
        Time.timeScale = 0f;
        settingUI.SetActive(true);
    }

    public void CloseUI()
    {
        Time.timeScale = 1f;
        settingUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
