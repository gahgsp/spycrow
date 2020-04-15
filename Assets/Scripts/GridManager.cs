using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    private GameObject _firstTile;
    private DifficultyManager _difficultyManager;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the difficulty configurations
        _difficultyManager = FindObjectOfType<DifficultyManager>();
        // Generate the map
        GenerateGridTerrain();
        // Retrieve the first tile on the map
        _firstTile = GetFirstTileOnGrid();
        // Spawn scarecrows and pumpkins based on difficulty configuration
        SpawnScareCrows();
        SpawnPumpkins();
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

    private void SpawnTileAtPosition(Vector3 startingSpawnPosition, int row, int col)
    {
        // Align the tiles with the terrain
        Vector3 tileOffset = new Vector3(Tile.XZ_SIZE / 2.0f, 0, Tile.XZ_SIZE / 2.0f);
        // Converting row and column to x and z coordinates
        Vector3 xyzPosition = new Vector3(row * Tile.XZ_SIZE, 0, col * Tile.XZ_SIZE);
        // Instantiate the tiles on the grid
        GameObject tile = Instantiate(tilePrefab,
            (startingSpawnPosition + tileOffset + xyzPosition), Quaternion.identity, transform);
        
        var tileScript = tile.GetComponent<Tile>();
        tileScript.SetRowPosition(row);
        tileScript.SetColumnPosition(col);
    }

    private void SpawnScareCrows()
    {
        var maxQuantityToSpawn = _difficultyManager.GetSpawnableQuantity();
        var currentQuantity = 0;
        while (currentQuantity < maxQuantityToSpawn)
        {
            var rowPosition = Random.Range(0, 9);
            var colPosition = Random.Range(0, 9);
            if (!IsFirstTile(rowPosition, colPosition) && !IsAdjacentToFirstTile(rowPosition, colPosition))
            {
                if (SetTileAtPositionOfType(rowPosition, colPosition, Tile.TileType.SCARECROW))
                {
                    currentQuantity++;   
                }
            }
        }
    }
    
    private void SpawnPumpkins()
    {
        var maxQuantityToSpawn = _difficultyManager.GetSpawnableQuantity();
        var currentQuantity = 0;
        while (currentQuantity < maxQuantityToSpawn)
        {
            var rowPosition = Random.Range(0, 9);
            var colPosition = Random.Range(0, 9);
            if (!IsFirstTile(rowPosition, colPosition) && !IsAdjacentToFirstTile(rowPosition, colPosition))
            {
                if (SetTileAtPositionOfType(rowPosition, colPosition, Tile.TileType.PLANT))
                {
                    ScoreManager.AddPumpkinQuantityOnMap(1);
                    currentQuantity++;   
                }
            }
        }
    }

    private bool SetTileAtPositionOfType(int rowPosition, int columnPosition, Tile.TileType tileType)
    {
        // FIXME: Check if there is a way to improve this logic, focusing on performance.
        for (int i = 0; i < transform.childCount; i++)
        {
            var tileObjectScript = transform.GetChild(i).GetComponent<Tile>();
            if (tileObjectScript.GetRowPosition() == rowPosition &&
                tileObjectScript.GetColumnPosition() == columnPosition &&
                tileObjectScript.IsGrass())
            {
                tileObjectScript.SetTileType(tileType);
                return true;
            }
        }

        return false;
    }

    private GameObject GetFirstTileOnGrid()
    {
        return gameObject.transform.GetChild(0).gameObject;
    }

    private Tile GetFirstGrassTileOnGrid()
    {
        return GetFirstTileOnGrid().GetComponent<Tile>();
    }

    private bool IsFirstTile(int rowNum, int colNum)
    {
        return rowNum == 0 && colNum == 0;
    }

    private bool IsAdjacentToFirstTile(int rowNum, int colNum)
    {
        return (rowNum == 0 && colNum == 1) || (rowNum == 1 && colNum == 0) || (rowNum == 1 && colNum == 1);
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