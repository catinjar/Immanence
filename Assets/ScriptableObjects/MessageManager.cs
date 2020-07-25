using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/MessageManager")]
public class MessageManager : SingletonScriptableObject<MessageManager> {
    private List<string> messages = new List<string>();

    public bool HasMessages => messages.Count > 0;
    public int MessageCount => messages.Count;
    public string CurrentMessage => messages[0];

    public void PushMessages(List<string> messages) {
        if (messages == null) {
            this.messages.Insert(0, "NULL");
        }
        else if (messages.Count == 0) {
            return;
        }
        else {
            this.messages.InsertRange(0, messages);
            PlayerState.Instance.LockMove();
        }
    }

    public void PushMessages(string key) {
        PushMessages(TextManager.Instance.Say(key));
    }

    public void PullMessage() {
        messages.RemoveAt(0);

        if (messages.Count == 0) {
            PlayerState.Instance.Free();
        }
    }
}
