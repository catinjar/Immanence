using UnityEngine;

public class CasseteReaction : Reaction {
    public VideoEditorPuzzle puzzle;
    public SceneField editorScene;

    public override void React() {
        if (puzzle.stage == VideoEditorPuzzle.Stage.GetClip1) {
            puzzle.GetClip("clip1");
        }
        else if (puzzle.stage == VideoEditorPuzzle.Stage.GetClip2) {
            puzzle.GetClip("clip2");
        }

        Initiate.Fade(editorScene, Color.black, 0.75f);
    }
}