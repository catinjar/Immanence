using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject menu;
    public GameObject settings;
    public GameObject continueButton;

    private bool changingScene = false;

    private void Awake() {
        continueButton.SetActive(GameSettings.lastSaveScene != null);
    }

    public void Continue() {
        if (changingScene)
            return;

        Initiate.Fade(GameSettings.lastSaveScene, Color.black, 0.75f);

        changingScene = true;
    }

    public void NewGame() {
        if (changingScene)
            return;

        GameSettings.Reset();
        Initiate.Fade("Intro1", Color.black, 0.75f);

        changingScene = true;
    }

    public void Exit() {
        Application.Quit();
    }

    public void Settings() {
        menu.SetActive(false);
        settings.SetActive(true);
    }
}
