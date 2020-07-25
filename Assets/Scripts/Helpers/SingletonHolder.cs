using UnityEngine;

public class SingletonHolder : MonoBehaviour {
    public PlayerProgress playerProgress;
    public MouseMode mouseMode;
    public PlayerState playerState;
    public SelectedItem selectedItem;
    public NextReaction nextReaction;
    public MessageManager messageManager;
    public TextManager textManager;
    public PhoneState phoneState;
    public ContactsManager contactsManager;
    public TwitterManager twitterManager;

    private void Start()
        => nextReaction.reactions = null;
}
