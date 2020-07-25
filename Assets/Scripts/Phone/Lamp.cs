using UnityEngine;

public class Lamp : MonoBehaviour {
    public GameObject lamp;

    private const float lerpSpeed = 10.0f;

    private Vector3 hiddenPosition         = new Vector3(60.0f, -55.0f, 0.0f);
    private Vector3 hiddenSelectedPosition = new Vector3(60.0f, -40.0f, 0.0f);

    private Vector3 currentPosition;

    private void Update() {
        if (MessageManager.Instance.HasMessages) {
            return;
        }

        if (Input.mousePosition.y < 80.0f) {
            currentPosition = hiddenSelectedPosition;
        }
        else {
            currentPosition = hiddenPosition;
        }

        lamp.transform.localPosition = Vector3.Lerp(
            lamp.transform.localPosition,
            currentPosition,
            lerpSpeed * Time.deltaTime
        );
    }
}
