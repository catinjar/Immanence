using UnityEngine;
using static TextManager;

public static class GameSettings {
    public static Language language = Language.English;
    public static bool selectedLanguage = false;
    public static string lastSaveScene = null;

    public static bool loaded = false;

    public static void Save() {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        PlayerPrefs.SetInt("language", (int)language);
        PlayerPrefs.SetInt("selectedLanguage", selectedLanguage ? 1 : 0);
        PlayerPrefs.SetString("lastSaveScene", lastSaveScene);

        PlayerProgress.Instance.Save();
        PhoneState.Instance.Save();
        ContactsManager.Instance.Save();
        TwitterManager.Instance.Save();

        PlayerPrefs.Save();
    }

    public static void Load() {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1.0f);
        language = (Language)PlayerPrefs.GetInt("language", 0);
        selectedLanguage = PlayerPrefs.GetInt("selectedLanguage", 0) == 1;
        lastSaveScene = PlayerPrefs.GetString("lastSaveScene", null);

        PlayerProgress.Instance.Load();
        PhoneState.Instance.Load();
        ContactsManager.Instance.Load();
        TwitterManager.Instance.Load();

        TextManager.Instance.UpdateLanguage();

        loaded = true;
    }

    public static void Reset() {
        language = Language.English;
        lastSaveScene = null;

        PlayerProgress.Instance.Reset();
        PhoneState.Instance.Reset();
        ContactsManager.Instance.Reset();
        TwitterManager.Instance.Reset();

        Save();
    }
}
