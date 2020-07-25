using UnityEngine;

public class HomeApp : App {
    public GameObject rgbApp;
    public GameObject lampApp;

    public override void UpdateApp() {
        rgbApp.SetActive(PlayerProgress.Instance.hasRGBApp);
        lampApp.SetActive(PlayerProgress.Instance.hasLampApp);
    }
}
