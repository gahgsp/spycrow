using UnityEngine;

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
        float terrainXSize = GetComponent<Collider>().bounds.extents.x * 2;
        float terrainZSize = GetComponent<Collider>().bounds.extents.z * 2;
        // Here we get the starting position on the grid from where the tiles will be spawned
        Vector3 gridTileStartingPos = transform.position - new Vector3(terrainXSize / 2, 0, terrainZSize / 2);
        // Number of rows and cols in the grid
        float numRows = (terrainXSize) / Tile.XZ_SIZE;
        float numCols = (terrainZSize) / Tile.XZ_SIZE;
        // Spawn each tile in its position based on the grid
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                SpawnTileAtPosition(gridTileStartingPos, row, col);
            }
        }
        GetFirstGrassTileOnGrid().SpawnCrow();
    }

    private void SpawnTileAtPosition(Vector3 startingSpawnPosition, float row, float col)
    {
        // Align the tiles with the terrain
        Vector3 tileOffset = new Vector3(Tile.XZ_SIZE / 2.0f, 0, Tile.XZ_SIZE / 2.0f);
        // Converting row and column to x and z coordinates
        Vector3 xyzPosition = new Vector3(row * Tile.XZ_SIZE, 0, col * Tile.XZ_SIZE);
        // Instantiate the tiles on the grid
        GameObject tile = Instantiate(tilePrefab,
            (startingSpawnPosition + tileOffset + xyzPosition), Quaternion.identity, transform);

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

    private Tile GetFirstGrassTileOnGrid()
    {
        foreach (Transform tileTransform in transform)
        {
            Tile tile = tileTransform.GetComponent<Tile>();
            if (tile.IsGrass())
            {
                return tile;
            }
        }
        
        return null;
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