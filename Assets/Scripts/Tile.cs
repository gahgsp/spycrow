using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Types of Tile")]
    [SerializeField] Material grassMaterial;
    [SerializeField] Material plantMaterial;
    [SerializeField] Material scarecrowMaterial;
    
    [Header("Spawnables")]
    [SerializeField] GameObject pumpkin;
    [SerializeField] GameObject scarecrow;
    [SerializeField] GameObject crow;

    [Header("Visuals")]
    [SerializeField] GameObject grass;
    
    // Consts
    public static float XZ_SIZE = 1.0f;
    
    // Available types for a ground tile.
    public enum TileType
    {
        GRASS,
        PLANT,
        SCARECROW
    }

    private TileType _currTileType = TileType.GRASS;

    // Start is called before the first frame update
    void Start()
    {
        SetTileType(_currTileType);
        SpawnPlant(_currTileType);
        SpawnScarecrow(_currTileType);
    }

    public void SetTileType(TileType newTileType)
    {
        if (newTileType == TileType.GRASS)
        {
            grass.SetActive(true);
            GetComponent<Renderer>().material = grassMaterial;
        } else if (newTileType == TileType.PLANT)
        {
            GetComponent<Renderer>().material = plantMaterial;
        } else if (newTileType == TileType.SCARECROW)
        {
            GetComponent<Renderer>().material = scarecrowMaterial;
        }
        _currTileType = newTileType;
    }
    
    private void SpawnPlant(TileType tileType)
    {
        if (tileType == TileType.PLANT)
        {
            Instantiate(pumpkin,
                new Vector3(GetComponent<Renderer>().bounds.center.x, 0.6f, GetComponent<Renderer>().bounds.center.z),
                Quaternion.identity,
                transform);
        }
    }

    private void SpawnScarecrow(TileType tileType)
    {
        if (tileType == TileType.SCARECROW)
        {
            Instantiate(scarecrow,
                new Vector3(GetComponent<Renderer>().bounds.center.x, 0.6f, GetComponent<Renderer>().bounds.center.z),
                Quaternion.identity,
                transform);
        }
    }

    public void SpawnCrow()
    {
        Instantiate(crow,
                new Vector3(GetComponent<Renderer>().bounds.center.x, 0.6f, GetComponent<Renderer>().bounds.center.z),
                Quaternion.identity,
                transform);
    }
    
    public bool IsGrass()
    {
        return _currTileType == TileType.GRASS;
    }

}
