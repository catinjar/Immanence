using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
    public UnityEvent unityEvent = null;

    private bool triggered = false;
    private GameObject player;
    private Reaction[] reactions;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        reactions = GetComponents<Reaction>();
    }

    private void Update() {
        if (transform.position.x - player.transform.position.x < 10.0f) {
            if (unityEvent != null) {
                unityEvent.Invoke();
            }

            if (reactions.Length > 0) {
                foreach (var r in reactions) {
                    r.React();
                }
            }

            triggered = true;
        }

        if (triggered) {
            gameObject.SetActive(false);
        }
    }
}
