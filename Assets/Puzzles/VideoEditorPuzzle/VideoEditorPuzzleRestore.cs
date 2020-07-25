using UnityEngine;

public class VideoEditorPuzzleRestore : MonoBehaviour {
    public VideoEditorPuzzle puzzle;

	void Start () {
        puzzle.RestoreState();
	}
}
