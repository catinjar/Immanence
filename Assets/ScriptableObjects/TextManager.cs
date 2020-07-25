using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextManager", menuName = "Gameplay/TextManager")]
public class TextManager : SingletonScriptableObject<TextManager> {
    public TextBundle russianBundle;
    public TextBundle englishBundle;

    public enum Language {
        English,
        Russian
    }

    private TextBundle currentBundle;

    private void OnEnable() {
        UpdateLanguage();
    }

    public void UpdateLanguage() {
        currentBundle = GameSettings.language == Language.Russian ? russianBundle : englishBundle;
    }

    public void SetLanguage(Language language) {
        GameSettings.language = language;
        GameSettings.selectedLanguage = true;
        GameSettings.Save();
        UpdateLanguage();
    }

    public string ItemName(string key)
        => currentBundle.ItemName(key);

    public List<string> AboutItem(string key)
        => currentBundle.AboutItem(key);

    public List<string> Say(string key)
        => currentBundle.Say(key);

    public List<(bool isYou, string name)> Send(string name, string key)
        => currentBundle.Send(name, key);

    public List<Tweet> PostTweets(int from, int count)
        => currentBundle.PostTweets(from, count);

    public string GetText(string key)
        => Say(key)[0];
}
