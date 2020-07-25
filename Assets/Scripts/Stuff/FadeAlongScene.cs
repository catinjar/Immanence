using UnityEngine;
using UnityEngine.UI;

public class FadeAlongScene : MonoBehaviour {
    public Transform bound1;
    public Transform bound2;
    public Transform player;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    private void Update() {
        float fraction = (player.position.x - bound1.position.x) / (bound2.position.x - bound1.position.x);
        image.color = new Color(0.0f, 0.0f, 0.0f, fraction);
    }
}
