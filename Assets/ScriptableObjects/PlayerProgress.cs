using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgress", menuName = "Gameplay/PlayerProgress")]
public class PlayerProgress : SingletonScriptableObject<PlayerProgress> {
    public bool gotToHome = false;
    public bool gotToBathroomWithShit = false;
    public bool hasPhone = false;
    public bool gotToManFlat = false;
    public bool hasRGBApp = false;
    public bool showedRGBApp = false;
    public bool redEye = false;
    public bool hasLampApp = false;
    public bool hasWeapon = false;
    public bool openedLastSilentHillDoor = false;

    public int flappyHighscore = 0;

    public void GetPhone() {
        hasPhone = true;
    }

    public void GetWeapon() {
        hasWeapon = true;
    }

    public void GetRedEye() {
        redEye = true;
    }

    public void GetRGBApp() {
        hasRGBApp = true;
    }

    public void GetLampApp() {
        hasLampApp = true;
    }

    public void GetToHome() {
        GetSomething(ref gotToHome, "GotToHome");
    }

    public void GetToBathroomWithShit() {
        GetSomething(ref gotToBathroomWithShit, "GotToHomeBathroom");
    }

    public void GetToManFlat() {
        if (!gotToManFlat) {
            ContactsManager.Instance.Send("StrangeMan", "GotToManFlat");
            gotToManFlat = true;
        }
    }

    public void ShowRGBApp() {
        if (hasRGBApp && !showedRGBApp) {
            PhoneState.Instance.opened = true;

            var appManager = FindObjectOfType<AppManager>();
            appManager.Change("HomeApp");
            
            showedRGBApp = true;
        }
    }

    public void OpenLastSilentHillDoor() {
        openedLastSilentHillDoor = true;
    }

    private void GetSomething(ref bool condition, string key) {
        if (!condition) {
            MessageManager.Instance.PushMessages(TextManager.Instance.Say(key));
            condition = true;
        }
    }

    public void Save() {
        PlayerPrefs.SetInt("gotToHome", gotToHome ? 1 : 0);
        PlayerPrefs.SetInt("gotToBathroomWithShit", gotToBathroomWithShit ? 1 : 0);
        PlayerPrefs.SetInt("hasPhone", hasPhone ? 1 : 0);
        PlayerPrefs.SetInt("gotToManFlat", gotToManFlat ? 1 : 0);
        PlayerPrefs.SetInt("hasRGBApp", hasRGBApp ? 1 : 0);
        PlayerPrefs.SetInt("showedRGBApp", showedRGBApp ? 1 : 0);
        PlayerPrefs.SetInt("redEye", redEye ? 1 : 0);
        PlayerPrefs.SetInt("hasLampApp", hasLampApp ? 1 : 0);
        PlayerPrefs.SetInt("hasWeapon", hasWeapon ? 1 : 0);
        PlayerPrefs.SetInt("openedLastSilentHillDoor", openedLastSilentHillDoor ? 1 : 0);
        PlayerPrefs.SetInt("flappyHighscore", flappyHighscore);
    }

    public void Load() {
        gotToHome                = PlayerPrefs.GetInt("gotToHome", 0) == 1 ? true : false;
        gotToBathroomWithShit    = PlayerPrefs.GetInt("gotToBathroomWithShit", 0) == 1 ? true : false;
        hasPhone                 = PlayerPrefs.GetInt("hasPhone", 0) == 1 ? true : false;
        gotToManFlat             = PlayerPrefs.GetInt("gotToManFlat", 0) == 1 ? true : false;
        hasRGBApp                = PlayerPrefs.GetInt("hasRGBApp", 0) == 1 ? true : false;
        showedRGBApp             = PlayerPrefs.GetInt("showedRGBApp", 0) == 1 ? true : false;
        redEye                   = PlayerPrefs.GetInt("redEye", 0) == 1 ? true : false;
        hasLampApp               = PlayerPrefs.GetInt("hasLampApp", 0) == 1 ? true : false;
        hasWeapon                = PlayerPrefs.GetInt("hasWeapon", 0) == 1 ? true : false;
        openedLastSilentHillDoor = PlayerPrefs.GetInt("openedLastSilentHillDoor", 0) == 1 ? true : false;
        flappyHighscore          = PlayerPrefs.GetInt("flappyHighscore", 0);
    }

    public void Reset() {
        gotToHome = false;
        gotToBathroomWithShit = false;
        hasPhone = false;
        gotToManFlat = false;
        hasRGBApp = false;
        showedRGBApp = false;
        redEye = false;
        hasLampApp = false;
        hasWeapon = false;
        openedLastSilentHillDoor = false;
    }
}
