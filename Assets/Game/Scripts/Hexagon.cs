using System;
using System.Collections;
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

    public enum Colors { 
        None = 0, 
        Red, 
        Green, 
        Yellow,
        Wall
    };

    public Colors currentColor;

    public bool isOccupied = false;

    public bool isWall = false;

    public Hexagon[] borders;

    [Header("Assignables")]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MeshRenderer treeMesh;

    public Spread spread;
    public GameObject tree;

    /// <summary>
    /// Finds borders by using index
    /// </summary>
    public void SetDirectionData() {
        int x = Int32.Parse(name.Split(' ')[0]);
        int y = Int32.Parse(name.Split(' ')[1]);

        int offTop = y % 2 == 0 ? 1 : 0; 
        int offBot = y % 2 == 0 ? 0 : -1;

        borders[(int)TileBorder.topLeft] = GridManager.Instance.GetTileByIndex(new Vector2(x + offTop, y + 1));     // top left
        borders[(int)TileBorder.topRight] = GridManager.Instance.GetTileByIndex(new Vector2(x + offTop, y - 1));     // top right
        borders[(int)TileBorder.right] = GridManager.Instance.GetTileByIndex(new Vector2(x, y - 2));     // right

        borders[(int)TileBorder.downLeft] = GridManager.Instance.GetTileByIndex(new Vector2(x + offBot, y + 1));     // down left
        borders[(int)TileBorder.downRight] = GridManager.Instance.GetTileByIndex(new Vector2(x + offBot, y - 1));     // down right
        borders[(int)TileBorder.left] = GridManager.Instance.GetTileByIndex(new Vector2(x, y + 2));     // left
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

    public void ChangeColour(Colors colour) {
        currentColor = colour;
        meshRenderer.material = GridManager.Instance.materials[(int)colour];
        treeMesh.material = GridManager.Instance.materials[(int)colour];

        if (colour == Colors.Wall || colour == Colors.None) {
            spread.enabled = false;
            tree.SetActive(false);
        }
        else {
            spread.enabled = true;
            tree.SetActive(true);
            isOccupied = true;
            PlaceWalls.instance.IncreaseCash();
        }
    }

    private void OnMouseDown() {
        if (GridManager.isPaused)
            return;

        if (PlaceWalls.instance.cash <= 0)
            return;

        PlaceWalls.instance.firstWall = this;
        ConvertToWall();
    }

    private void OnMouseEnter() {
        if (PlaceWalls.instance.firstWall == null)
            return;

        if (PlaceWalls.instance.cash <= 0)
            return;

        ConvertToWall();
    }

    private void OnMouseUp() {
        PlaceWalls.instance.firstWall = null;
    }

    void ConvertToWall() {
        if (isWall)
            return;

        StartCoroutine(WallConversion());
    }

    IEnumerator WallConversion() {
        ChangeColour(Colors.Wall);
        isOccupied = true;
        isWall = true;
        PlaceWalls.instance.ReduceCash();

        yield return new WaitForSeconds(GridManager.Instance.wallTime);

        ChangeColour(Colors.None);
        isOccupied = false;
        isWall = false;
    }
}
