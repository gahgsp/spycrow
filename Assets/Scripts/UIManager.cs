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
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(3);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
