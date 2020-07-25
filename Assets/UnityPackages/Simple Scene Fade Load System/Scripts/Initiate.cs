using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initiate {
    public static void Fade(string scene, Color color, float damp){
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        var init = new GameObject();
		init.name = "Fader";
		init.AddComponent<Fader>();

		var scr = init.GetComponent<Fader>();
		scr.fadeDamp = damp;
		scr.fadeScene = scene;
		scr.fadeColor = color;
		scr.start = true;
	}
}
