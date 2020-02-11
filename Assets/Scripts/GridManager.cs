using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;

    private GameObject _firstTile;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGridTerrain();
        _firstTile = GetFirstTileOnGrid();
    }

    private void GenerateGridTerrain()
    {
        // Calculate the sizes of the terrain
        float TERRAIN_X_SIZE = GetComponent<Collider>().bounds.extents.x * 2;
        float TERRAIN_Z_SIZE = GetComponent<Collider>().bounds.extents.z * 2;
        // Here we get the starting position on the grid from where the tiles will be spawned
        Vector3 gridTileStartingPos = transform.position - new Vector3(TERRAIN_X_SIZE / 2, 0, TERRAIN_Z_SIZE / 2);
        // Number of rows and cols in the grid
        float NUM_ROWS = (TERRAIN_X_SIZE) / Tile.XZ_SIZE;
        float NUM_COLS = (TERRAIN_Z_SIZE) / Tile.XZ_SIZE;
        // Spawn each tile in its position based on the grid
        for (int row = 0; row < NUM_ROWS; row++)
        {
            for (int col = 0; col < NUM_COLS; col++)
            {
                SpawnTileAtPosition(gridTileStartingPos, row, col);
            }
        }
    }

    private void SpawnTileAtPosition(Vector3 startingSpawnPosition, float row, float col)
    {
        // Align the tiles with the terrain
        Vector3 TILE_OFFSET = new Vector3(Tile.XZ_SIZE / 2.0f, 0, Tile.XZ_SIZE / 2.0f);
        // Converting row and column to x and z coordinates
        Vector3 xyzPosition = new Vector3(row * Tile.XZ_SIZE, 0, col * Tile.XZ_SIZE);
        // Instantiate the tiles on the grid
        GameObject tile = Instantiate(tilePrefab,
            (startingSpawnPosition + TILE_OFFSET + xyzPosition), Quaternion.identity, transform);

        // FIXME: This should be a generic random seed generator
        var tileScript = tile.GetComponent<Tile>();
        var range = Random.Range(0, 10);
        if (range == 0)
        {
            tileScript.SetTileType(Tile.TileType.SCARECROW);
        }
        else if (range == 1 || range == 2 || range == 3 || range == 4 || range == 5 || range == 6)
        {
            tileScript.SetTileType(Tile.TileType.GRASS);
        }
        else
        {
            tileScript.SetTileType(Tile.TileType.PLANT);
            ScoreManager.AddPumpkinQuantityOnMap(1);
        }
    }

    private GameObject GetFirstTileOnGrid()
    {
        return gameObject.transform.GetChild(0).gameObject;
    }

    public float MinPositionX()
    {
        return _firstTile.transform.position.x;
    }

    public float MaxPositionX()
    {
        return Mathf.Abs(_firstTile.transform.position.x);
    }

    public float MinPositionZ()
    {
        return _firstTile.transform.position.z;
    }

    public float MaxPositionZ()
    {
        return Mathf.Abs(_firstTile.transform.position.z);
    }
}