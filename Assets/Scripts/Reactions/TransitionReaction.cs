using UnityEngine;

public class TransitionReaction : Reaction {
    public SceneField scene;
    public float speed = 0.75f;
    public AudioSource sound;

    public override void React() {
        if (scene == null) {
            return;
        }

        if (sound != null) {
            sound.Play();
        }

        Initiate.Fade(scene, Color.black, speed);
    }
}