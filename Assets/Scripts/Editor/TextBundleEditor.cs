using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TextManager;

[CustomEditor(typeof(TextBundle))]
public class TextBundleEditor : Editor {
    private class TextOptionsHandle {
        public int index = 0;
        public string name = "";
    }

    private static readonly string[] options = { "Items", "Messages", "Contacts", "Twitter" };
    private int editorIndex = 0;

    private TextOptionsHandle messageHandle        = new TextOptionsHandle();
    private TextOptionsHandle contactsHandle       = new TextOptionsHandle();
    private TextOptionsHandle contactMessageHandle = new TextOptionsHandle();

    public override void OnInspectorGUI() {
        EditorStyles.textField.wordWrap = true;

        editorIndex = EditorGUILayout.Popup(editorIndex, options);

        var bundle = target as TextBundle;

        var language = bundle.language;
        bundle.language = (Language)EditorGUILayout.Popup((int)bundle.language, new string[] { "English", "Russian" });
        if (language != bundle.language)
            EditorUtility.SetDirty(target);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save")) {
            bundle.Save();
        }

        if (GUILayout.Button("Load")) {
            bundle.Load();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        switch (editorIndex) {
            case 0:
                ItemTextGUI(bundle);
                break;
            case 1:
                MessageTextGUI(bundle);
                break;
            case 2:
                ContactsTextGUI(bundle);
                break;
            case 3:
                TwitterTextGUI(bundle);
                break;
        }
    }

    private void ItemTextGUI(TextBundle bundle) {
        var itemText = bundle.GetItemText();

        string sceneName = SceneManager.GetActiveScene().name;

        if (!itemText.ContainsKey(sceneName)) {
            itemText[sceneName] = new Dictionary<string, List<string>>();
        }

        if (Selection.gameObjects.Length != 1 ||
            Selection.gameObjects[0].GetComponent<Item>() == null) {

            EditorGUILayout.LabelField("No item selected", EditorStyles.boldLabel);
            return;
        }

        var item = Selection.gameObjects[0];
        EditorGUILayout.LabelField(item.name, EditorStyles.boldLabel);

        if (!itemText[sceneName].ContainsKey(item.name)) {
            itemText[sceneName][item.name] = new List<string>();
        }

        UpdateMessages(itemText[sceneName][item.name], () => "", (messages, index) => {
            messages[index] = EditorGUILayout.TextArea(messages[index], GUILayout.Height(50.0f));
        });
    }

    private void MessageTextGUI(TextBundle bundle) {
        var messageText = bundle.GetMessageText();

        string messageName = UpdateTextDictionary(messageText, messageHandle);
        AddDeleteWidget(messageText, messageHandle);

        if (!messageText.ContainsKey(messageName)) {
            messageText[messageName] = new List<string>();
        }

        UpdateMessages(messageText[messageName], () => "", (messages, index) => {
            messages[index] = EditorGUILayout.TextArea(messages[index], GUILayout.Height(50.0f));
        });
    }

    private void ContactsTextGUI(TextBundle bundle) {
        var contactsText = bundle.GetContactsText();

        EditorGUILayout.LabelField("Contact", EditorStyles.boldLabel);

        string contactName = UpdateTextDictionary(contactsText, contactsHandle);
        AddDeleteWidget(contactsText, contactsHandle);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Contact Message Pack", EditorStyles.boldLabel);

        string contactMessageName = UpdateTextDictionary(contactsText[contactName], contactMessageHandle);

        if (contactMessageName == "") {
            return;
        }

        AddDeleteWidget(contactsText[contactName], contactMessageHandle);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Messages", EditorStyles.boldLabel);

        if (!contactsText[contactName].ContainsKey(contactMessageName)) {
            contactsText[contactName][contactMessageName] = new List<TextBundle.Message>();
        }

        UpdateMessages(contactsText[contactName][contactMessageName], () => new TextBundle.Message(), (messages, index) => {
            messages[index].isYou = GUILayout.Toggle(messages[index].isYou, "Is You");
            messages[index].text  = EditorGUILayout.TextArea(messages[index].text, GUILayout.Height(50.0f));
        });
    }

    private void TwitterTextGUI(TextBundle bundle) {
        var twitterText = bundle.GetTwitterText();

        UpdateMessages(twitterText, () => new Tweet(), (tweets, index) => {
            tweets[index].name    = EditorGUILayout.TextField(tweets[index].name);
            tweets[index].text    = EditorGUILayout.TextArea(tweets[index].text, GUILayout.Height(50.0f));
            tweets[index].likes   = EditorGUILayout.IntField(tweets[index].likes);
            tweets[index].reposts = EditorGUILayout.IntField(tweets[index].reposts);
            tweets[index].sprite  = EditorGUILayout.TextField(tweets[index].sprite);
        });
    }

    // Returns current selected option
    private string UpdateTextDictionary<T>(Dictionary<string, T> dictionary, TextOptionsHandle handle)
        where T : new() {

        handle.name = EditorGUILayout.TextField(handle.name);

        if (dictionary.Count != 0) {
            var options = dictionary.Keys.ToArray();
            handle.index = EditorGUILayout.Popup(handle.index, options);
            return options[handle.index];
        }
        else {
            if (GUILayout.Button("Start")) {
                dictionary.Add(handle.name, new T());
            }
            return "";
        }
    }

    private void AddDeleteWidget<T>(Dictionary<string, T> dictionary, TextOptionsHandle handle)
        where T : new() {

        var options = dictionary.Keys.ToArray();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add")) {
            if (!dictionary.ContainsKey(handle.name)) {
                dictionary.Add(handle.name, new T());
            }
        }

        if (GUILayout.Button("Delete")) {
            dictionary.Remove(options[handle.index]);
        }

        EditorGUILayout.EndHorizontal();
    }

    private void UpdateMessages<T>(List<T> messages, Func<T> instantiate, Action<List<T>, int> specificGUI) {
        if (messages.Count == 0) {
            if (GUILayout.Button("Start")) {
                messages.Add(instantiate());
            }
            return;
        }

        for (int i = 0; i < messages.Count; ++i) {
            EditorGUILayout.LabelField(i.ToString(), EditorStyles.boldLabel, GUILayout.Width(25.0f));

            specificGUI(messages, i);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add", GUILayout.Width(50.0f))) {
                messages.Insert(i + 1, instantiate());
            }

            if (GUILayout.Button("Delete", GUILayout.Width(50.0f))) {
                messages.Remove(messages[i]);
            }

            EditorGUILayout.EndHorizontal();
        }
    } 
}
