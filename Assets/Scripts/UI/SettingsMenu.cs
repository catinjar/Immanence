using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public Slider soundSlider;

    public Image russianLanguage;
    public Image englishLanguage;

    public GameObject mainMenu;

    private void Awake() {
        soundSlider.value = AudioListener.volume;

        UpdateLanguageColors();
    }

    public void Back() {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
        GameSettings.Save();
    }

    public void ChangeSoundVolume() {
        AudioListener.volume = soundSlider.value;
    }

    public void SetRussianLanguage() {
        TextManager.Instance.SetLanguage(TextManager.Language.Russian);
        UpdateLanguageColors();
    }

    public void SetEnglishLanguage() {
        TextManager.Instance.SetLanguage(TextManager.Language.English);
        UpdateLanguageColors();
    }

    public void UpdateLanguageColors() {
        russianLanguage.color = GameSettings.language == TextManager.Language.Russian ?
            new Color(1.0f, 1.0f, 1.0f, 1.0f) :
            new Color(1.0f, 1.0f, 1.0f, 0.6f);

        englishLanguage.color = GameSettings.language == TextManager.Language.Russian ?
            new Color(1.0f, 1.0f, 1.0f, 0.6f) :
            new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
