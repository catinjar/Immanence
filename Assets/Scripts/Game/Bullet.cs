using UnityEngine;

public class Bullet : MonoBehaviour {
    public float direction;
    public float speed = 50.0f;

    private void Awake() {
        Destroy(gameObject, 10.0f);
    }

    private void Update() {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
