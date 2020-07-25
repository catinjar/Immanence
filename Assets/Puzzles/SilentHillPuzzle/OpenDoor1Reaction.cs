using UnityEngine;

public class OpenDoor1Reaction : Reaction {
    public SceneField scene;
    public AudioSource sound;

    public override void React() {
        if (PlayerProgress.Instance.hasWeapon) {
            sound.Play();
            Initiate.Fade(scene, Color.black, 0.75f);
        }
        else {
            MessageManager.Instance.PushMessages("NeedWeapon");
        }
    }
}
