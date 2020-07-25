using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour {
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if (GameSettings.lastSaveScene == SceneManager.GetActiveScene().name)
            return;

        if (transform.position.x - player.transform.position.x < 15.0f) {
            GameSettings.lastSaveScene = SceneManager.GetActiveScene().name;
            GameSettings.Save();

            FindObjectOfType<UIManager>().GameSaved();
        }
    }
}
