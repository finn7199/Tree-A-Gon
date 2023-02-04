using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int redX;
    public int redY;

    public int greenX;
    public int greenY;

    public int yellowX;
    public int yellowY;

    public void SpawnTrees() {
        GetTile(redX, redY).ChangeColour(Hexagon.Colors.Red);
        GetTile(greenX, greenY).ChangeColour(Hexagon.Colors.Green);
        GetTile(yellowX, yellowY).ChangeColour(Hexagon.Colors.Yellow);
    }
    
    Hexagon GetTile(float x, float y) {
        return GridManager.Instance.GetTileByIndex(new Vector2(x, y));
    }
}
