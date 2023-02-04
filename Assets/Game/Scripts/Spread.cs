using UnityEngine;

public class Spread : MonoBehaviour {

    public Hexagon hexagon;
    public GameObject tree;

    private void OnEnable() {
        Invoke(nameof(SpreadFunc), GridManager.Instance.spreadTime);
    }

    void SpreadFunc() {
        if (hexagon.isWall)
            return;

        if (hexagon.isOccupied) {
            bool flag = true;

            for (int i = 0; i < 6; i++) {
                if (hexagon.borders[i] == null)
                    continue;

                if (!hexagon.borders[i].isOccupied) 
                    flag = false;

                if (!hexagon.borders[i].isOccupied) {
                    hexagon.borders[i].ChangeColour(hexagon.currentColor);
                    break;
                }
            }

            if (flag) {
                EatColour(Hexagon.Colors.Red, Hexagon.Colors.Green);
                EatColour(Hexagon.Colors.Green, Hexagon.Colors.Yellow);
                EatColour(Hexagon.Colors.Yellow, Hexagon.Colors.Red);
            }
        }

        float rand = Random.Range(-100, 100);
        rand /= 100;

        Invoke(nameof(SpreadFunc), GridManager.Instance.spreadTime + rand * 2);
    }

    void CheckForColor() {
        bool flag = true;
        if (hexagon.isOccupied) {
            for (int i = 0; i < 6; i++) {
                if (!hexagon.borders[i].isOccupied) {
                    flag = false;
                }
            }
        }

        if (flag) {
            EatColour(Hexagon.Colors.Red, Hexagon.Colors.Green);
            EatColour(Hexagon.Colors.Green, Hexagon.Colors.Yellow);
            EatColour(Hexagon.Colors.Yellow, Hexagon.Colors.Red);
        }
    }

    void EatColour(Hexagon.Colors eater, Hexagon.Colors eaten) {
        if (hexagon.currentColor == eater)
            for (int i = 0; i < 6; i++) {
                if (hexagon.borders[i] == null)
                    continue;

                if (hexagon.borders[i].currentColor == eaten)
                    TakeOver(i, eater);
            }
    }

    void TakeOver(int i, Hexagon.Colors color) {
        hexagon.borders[i].ChangeColour(color);
    }
}
