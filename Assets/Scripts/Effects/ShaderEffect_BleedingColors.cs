using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour {
	public float intensity = 3;
	public float shift = 0.5f;

    private float currentShift = 0.0f;
    private float nextShift = 0.0f;

	private Material material;

	private void Awake () {
		material = new Material(Shader.Find("Hidden/BleedingColors") );
	}

    private void Update() {
        currentShift = Mathf.Lerp(currentShift, nextShift, 50.0f * Time.deltaTime);

        if (Mathf.Abs(nextShift - currentShift) < 0.01f) {
            nextShift = Random.Range(0.0f, shift);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ValueX", currentShift);

		Graphics.Blit(source, destination, material);
	}
}
