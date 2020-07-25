using System;
using UnityEngine;

public class AppManager : MonoBehaviour {
    public App currentApp;
    public App[] apps;

    private void Start() {
        Change(PhoneState.Instance.currentApp);
        currentApp.gameObject.SetActive(true);
    }

    private void Update() {
        foreach (var app in apps) {
            app.UpdateApp();
        }
    }

    public void Change(string name) {
        foreach (var app in apps) {
            app.gameObject.SetActive(false);
        }

        currentApp = Array.Find(apps, (x) => x.name == name);
        currentApp.gameObject.SetActive(true);
        currentApp.Show();

        PhoneState.Instance.currentApp = name;
    }
}