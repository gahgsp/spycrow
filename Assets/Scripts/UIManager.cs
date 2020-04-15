using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private DifficultyManager _difficultyManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        _difficultyManager = FindObjectOfType<DifficultyManager>();
    }

    public void LoadNextLevel()
    {
        ScoreManager.ResetQuantity();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToEasyGame()
    {
        _difficultyManager.SetEasyDifficulty();
        LoadNextLevel();
    }
    
    public void GoToNormalGame()
    {
        _difficultyManager.SetNormalDifficulty();
        LoadNextLevel();
    }
    
    public void GoToHardGame()
    {
        _difficultyManager.SetHardDifficulty();
        LoadNextLevel();
    }
    
    public void Retry()
    {
        ScoreManager.ResetQuantity();
        SceneManager.LoadScene(2);
    }

    public void BackToMainMenu()
    {
        ScoreManager.ResetQuantity();
        SceneManager.LoadScene(0);
    }

    public static void GoToDeathScreen()
    {
        ScoreManager.ResetQuantity();
        SceneManager.LoadScene(3);
    }

    public static void GoToWinScreen()
    {
        ScoreManager.ResetQuantity();
        SceneManager.LoadScene(4);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
