using UnityEngine;

public class LanguageSelectionMenu : MonoBehaviour {
    public void SelectRussian()
    {
        TextManager.Instance.SetLanguage(TextManager.Language.Russian);
        Initiate.Fade("Startup2", Color.black, 0.75f);
    }

    public void SelectEnglish()
    {
        TextManager.Instance.SetLanguage(TextManager.Language.English);
        Initiate.Fade("Startup2", Color.black, 0.75f);
    }
}
