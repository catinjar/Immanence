public class SayReaction : Reaction {
    public string key;

    public override void React()
        => MessageManager.Instance.PushMessages(TextManager.Instance.Say(key));
}
