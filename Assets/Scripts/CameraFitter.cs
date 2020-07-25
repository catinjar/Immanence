using UnityEngine;

[ExecuteInEditMode]
public class CameraFitter : MonoBehaviour {
    private Camera camera;

    private void Awake() {
        SetCameraSize();
    }

#if UNITY_EDITOR
    private void Update() {
        SetCameraSize();
    }
#endif

    private void SetCameraSize() {
        if (camera == null)
            camera = GetComponent<Camera>();
        camera.orthographicSize = Mathf.CeilToInt(160 * Screen.height / (Screen.width + 0.01f) * 0.5f);
    }
}
