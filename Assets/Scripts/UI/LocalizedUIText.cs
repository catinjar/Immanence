using TMPro;
using UnityEngine;

public class LocalizedUIText : MonoBehaviour {
    public string key;

    private TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        text.text = TextManager.Instance.GetText(key);
    }
}
