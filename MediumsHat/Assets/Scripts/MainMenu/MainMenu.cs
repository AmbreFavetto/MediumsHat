using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;

    public void StartGame() {
        Time.timeScale = 1;           
        SceneManager.LoadScene(levelToLoad);    
    }

    public void SettingsButton() {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow() {
        settingsWindow.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

    
