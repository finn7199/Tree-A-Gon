using System;
using UnityEngine;

public class Hexagon : MonoBehaviour
{

    /// <summary>
    /// Border possible positions
    /// </summary>
    public enum TileBorder {
        topLeft=0,
        topRight,
        right,

        downLeft,
        downRight,
        left
    }

    public bool isOccupied = false;

    public Hexagon[] borders;

    /// <summary>
    /// Finds borders by using index
    /// </summary>
    public void SetDirectionData() {
        int x = Int32.Parse(name.Split(' ')[0]);
        int y = Int32.Parse(name.Split(' ')[1]);

        borders[(int)TileBorder.topLeft] = GridManager.Instance.GetTileByIndex(new Vector2(x, y + 1));     // top left
        borders[(int)TileBorder.topRight] = GridManager.Instance.GetTileByIndex(new Vector2(x + 1, y + 1));     // top right
        borders[(int)TileBorder.right] = GridManager.Instance.GetTileByIndex(new Vector2(x + 1, y));     // right

        borders[(int)TileBorder.downLeft] = GridManager.Instance.GetTileByIndex(new Vector2(x, y - 1));     // down left
        borders[(int)TileBorder.downRight] = GridManager.Instance.GetTileByIndex(new Vector2(x + 1, y - 1));     // down right
        borders[(int)TileBorder.left] = GridManager.Instance.GetTileByIndex(new Vector2(x - 1, y));     // left
    }

    /// <summary>
    /// Finds borders by using position of tile
    /// </summary>
    public void SetDirectionDataByPosition() {
        Vector2 tilePos = transform.position;

        float x = GridManager.xDist;
        float y = GridManager.yDist;

        borders[(int)TileBorder.topLeft] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(-x / 2, y));     // top left
        borders[(int)TileBorder.topRight] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(x / 2, y));     // top right
        borders[(int)TileBorder.right] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(x, 0));     // right

        borders[(int)TileBorder.downLeft] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(-x / 2, -y));     // down left
        borders[(int)TileBorder.downRight] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(x / 2, -y));     // down right
        borders[(int)TileBorder.left] = GridManager.Instance.GetTileByIndex(tilePos + new Vector2(-x, 0));     // left
    }
}
