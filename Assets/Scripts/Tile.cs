using UnityEngine;

public class Tile : MonoBehaviour
{

    // Materials
    public Material grassMaterial;
    public Material plantMaterial;
    public Material scarecrowMaterial;
    
    // Objects to spawn
    public GameObject pumpkin;
    public GameObject scarecrow;
    
    // Consts
    static public float XZ_SIZE = 1.0f;

    // Available types for a ground tile
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

    public bool IsGrass()
    {
        return _currTileType == TileType.GRASS;
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
}
