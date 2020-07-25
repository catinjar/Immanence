using UnityEngine;

public class NyctophiliaPuzzleRestore : MonoBehaviour {
    public NyctophiliaPuzzle puzzle;

    private void Start() {
        puzzle.RestoreState();
    }
}
