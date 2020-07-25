using UnityEditor;
using UnityEngine;
using TMPro;

[System.Serializable]
public class CutsceneStage {
    public enum Type {
        Delay = 0,
        ChangeScene = 1,
        TextFade = 2,
        Say = 3,
        OpenPhone = 4,
        Send = 5,
        Flip = 6,
        Action = 7
    }

    [SerializeField] private Type type = Type.Delay;

    public CutsceneStage(Type type) {
        this.type = type;
    }

    public bool Play() {
        switch (type) {
            case Type.Delay:
                return Delay();
            case Type.ChangeScene:
                return ChangeScene();
            case Type.TextFade:
                return TextFade();
            case Type.Say:
                return Say();
            case Type.OpenPhone:
                return OpenPhone();
            case Type.Send:
                return Send();
            case Type.Flip:
                return Flip();
            case Type.Action:
                return Reaction();
            default:
                return false;
        }
    }

#if UNITY_EDITOR
    public void GUI() {
        switch (type) {
            case Type.Delay:
                DelayGUI();
                break;
            case Type.ChangeScene:
                ChangeSceneGUI();
                break;
            case Type.TextFade:
                TextFadeGUI();
                break;
            case Type.Say:
                SayGUI();
                break;
            case Type.OpenPhone:
                break;
            case Type.Send:
                SendGUI();
                break;
            case Type.Flip:
                FlipGUI();
                break;
            case Type.Action:
                ReactionGUI();
                break;
        }
    }
#endif

    public int GetStageType() => (int)type;

    // Delay
    [SerializeField] private float duration = 1.0f;
    private float currentTime = 0.0f;

    private bool Delay() {
        currentTime += Time.deltaTime;
        if (currentTime > duration) {
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    private void DelayGUI() {
        duration = EditorGUILayout.FloatField("Duration", duration);
    }
#endif

    // ChangeScene
    [SerializeField] private string sceneName;
    [SerializeField] private float changeSceneDuration = 0.75f;

    private bool ChangeScene() {
        Initiate.Fade(sceneName, Color.black, changeSceneDuration);
        return true;
    }

#if UNITY_EDITOR
    private void ChangeSceneGUI() {
        sceneName = EditorGUILayout.TextField("Scene Name", sceneName);
        changeSceneDuration = EditorGUILayout.FloatField("Change Duration", changeSceneDuration);
    }
#endif

    // TextFade
    private static readonly string[] fadeOptions = { "In", "Out" };
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int fadeType = 0;
    [SerializeField] private float fadeSpeed = 1.0f;

    private bool TextFade() {
        float alpha = text.color.a;
        alpha = Mathf.Lerp(alpha, fadeType == 0 ? 1.1f : -0.1f, fadeSpeed * Time.deltaTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

        if (fadeType == 0 && alpha > 1.0f || fadeType == 1 && alpha < 0.0f) {
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    private void TextFadeGUI() {
        text = EditorGUILayout.ObjectField("Text", text, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
        fadeType = EditorGUILayout.Popup(fadeType, fadeOptions);
        fadeSpeed = EditorGUILayout.FloatField("Fade Speed", fadeSpeed);
    }
#endif

    // Say
    [SerializeField] private string key;

    private bool Say() {
        MessageManager.Instance.PushMessages(TextManager.Instance.Say(key));
        return true;
    }

#if UNITY_EDITOR
    private void SayGUI() {
        key = EditorGUILayout.TextField("Key", key);
    }
#endif

    // OpenPhone
    private bool OpenPhone() {
        var phone = GameObject.FindObjectOfType<Phone>();
        phone.Open();
        phone.GetComponent<AppManager>().Change("HomeApp");
        return true;
    }

    // Send
    [SerializeField] private string contactName;
    [SerializeField] private string messagePack;

    private bool Send() {
        ContactsManager.Instance.Send(contactName, messagePack);
        return true;
    }

#if UNITY_EDITOR
    private void SendGUI() {
        contactName = EditorGUILayout.TextField("Contact Name", contactName);
        messagePack = EditorGUILayout.TextField("Message Pack Name", messagePack);
    }
#endif

    // Flip
    [SerializeField] private GameObject obj;

    private bool Flip() {
        obj.SetActive(!obj.activeSelf);
        return true;
    }

#if UNITY_EDITOR
    private void FlipGUI() {
        obj = EditorGUILayout.ObjectField("Object", obj, typeof(GameObject), true) as GameObject;
    }
#endif

    // Reaction
    [SerializeField] private Reaction reaction;

    private bool Reaction() {
        reaction.React();
        return true;
    }

#if UNITY_EDITOR
    private void ReactionGUI() {
        reaction = EditorGUILayout.ObjectField("Reaction", reaction, typeof(Reaction), true) as Reaction;
    }
#endif
}
