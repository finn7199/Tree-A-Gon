using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceWalls : MonoBehaviour
{

    public static PlaceWalls instance;

    public Hexagon firstWall = null;

    public int cash = 1000;

    [SerializeField] int maxCash = 2000;

    [SerializeField] int cashReduceAmount = 250;
    [SerializeField] int cashIncreaseAmount = 250;

    [SerializeField] TMP_Text cashText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cashText.text = cash.ToString();
    }

    public void ReduceCash() {
        cash -= cashReduceAmount;

        if (cash < 0)
            cash = 0;

        cashText.text = cash.ToString();
    }

    public void IncreaseCash() {
        cash += cashIncreaseAmount;

        if (cash > maxCash)
            cash = maxCash;

        cashText.text = cash.ToString();
    }
}
