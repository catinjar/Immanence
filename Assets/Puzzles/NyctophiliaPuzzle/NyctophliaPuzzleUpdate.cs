using UnityEngine;

public class NyctophliaPuzzleUpdate : MonoBehaviour {
    public NyctophiliaPuzzle puzzle;

    private void Start() {
        puzzle.SceneStart();
    }

    private void Update() {
        puzzle.Update();
    }
}
