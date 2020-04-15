using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour
{

    // Cached reference.
    private Text _scoreText;
    
    private static int _pumpkinAmount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _pumpkinAmount.ToString();
    }
    
    public static void ResetQuantity()
    {
        _pumpkinAmount = 0;
    }

    public static void UpdateScore()
    {
        _pumpkinAmount--;
    }

    public static void AddPumpkinQuantityOnMap(int quantity)
    {
        _pumpkinAmount += quantity;
    }

    public static bool CollectedAllPumpkins()
    {
        return _pumpkinAmount <= 0;
    }
}
