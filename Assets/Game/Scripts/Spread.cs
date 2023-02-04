using UnityEngine;

public class Spread : MonoBehaviour {
    public Hexagon hexagon;
    public GameObject tree;

    private void Start() {
        Invoke(nameof(SpreadFunc), 5);
    }

    void SpreadFunc() {
        if (hexagon.isOccupied) {
            for (int i = 0; i < 6; i++) {
                if (!hexagon.borders[i].isOccupied && !hexagon.borders[i].planted) {
                    Instantiate(tree, hexagon.borders[i].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    hexagon.borders[i].planted = true;
                    hexagon.borders[i].isOccupied = true;
                    hexagon.borders[i].GetComponent<Spread>().enabled = true;

                    break; 
                }
            }
        }

        float rand = Random.Range(-100, 100);
        rand /= 100;

        Invoke(nameof(SpreadFunc), 5 + rand * 2);
    }
}
