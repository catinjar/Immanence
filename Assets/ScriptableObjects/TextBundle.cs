using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TextManager;

public class Tweet {
    public string name;
    public string text;
    public int likes;
    public int reposts;
    public string sprite;
}

[CreateAssetMenu(fileName = "TextBundle", menuName = "TextBundle")]
[Serializable]
public class TextBundle : ScriptableObject {
    public class Message {
        public bool isYou = false;
        public string text = "";
    }

    public Language language = Language.Russian;

    public class Data {
        public Dictionary<string, Dictionary<string, List<string>>> itemText = new Dictionary<string, Dictionary<string, List<string>>>();
        public Dictionary<string, List<string>> messageText = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, List<Message>>> contactsText = new Dictionary<string, Dictionary<string, List<Message>>>();
        public List<Tweet> twitterText = new List<Tweet>();
    }

    public Data data = new Data();

    private void OnEnable() {
        Load();
    }

#if UNITY_EDITOR
    public void Save() {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(Path.ChangeExtension(AssetDatabase.GetAssetPath(this), "txt"), jsonData);
    }
#endif

    public void Load() {
        try {
            var textAsset = Resources.Load<TextAsset>($"Text/TextBundle{language}");
            var jsonData = textAsset.text;
            data = JsonConvert.DeserializeObject<Data>(jsonData);
        }
        catch (FileNotFoundException) {
            Debug.Log("Bundle file not found");
        }
    }

    public Dictionary<string, Dictionary<string, List<string>>> GetItemText()
        => data.itemText;

    public Dictionary<string, List<string>> GetMessageText()
        => data.messageText;

    public Dictionary<string, Dictionary<string, List<Message>>> GetContactsText()
        => data.contactsText;

    public List<Tweet> GetTwitterText()
        => data.twitterText;

    public string ItemName(string key)
        => data.itemText[SceneManager.GetActiveScene().name].ContainsKey(key) ? data.itemText[SceneManager.GetActiveScene().name][key][0] : "NULL";

    public List<string> AboutItem(string key)
        => data.itemText[SceneManager.GetActiveScene().name][key].Skip(1).ToList();

    public List<string> Say(string key)
        => data.messageText[key];

    public List<(bool isYou, string name)> Send(string name, string key)
        => data.contactsText[name][key].Select((m) => (m.isYou, m.text)).ToList();

    public List<Tweet> PostTweets(int from, int count)
        => data.twitterText.Skip(from).Take(count).ToList();
}
