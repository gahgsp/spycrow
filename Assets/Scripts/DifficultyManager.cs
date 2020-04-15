using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }

    private Difficulty _currDifficulty = Difficulty.NORMAL;
    private float _defaultQuantity = 10f;
    private float _easyFactor = 1f;
    private float _normalFactor = 1.5f;
    private float _hardFactor = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateSingletonObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSingletonObject()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetEasyDifficulty()
    {
        _currDifficulty = Difficulty.EASY;
    }
    
    public void SetNormalDifficulty()
    {
        _currDifficulty = Difficulty.NORMAL;
    }
    
    public void SetHardDifficulty()
    {
        _currDifficulty = Difficulty.HARD;
    }
    
    public float GetSpawnableQuantity()
    {
        switch (_currDifficulty)
        {
            case Difficulty.EASY:
                return _defaultQuantity * _easyFactor;
            case Difficulty.NORMAL:
                return _defaultQuantity * _normalFactor;
            case Difficulty.HARD:
                return _defaultQuantity * _hardFactor;
        }
        return _defaultQuantity;
    }
}
