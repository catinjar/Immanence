using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneState", menuName = "Gameplay/PhoneState")]
public class PhoneState : SingletonScriptableObject<PhoneState> {
    [NonSerialized]
    public bool opened = false;
    [NonSerialized]
    public string currentApp = "HomeApp";

    public void Save() {
        PlayerPrefs.SetString("currentApp", currentApp);
    }

    public void Load() {
        currentApp = PlayerPrefs.GetString("currentApp", "HomeApp");
    }

    public void Reset() {
        currentApp = "HomeApp";
    }
}
