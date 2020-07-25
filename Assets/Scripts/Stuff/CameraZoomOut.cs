using UnityEngine;

public class CameraZoomOut : MonoBehaviour {
    public new Camera camera;
    public float speed = 5.0f;

    private void Update() {
        camera.orthographicSize += speed * Time.deltaTime;
    }
}
