using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "ContactsManager", menuName = "Gameplay/ContactsManager")]
public class ContactsManager : SingletonScriptableObject<ContactsManager> {
    public Sprite doctorAvatar;
    public Sprite eveAvatar;
    public Sprite adsAvatar;

    [Serializable]
    public class Contact {
        public string name = "";
        public List<(bool isYou, string name)> messages       = new List<(bool isYou, string name)>();
        public List<(bool isYou, string name)> messagesToSend = new List<(bool isYou, string name)>();
        public int unreadCount = 0;
    }

    [NonSerialized]
    public List<Contact> contacts = new List<Contact>();

    [NonSerialized]
    public List<string> sentMessagePacks = new List<string>();

    private bool hasMessagesToSend = false;

    public void Send(string name) {
        if (sentMessagePacks == null)
            sentMessagePacks = new List<string>();

        if (sentMessagePacks.Contains(name))
            return;

        sentMessagePacks.Add(name);

        var array = name.Split('/');
        Send(array[0], array[1]);
    }

    public void Send(string contactName, string messagePackName) {
        var contact = contacts.Find((c) => c.name == contactName);
        if (contact == null) {
            contact = new Contact();
            contact.name = contactName;
            contacts.Add(contact);
        }
        contact.messagesToSend.AddRange(TextManager.Instance.Send(contactName, messagePackName));
        hasMessagesToSend = true;
    }

    public bool NextMessage() {
        if (!hasMessagesToSend) {
            return false;
        }

        bool sent = false;

        foreach (var contact in contacts) {
            if (contact.messagesToSend.Count != 0) {
                contact.messages.Add(contact.messagesToSend[0]);
                contact.messagesToSend.RemoveAt(0);
                ++contact.unreadCount;
                sent = true;
            }
        }

        if (!sent) {
            hasMessagesToSend = false;
        }

        return sent;
    }

    public bool HasUnreadMessages
        => contacts.Where(c => c.unreadCount > 0).Count() != 0;

    public int UnreadMessagesCount
        => contacts.Sum(c => c.unreadCount);

    public void Save() {
        {
            if (contacts == null)
                contacts = new List<Contact>();

            string jsonData = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            PlayerPrefs.SetString("contacts", jsonData);
        }

        {
            if (sentMessagePacks == null)
                sentMessagePacks = new List<string>();
            
            string jsonData = JsonConvert.SerializeObject(sentMessagePacks, Formatting.Indented);
            PlayerPrefs.SetString("sentMessagePacks", jsonData);
        }
    }

    public void Load() {
        {
            string jsonData = PlayerPrefs.GetString("contacts", null);
            contacts = jsonData == null || jsonData == "null" ? new List<Contact>() : JsonConvert.DeserializeObject<List<Contact>>(jsonData);
        }

        {
            string jsonData = PlayerPrefs.GetString("sentMessagePacks", null);
            sentMessagePacks = jsonData == null || jsonData == "null" ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(jsonData);
        }
    }

    public void Reset() {
        contacts = new List<Contact>();
        sentMessagePacks = new List<string>();
    }
}
