using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauzeMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //Can be used for sound change
    public static bool LargeMap = false;
    public GameObject GameUI;
    public GameObject pauseMenuUI;
    public GameObject controlsMenu;

    // Update is called once per frame
    void Update()
    {
        if ((Timer.currentTime > 0) && (Health.currentHealth != 0))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        controlsMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        GameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
    }

    public void Restart2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
