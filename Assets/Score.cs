using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    // Cached reference
    private Text _scoreText;
    
    private static float _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _score.ToString();
    }

    public static void ResetScore()
    {
        _score = 0;
    }

    public static void UpdateScore()
    {
        _score++;
    }
}
