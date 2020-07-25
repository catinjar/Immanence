using UnityEngine;

public class ManDoorHasPhoneReaction : Reaction {
    public SceneField scene;
    public AudioSource sound;

    public override void React() {
        if (PlayerProgress.Instance.hasPhone) {
            sound.Play();
            Initiate.Fade(scene, Color.black, 0.75f);
        }
        else {
            MessageManager.Instance.PushMessages(TextManager.Instance.AboutItem("ManDoor"));
        }
    }
}
