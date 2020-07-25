using UnityEngine;

public class OpenDoor2Reaction : Reaction {
    public SceneField scene;
    public AudioSource sound;

    public override void React() {
        if (PlayerProgress.Instance.openedLastSilentHillDoor) {
            sound.Play();
            Initiate.Fade(scene, Color.black, 0.75f);
        }
        else {
            MessageManager.Instance.PushMessages("NeedToOpenDoor");
        }
    }
}