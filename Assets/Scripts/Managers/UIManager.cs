using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI itemText;
    public GameObject messageBox;
    public TextMeshProUGUI messageText;
    public GameObject gameSaved;
    public RandomAudioSource writeSound;

    public GameObject pause;
    public GameObject menu;
    public GameObject settings;

    private string sentence = "";
    private int charIndex = 0;
    private bool reading = false;

    private void OnEnable() {
        SceneManager.sceneLoaded += FindCamera;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= FindCamera;
    }

    private void FindCamera(Scene scene, LoadSceneMode mode) {
        GetComponent<Canvas>().worldCamera =
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void LateUpdate() {
        if (SelectedItem.Instance.selected != null && SelectedItem.Instance.selected.GetComponent<Enemy>() == null) {
            itemText.text = TextManager.Instance.ItemName(SelectedItem.Instance.selected.name);
            SelectedItem.Instance.selected = null;
        }
        else {
            itemText.text = "";
        }

        if (MessageManager.Instance.HasMessages) {
            if (sentence == "") {
                StartCoroutine("ReadMessage");
            }
            messageText.text = sentence;
            messageBox.SetActive(true);
        }
        else {
            messageBox.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !MessageManager.Instance.HasMessages && PlayerState.Instance.IsFree()) {
            PlayerState.Instance.paused = !PlayerState.Instance.paused;
            menu.SetActive(true);
            settings.SetActive(false);
        }

        pause.SetActive(PlayerState.Instance.paused);
    }

    public void NextMessage() {
        if (!MessageManager.Instance.HasMessages)
            return;

        if (reading) {
            sentence = MessageManager.Instance.CurrentMessage;
            reading = false;
            charIndex = 0;
            StopCoroutine("ReadMessage");
        }
        else {
            MessageManager.Instance.PullMessage();
            if (MessageManager.Instance.HasMessages) {
                StartCoroutine("ReadMessage");
            }
            else {
                sentence = "";
            }
        }
    }

    public IEnumerator ReadMessage() {
        reading = true;
        
        writeSound.Play();

        sentence = "";
        var fullSentence = MessageManager.Instance.CurrentMessage;

        for (charIndex = 0; charIndex < fullSentence.Length; ++charIndex)
        {
            sentence += fullSentence[charIndex];
            yield return new WaitForSeconds(0.025f);
        }

        reading = false;
    }

    public void Continue() {
        PlayerState.Instance.paused = false;
    }

    public void Settings() {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void Exit() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameSaved() {
        StartCoroutine(GameSavedRoutine());
    }

    private IEnumerator GameSavedRoutine() {
        gameSaved.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        gameSaved.SetActive(false);
    }
}
