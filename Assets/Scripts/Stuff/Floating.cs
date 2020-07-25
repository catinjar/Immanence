using UnityEngine;

public class Floating : MonoBehaviour {
    public float randomHeightRange;
    public float randomSpeedRange;

    private bool goingUp;
    private float maxHeight;
    private float minHeight;
    private float speed;

    private void Start() {
        goingUp = Random.Range(0.0f, 100.0f) > 50.0f;

        maxHeight = Random.Range(randomHeightRange / 2.0f, randomHeightRange)   - 5.0f;
        minHeight = Random.Range(-randomHeightRange, -randomHeightRange / 2.0f) - 5.0f;

        speed = Random.Range(randomSpeedRange / 2.0f, randomSpeedRange);

        var position = transform.position;
        position.y = Random.Range(minHeight, maxHeight);
        transform.position = position;
    }

    private void Update() {
        float direction = goingUp ? 1.0f : -1.0f;

        var position = transform.position;

        position.y += direction * speed * Time.deltaTime;

        if (goingUp && position.y > maxHeight) {
            goingUp = false;
            position.y = maxHeight;
        }

        if (!goingUp && position.y < minHeight) {
            goingUp = true;
            position.y = minHeight;
        }

        transform.position = position;
    }
}
