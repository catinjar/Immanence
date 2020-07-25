using UnityEngine;

public class TremblingLight : MonoBehaviour {
    public float amplitude = 1.0f;
    public float speed = 1.0f;

    private Light l;
    private float baseIntensity;
    private float currentIntensity;

    private void Start() {
        l = GetComponent<Light>();

        baseIntensity    = l.intensity;
        currentIntensity = baseIntensity;
    }

    private void Update() {
        l.intensity = Mathf.Lerp(l.intensity, currentIntensity, speed * Time.deltaTime);

        if (Mathf.Abs(l.intensity - currentIntensity) < 0.001f) {
            currentIntensity = Random.Range(baseIntensity - amplitude, baseIntensity + amplitude);
        }
    }
}
