using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    // Cached reference
    private Text _scoreText;
    
    private static int _score = 0;
    private static int _pumpkinAmount = 0;
    
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

    public static void ResetQuantity()
    {
        _pumpkinAmount = 0;
    }

    public static void UpdateScore()
    {
        _score++;
        Debug.Log("Coletado: " + _score);
    }

    public static void AddPumpkinQuantityOnMap(int quantity)
    {
        _pumpkinAmount += quantity;
        Debug.Log("Quantidade: " + _pumpkinAmount);
    }

    public static bool CollectedAllPumpkins()
    {
        return _pumpkinAmount == _score;
    }
}
