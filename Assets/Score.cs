using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    // Cached reference
    private Text _scoreText;
    
    public static float score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = score.ToString();
    }
}
