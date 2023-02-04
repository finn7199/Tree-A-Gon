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

    public bool isOccupied = false;

    public bool planted = false;

    public bool isWall = false;

    public Hexagon[] borders;

    [SerializeField] Material wallMaterial;

    [SerializeField] MeshRenderer meshRenderer;

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

    private void OnMouseDown() {    
        Debug.Log("clicked");

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

        Debug.Log("entered");
        ConvertToWall();
    }

    private void OnMouseUp() {
        PlaceWalls.instance.firstWall = null;
    }

    void ConvertToWall() {
        StartCoroutine(WallConversion());
    }

    IEnumerator WallConversion() {
        Material mat = meshRenderer.material;
        meshRenderer.material = wallMaterial;
        isOccupied = true;
        isWall = true;
        PlaceWalls.instance.ReduceCash();

        yield return new WaitForSeconds(10);

        meshRenderer.material = mat;
        isOccupied = false;
        isWall = false;
    }
}
