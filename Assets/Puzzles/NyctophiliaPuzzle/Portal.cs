using UnityEngine;

public class Portal : MonoBehaviour {
    public NyctophiliaPuzzle puzzle;
    public SceneField scene;

    private Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        var distance = transform.position - player.transform.position;
        float length = distance.magnitude;

        if (length < 30.0f) {
            puzzle.currentPortal = this;
        }
        else {
            puzzle.currentPortal = null;
        }
    }
}