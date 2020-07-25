using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/RGBPuzzle", order = 1)]
public class RGBPuzzle : ScriptableObject {
    public enum AppState {
        Default,
        Red,
        Green,
        Blue
    }

    public AppState appState;

    public void SetRed() {
        if (appState == AppState.Red) {
            appState = AppState.Default;
        }
        else {
            appState = AppState.Red;
        }
    }

    public void SetGreen() {
        if (appState == AppState.Green) {
            appState = AppState.Default;
        }
        else {
            appState = AppState.Green;
        }
    }

    public void SetBlue() {
        if (appState == AppState.Blue) {
            appState = AppState.Default;
        }
        else {
            appState = AppState.Blue;
        }
    }

    public enum Stage {
        s1GoCycle, // Don't have RGB app access
        s2TalkFlower,
        s3FindFlower,
        s4FindFlesh,
        s5FindKnife,
        s6FindDrugs,
        s7GetPot,
        s8WashPot,
        s9GetFlower,
        s10GiveFlower,
        Complete
    }

    public Stage stage;

    public RGBPuzzle() {
        appState = AppState.Default;
        stage = Stage.s1GoCycle;
    }
}
