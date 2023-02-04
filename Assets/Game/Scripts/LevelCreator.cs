using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour
{
    float timeSinceRatio = 0;

    [SerializeField] float timeTakenToWin = 5f;

    [Header("Tree Positions")]
    [SerializeField] Vector2 redPos;
    [SerializeField] Vector2 greenPos;
    [SerializeField] Vector2 yellowPos;

    [Header("Target Ratios")]
    [SerializeField] float redRatio = 0.5f;
    [SerializeField] float greenRatio = 0.25f;
    [SerializeField] float yellowRatio = 0.25f;

    [Header("Assignables")]
    [SerializeField] GameObject winScreen;

    [SerializeField] TMP_Text redText;
    [SerializeField] TMP_Text greenText;
    [SerializeField] TMP_Text yellowText;

    [SerializeField] TMP_Text redTextTarget;
    [SerializeField] TMP_Text greenTextTarget;
    [SerializeField] TMP_Text yellowTextTarget;

    [SerializeField] Slider winSlider;

    private void Start() {
        winSlider.minValue = 0;
        winSlider.maxValue = timeTakenToWin;

        redTextTarget.text = "Red: " + (int)(redRatio * 100) + "%";
        greenTextTarget.text = "Green: " + (int)(greenRatio * 100) + "%";
        yellowTextTarget.text = "Yellow: " + (int)(yellowRatio * 100) + "%";
    }
    
    private void Update() {
        CheckIfWinning();
    }

    public void SpawnTrees() {
        GetTile(redPos)?.ChangeColour(Hexagon.Colors.Red);
        GetTile(greenPos)?.ChangeColour(Hexagon.Colors.Green);
        GetTile(yellowPos)?.ChangeColour(Hexagon.Colors.Yellow);
    }
    
    Hexagon GetTile(Vector2 vec) {
        return GridManager.Instance.GetTileByIndex(vec);
    }

    void CheckIfWinning() {
        float error = 0.1f;

        // Gets percentage of trees
        float rPercent = (float)GridManager.Instance.redCount / GridManager.Instance.totalCount;
        float gPercent = (float)GridManager.Instance.greenCount / GridManager.Instance.totalCount;
        float yPercent = (float)GridManager.Instance.yellowCount / GridManager.Instance.totalCount;

        // sets text
        redText.text = "Red: " + (int)(rPercent * 100) + "%";
        greenText.text = "Green: " + (int)(gPercent * 100) + "%";
        yellowText.text = "Yellow: " + (int)(yPercent * 100) + "%";

        // removes text if tree does not exist
        if (rPercent == 0) redText.text = "";
        if (gPercent == 0) greenText.text = "";
        if (yPercent == 0) yellowText.text = "";

        // Checks if trees are within ratio
        bool r = rPercent < redRatio + error && rPercent > redRatio - error;
        bool g = gPercent < greenRatio + error && rPercent > greenRatio - error;
        bool y = yPercent < yellowRatio + error && rPercent > yellowRatio - error;

        if (r && g && y) {
            float val = Time.time - timeSinceRatio;
            winSlider.value = val;
            if (val > timeTakenToWin)
                WinGame();
        }
        else {
            winSlider.value = 0;
            timeSinceRatio = Time.time;
        }
    }

    void WinGame() {
        winScreen.SetActive(true);
        Invoke(nameof(GoToNextScene), 10);
    }

    void GoToNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
