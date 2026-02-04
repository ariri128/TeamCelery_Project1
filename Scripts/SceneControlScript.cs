using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    //Some functions contain placholder scene names
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }

    public void StartScreen()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
