using TMPro;
using UnityEngine;
using UnityEngine.Events;

class MenuElement : MonoBehaviour {
    public Reaction reaction;
    public UnityEvent unityEvent;
    public bool closeAfter = true;

    private bool selected = false;
    private TextMeshProUGUI text;

    private Color selectedColor   = new Color(1.0f, 1.0f, 0.4f);
    private Color unselectedColor = new Color(1.0f, 1.0f, 1.0f);

    private const float lerpSpeed = 2.5f;

    private void Start()
        => text = GetComponent<TextMeshProUGUI>();

    private void Update() {
        if (text != null)
            text.color = Color.Lerp(text.color, selected ? selectedColor : unselectedColor, lerpSpeed * Time.deltaTime);
    }

    private void OnMouseUpAsButton() {
        if (reaction != null) {
            reaction.React();
        }

        if (unityEvent != null) {
            unityEvent.Invoke();
        }

        if (closeAfter) {
            transform.parent.parent.gameObject.SetActive(false);
        }
    }

    private void OnMouseEnter()
        => selected = true;

    private void OnMouseExit()
        => selected = false;

    private void OnDisable() {
        if (text != null)
            text.color = unselectedColor;

        selected = false;
    }
}
