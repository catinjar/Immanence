using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContactsApp : App {
    public GameObject contactsScreen;
    public GameObject contactMessagesScreen;
    public GameObject backButton;
    public TextMeshProUGUI title;
    public TextMeshProUGUI contactText;
    public ScrollRect scroll;

    public List<GameObject> contactObjects    = new List<GameObject>();
    public List<Image> avatars                = new List<Image>();
    public List<TextMeshProUGUI> names        = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> lastMessages = new List<TextMeshProUGUI>();
    public List<GameObject> unreadSquare      = new List<GameObject>();
    public List<TextMeshProUGUI> unreadCounts = new List<TextMeshProUGUI>();

    private int contactIndex = 0;

    private float sendTimeCurrent = 0.0f;
    private float sendTime = 1.0f;

    private readonly float sendTimeMin = 1.0f;
    private readonly float sendTimeMax = 3.0f;

    private void OnEnable()
    {
        if (scroll != null)
            scroll.verticalNormalizedPosition = 0.0f;
    }

    public override void UpdateApp() {
        if (scroll != null && PlayerState.Instance.cutscene)
            scroll.verticalNormalizedPosition = 0.0f;

        sendTimeCurrent += Time.deltaTime;
        if (sendTimeCurrent > sendTime) {
            if (ContactsManager.Instance.NextMessage()) {
                scroll.verticalNormalizedPosition = 0.0f;
            }
            sendTimeCurrent = 0.0f;
            sendTime = Random.Range(sendTimeMin, sendTimeMax);
        }

        for (int i = 0; i < contactObjects.Count; ++i) {
            contactObjects[i].SetActive(false);
        }

        var contacts = ContactsManager.Instance.contacts;

        for (int i = 0; i < contacts.Count; ++i) {
            if (contacts[i].messages.Count == 0) {
                continue;
            }

            contactObjects[i].SetActive(true);

            if (contacts[i].name == "Girl")
            {
                int asciiCharacterStart = 65;
                int asciiCharacterEnd = 122;
                int characterCount = 10;

                var sb = new StringBuilder();

                for (int j = 0; j < characterCount; ++j)
                    sb.Append((char)(Random.Range(asciiCharacterStart, asciiCharacterEnd)));

                names[i].text = sb.ToString();
            }
            else
            {
                names[i].text = TextManager.Instance.GetText(contacts[i].name);
            }

            if (contacts[i].messages.Count > 0) {
                var lastMessage = contacts[i].messages[contacts[i].messages.Count - 1];
                var message = lastMessage.name;
                lastMessages[i].text = message.Length > 25 ? (message.Substring(0, 25) + "...") : message;
            }

            if (contactMessagesScreen.activeInHierarchy && i == contactIndex)
                contacts[i].unreadCount = 0;

            unreadSquare[i].SetActive(contacts[i].unreadCount != 0);
            unreadCounts[i].text = contacts[i].unreadCount.ToString();

            switch (contacts[i].name) {
                case "StrangeMan":
                    avatars[i].sprite = ContactsManager.Instance.doctorAvatar;
                    break;
                case "Girl":
                    avatars[i].sprite = ContactsManager.Instance.eveAvatar;
                    break;
                case "Ads":
                    avatars[i].sprite = ContactsManager.Instance.adsAvatar;
                    break;
            }
        }

        if (contacts.Count == 0 || contacts[contactIndex].messages.Count == 0) {
            return;
        }

        contactText.text = "";
        foreach (var message in contacts[contactIndex].messages) {
            contactText.text += message.name;
            contactText.text += "\n\n";
        }

        if (!contactsScreen.activeInHierarchy)
        {
            if (ContactsManager.Instance.contacts[contactIndex].name == "Girl")
            {
                int asciiCharacterStart = 65;
                int asciiCharacterEnd = 122;
                int characterCount = 10;

                var sb = new StringBuilder();

                for (int j = 0; j < characterCount; ++j)
                    sb.Append((char)(Random.Range(asciiCharacterStart, asciiCharacterEnd)));

                title.text = sb.ToString();
            }
            else
            {
                title.text = TextManager.Instance.GetText(ContactsManager.Instance.contacts[contactIndex].name);
            }
        }
    }

    public override void Show() {
        GoToContacts();
    }

    public void GoToContactMessages(int index) {
        contactIndex = index;
        ContactsManager.Instance.contacts[index].unreadCount = 0;
        contactsScreen.SetActive(false);
        contactMessagesScreen.SetActive(true);
        backButton.SetActive(true);

        if (ContactsManager.Instance.contacts[contactIndex].name == "Girl")
        {
            int asciiCharacterStart = 65;
            int asciiCharacterEnd = 122;
            int characterCount = 10;

            var sb = new StringBuilder();

            for (int j = 0; j < characterCount; ++j)
                sb.Append((char)(Random.Range(asciiCharacterStart, asciiCharacterEnd)));

            title.text = sb.ToString();
        }
        else
        {
            title.text = TextManager.Instance.GetText(ContactsManager.Instance.contacts[contactIndex].name);
        }
    }

    public void GoToContactMessages(string name) {
        int index = ContactsManager.Instance.contacts.FindIndex(c => c.name == name);
        GoToContactMessages(index);
    }

    public void GoToContacts() {
        contactsScreen.SetActive(true);
        contactMessagesScreen.SetActive(false);
        backButton.SetActive(false);
        title.text = "Contacts";
    }
}
