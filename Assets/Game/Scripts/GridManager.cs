using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public float spreadTime = 10f;
    public float wallTime = 10f;

    [Header("Grid Values")]
    [SerializeField] int _width;
    [SerializeField] int _height;

    [Header("Assignables")]
    [SerializeField] Hexagon _tilePrefab;
    [SerializeField] Transform tileParent;
    [SerializeField] LevelCreator levelCreator;

    public Material[] materials;

    public int redCount = 0;
    public int yellowCount = 0;
    public int greenCount = 0;
    public int totalCount = 0;

    public Dictionary<Vector2, Hexagon> tiles = new Dictionary<Vector2, Hexagon>();

    // Spacing values
    public static float xDist = 3.2f;
    public static float yDist = 0.9f;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        totalCount = _width * _height;
        GenerateGrid();
    }

    private void Update() {
        QuickMaths();
    }

    /// <summary>
    /// Generates a gridth as per width and height
    /// </summary>
    void GenerateGrid() {

        // Positioning the tiles
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {

                var spawnedTile = Instantiate(_tilePrefab);

                spawnedTile.transform.position = new Vector3(x * xDist, 0, y * yDist);

                if (y % 2 == 0)
                    spawnedTile.transform.position += new Vector3(xDist / 2, 0);

                spawnedTile.transform.parent = tileParent;
                spawnedTile.name = $"{x} {y}";
                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        // Assigning data for borders
        foreach (var key in tiles.Keys)
            tiles[key].SetDirectionData();

        levelCreator.SpawnTrees();
    }

    /// <summary>
    /// Finds tile by 2D Index of tile
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Hexagon GetTileByIndex(Vector2 pos) {
        if (tiles.ContainsKey(pos))
            return tiles[pos];
        return null;
    }


    void QuickMaths()
    {
        redCount = 0;
        greenCount= 0;
        yellowCount = 0;

        foreach (var item in tiles)
        {
            if(item.Value.currentColor == Hexagon.Colors.Red)
            {
                redCount++;
            }
            else if (item.Value.currentColor == Hexagon.Colors.Green)
            {
                greenCount++;
            }
            else if (item.Value.currentColor == Hexagon.Colors.Yellow)
            {
                yellowCount++;
            }
        }

        //totalCount = redCount + greenCount + yellowCount;
    }
}
