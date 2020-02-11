using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackToMainMenu()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(0);
    }

    public static void GoToDeathScreen()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(2);
    }

    public static void GoToWinScreen()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
