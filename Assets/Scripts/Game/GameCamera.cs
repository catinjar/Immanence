using UnityEngine;

public class GameCamera : MonoBehaviour {
    public GameObject cameraObject;
    public float amplitude = 0.25f;
    public float speed = 5.0f;
    public bool adjustCameraOnStart = true;

    private const float lerpSpeed = 5.0f;

    private GameObject player;
    private Rigidbody2D body;

    private Vector2 destination = new Vector3();
    private Vector3 currentOffset = new Vector3();
    private Vector3 offset = new Vector3();

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();

        if (player != null) {
            destination = new Vector3(
                player.transform.position.x,
                transform.position.y,
                transform.position.z
            );
        }

        if (!adjustCameraOnStart) {
            return;
        }

        var collider = GetComponent<BoxCollider2D>();

        var bound1 = GameObject.Find("Bound");
        var bound2 = GameObject.Find("Bound (1)");

        var collider1 = bound1.GetComponent<BoxCollider2D>();
        var collider2 = bound2.GetComponent<BoxCollider2D>();

        float boundPos1 = bound1.transform.position.x + collider1.size.x / 2.0f;
        float boundPos2 = bound2.transform.position.x - collider2.size.x / 2.0f;

        if (boundPos1 > destination.x - collider.size.x / 2.0f) {
            destination.x = boundPos1 + collider.size.x / 2.0f;
        }

        if (boundPos2 < destination.x + collider.size.x / 2.0f) {
            destination.x = boundPos2 - collider.size.x / 2.0f;
        }

        transform.position = destination;
    }

    private void FixedUpdate() {
        currentOffset = Vector3.Lerp(currentOffset, offset, speed * Time.deltaTime);

        if ((currentOffset - offset).magnitude < 0.1f) {
            offset = new Vector3(
                Random.Range(-amplitude, amplitude),
                Random.Range(-amplitude, amplitude),
                0.0f
            );
        }

        if (player == null)
            return;

        var destination = transform.position;
        destination.x = player.transform.position.x;

        body.MovePosition(Vector2.Lerp(
            body.position,
            destination,
            lerpSpeed * Time.deltaTime
        ));

        var cameraPosition = body.position + (Vector2)currentOffset;
        cameraObject.transform.position = new Vector3(
            cameraPosition.x,
            cameraPosition.y,
            cameraObject.transform.position.z
        );
    }
}
