public class SendReaction : Reaction {
    public string contactName;
    public string messagePackName;

    public override void React()
        => ContactsManager.Instance.Send(contactName, messagePackName);
}
