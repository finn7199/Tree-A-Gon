using System.Collections;
using UnityEngine;

public class Spread : MonoBehaviour
{
    public Hexagon hexagon;
    public GameObject tree;


    private void LateUpdate()
    {
        StartCoroutine(SpreadFunc());
    }

    IEnumerator SpreadFunc()
    {
        yield return new WaitForSeconds(5.0f);
        if (hexagon.isOccupied)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!hexagon.borders[i].isOccupied && !hexagon.borders[i].planted)
                {
                    Instantiate(tree, hexagon.borders[i].transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    hexagon.borders[i].planted = true;
                    hexagon.borders[i].isOccupied = true;
                }
            }
        }
    }
    
}
