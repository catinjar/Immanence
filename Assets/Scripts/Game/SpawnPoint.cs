using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public SceneField[] scenes = new SceneField[1];

    public bool Contains(string sceneName) {
        foreach (var scene in scenes) {
            if (scene.SceneName == sceneName) {
                return true;
            }
        }
        return false;
    }
}
