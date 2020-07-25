using UnityEngine;

public class RoguelikeCamera : MonoBehaviour {
    private GameObject player = null;

    private void Start() {
        player = FindObjectOfType<RoguelikePlayer>().gameObject;
    }

    private void FixedUpdate() {
        if (player == null) {
            return;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                -65.0f
            ),
            15.0f * Time.deltaTime
        );
    }
}
