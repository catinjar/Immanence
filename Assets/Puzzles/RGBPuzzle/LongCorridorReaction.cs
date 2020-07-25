using UnityEngine;

public class LongCorridorReaction : RGBPuzzleReaction {
    public SceneField cycleScene;
    public SceneField longCorridorScene;

    public override void React() {
        if (puzzle.stage == RGBPuzzle.Stage.s1GoCycle) {
            ++puzzle.stage;
            Initiate.Fade(cycleScene, Color.black, 0.75f);
        }

        if (puzzle.stage == RGBPuzzle.Stage.Complete) {
            Initiate.Fade(longCorridorScene, Color.black, 0.75f);
        }
    }
}
