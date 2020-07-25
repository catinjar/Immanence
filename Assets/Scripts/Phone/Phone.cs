using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {
    public GameObject phone;
    public Image notification;

    public GameObject contactsMessageCountBackground;
    public TextMeshProUGUI contactsMessageCount;

    public GameObject twitterMessageCountBackground;
    public TextMeshProUGUI twitterMessageCount;

    public TextMeshProUGUI time;

    private const float lerpSpeed = 10.0f;

    private Vector3 openedPosition         = new Vector3(40.0f, 0.0f, 0.0f);
    private Vector3 hiddenPosition         = new Vector3(40.0f, -90.0f, 0.0f);
    private Vector3 hiddenSelectedPosition = new Vector3(40.0f, -81.5f, 0.0f);

    private Vector3 currentPosition;

    private void Update() {
        if (!PlayerProgress.Instance.hasPhone) {
            return;
        }

        if (MessageManager.Instance.HasMessages) {
            return;
        }

        if (PhoneState.Instance.opened) {
            currentPosition = openedPosition;
        }
        else {
            bool show = (Input.mousePosition.y < 45.0f || ContactsManager.Instance.HasUnreadMessages) && !PlayerState.Instance.cutscene;
            currentPosition = show ? hiddenSelectedPosition : hiddenPosition;
        }

        phone.transform.localPosition = Vector3.Lerp(
            phone.transform.localPosition,
            currentPosition,
            lerpSpeed * Time.deltaTime
        );

        notification.gameObject.SetActive(ContactsManager.Instance.HasUnreadMessages);

        int unreadMessagesCount = ContactsManager.Instance.UnreadMessagesCount;
        contactsMessageCountBackground.SetActive(unreadMessagesCount > 0);
        contactsMessageCount.text = unreadMessagesCount.ToString();

        int twitterMessagesCount = TwitterManager.Instance.UnreadMessagesCount;
        twitterMessageCountBackground.SetActive(twitterMessagesCount > 0);
        twitterMessageCount.text = twitterMessagesCount.ToString();

        time.text = $"{DateTime.Now.Hour.ToString("D2")}:{DateTime.Now.Minute.ToString("D2")}";
    }

    public void Hide() {
        if (PlayerState.Instance.cutscene)
        {
            return;
        }

        if (PhoneState.Instance.opened) {
            PlayerState.Instance.Free();
        }
        PhoneState.Instance.opened = false;
    }

    public void Open() {
        PhoneState.Instance.opened = true;
        PlayerState.Instance.LockMove();
    }
}
