using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_CRT : MonoBehaviour {
	public float scanlineIntensity = 100.0f;
	public int scanlineWidth = 1;

	private Material materialScanlines;

	private void Awake () {
		materialScanlines = new Material(Shader.Find("Hidden/Scanlines"));
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
		materialScanlines.SetFloat("_Intensity", scanlineIntensity * 0.01f);
		materialScanlines.SetFloat("_ValueX", scanlineWidth);

		Graphics.Blit(source, destination, materialScanlines);
	}
}
