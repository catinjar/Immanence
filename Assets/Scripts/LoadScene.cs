using Steamworks;
using System.Collections;
using UnityEngine;

public class LoadScene : MonoBehaviour {
    public SceneField scene;
    public float delay = 2.0f;
    public float damp = 0.75f;
    public bool goToLanguageSelection = false;

    private void Start() {
        if (!GameSettings.loaded)
            GameSettings.Load();
        StartCoroutine(LoadLogo());
    }

    private IEnumerator LoadLogo() {
        yield return new WaitForSeconds(delay);

        if (goToLanguageSelection && !GameSettings.selectedLanguage)
        {
            if (SteamManager.Initialized)
            {
                var steamUILanguage = SteamUtils.GetSteamUILanguage();
                
                switch (steamUILanguage)
                {
                    case "russian":
                        TextManager.Instance.SetLanguage(TextManager.Language.Russian);
                        break;

                    default:
                    case "english":
                        TextManager.Instance.SetLanguage(TextManager.Language.English);
                        break;
                }

                Initiate.Fade(scene, Color.black, damp);
            }
            else
            {
                Initiate.Fade("LanguageSelectionMenu", Color.black, damp);
            }
        }
        else
        {
            Initiate.Fade(scene, Color.black, damp);
        }
    }
}
