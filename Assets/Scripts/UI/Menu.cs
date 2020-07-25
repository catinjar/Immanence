using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    [System.Serializable]
    public class MenuElementInfo {
        public string text;
        public Reaction reaction;
        public UnityEvent unityEvent;
    }

    public GameObject menuElementPrefab;

    public MenuElementInfo[] elements;

    private void Awake() {
        const float offsetY = 30.0f;
        const float stepY   = -7.0f;

        for (int i = 0; i < elements.Length; i++) {
            var menuElementObject = Instantiate(menuElementPrefab);

            var transform = menuElementObject.transform;

            transform.SetParent(gameObject.GetComponent<RectTransform>());
            transform.localPosition = new Vector3(0, offsetY + i * stepY, 0);
            transform.localScale    = new Vector3(1.0f, 1.0f, 1.0f);

            menuElementObject.GetComponent<TextMeshProUGUI>().text = TextManager.Instance.Say(elements[i].text)[0];

            var menuElement = menuElementObject.GetComponent<MenuElement>();

            menuElement.reaction   = elements[i].reaction;
            menuElement.unityEvent = elements[i].unityEvent;

            float preferredWidth = LayoutUtility.GetPreferredWidth(menuElementObject.GetComponent<RectTransform>());

            menuElementObject.GetComponent<BoxCollider2D>().size = new Vector2(preferredWidth, 6.0f);
        }
    }

    private void OnEnable()
        => PlayerState.Instance.LockMove();

    private void OnDisable() {
        if (!MessageManager.Instance.HasMessages) {
            PlayerState.Instance.Free();
        }
    }
}
