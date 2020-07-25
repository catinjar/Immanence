using UnityEngine;

public class FlickeringLight : MonoBehaviour {
    public float minDelay = 0.1f;
    public float maxDelay = 0.5f;

    private Light l;

    private float currentTime = 0.0f;
    private float maxTime;

    private void Start() {
        l = GetComponent<Light>();

        maxTime = Random.Range(minDelay, maxDelay);
    }

    private void Update() {
        currentTime += Time.deltaTime;

        if (currentTime > maxTime) {
            l.enabled   = !l.enabled;
            currentTime = 0.0f;
            maxTime     = Random.Range(minDelay, maxDelay);
        }
    }
}
