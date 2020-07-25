using UnityEngine;
using UnityEngine.UI;

public class RGBApp : App {
    public RGBPuzzle puzzle;

    public Image red;
    public Image green;
    public Image blue;

    public Image background;

    public GameObject defaultItems;
    public GameObject redItems;
    public GameObject greenItems;
    public GameObject blueItems;

    public override void UpdateApp() {
        if (red == null || green == null || blue == null || background == null ||
            defaultItems == null || redItems == null || greenItems == null || blueItems == null) {
            return;
        }

        red.color   = new Color(1.0f, 0.0f, 0.0f, puzzle.appState == RGBPuzzle.AppState.Red   ? 1.0f : 0.5f);
        green.color = new Color(0.0f, 1.0f, 0.0f, puzzle.appState == RGBPuzzle.AppState.Green ? 1.0f : 0.5f);
        blue.color  = new Color(0.0f, 0.0f, 1.0f, puzzle.appState == RGBPuzzle.AppState.Blue  ? 1.0f : 0.5f);

        switch (puzzle.appState) {
            case RGBPuzzle.AppState.Red:
                background.color = new Color(1.0f, 0.0f, 0.0f, 0.05f);
                break;
            case RGBPuzzle.AppState.Green:
                background.color = new Color(0.0f, 1.0f, 0.0f, 0.05f);
                break;
            case RGBPuzzle.AppState.Blue:
                background.color = new Color(0.0f, 0.0f, 1.0f, 0.05f);
                break;
            default:
                background.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                break;
        }

        defaultItems.SetActive(puzzle.appState == RGBPuzzle.AppState.Default);
        redItems.SetActive(puzzle.appState == RGBPuzzle.AppState.Red);
        greenItems.SetActive(puzzle.appState == RGBPuzzle.AppState.Green);
        blueItems.SetActive(puzzle.appState == RGBPuzzle.AppState.Blue);
    }
}
