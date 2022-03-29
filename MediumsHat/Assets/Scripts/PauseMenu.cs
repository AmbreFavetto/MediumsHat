using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gameIsPaused) {
                Resume();
            } else {
                Paused();
            }
        }
    }

    private void Paused() {
        PlayerController.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume() {
        PlayerController.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        if(inventoryUI.activeSelf == false) {
            Time.timeScale = 1;
        }
        gameIsPaused = false;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    
}
