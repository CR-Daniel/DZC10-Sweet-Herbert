using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

    public void BackStorySelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BackStory");
    }

    public void SelectShop()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }

    public void SelectDemo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IceTorture");
    }

    public void SelectDemo2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IT_Hard");
    }

    public void SelectIntro()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
