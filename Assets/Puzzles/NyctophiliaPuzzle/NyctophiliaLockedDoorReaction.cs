using UnityEngine;

public class NyctophiliaLockedDoorReaction : Reaction {
    public NyctophiliaPuzzle puzzle;
    public SceneField scene;

    public override void React() {
        if (puzzle.hasKey) {
            Initiate.Fade(scene, Color.black, 0.75f);
        }
        else {
            MessageManager.Instance.PushMessages("Locked2");
        }
    }
}
