using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] int _width, _height;
    [SerializeField] Hexagon _tilePrefab;
    [SerializeField] Transform tileParent;

    Dictionary<Vector2, Hexagon> tiles = new Dictionary<Vector2, Hexagon>();

    // Spacing values
    public static float xDist = 3.2f;
    public static float yDist = 0.9f;

    void Start()
    {
        Instance = this;
        GenerateGrid();
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
}
