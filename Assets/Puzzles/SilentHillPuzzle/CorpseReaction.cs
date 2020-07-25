public class CorpseReaction : Reaction {
    public override void React() {
        if (!PlayerProgress.Instance.hasWeapon) {
            PlayerProgress.Instance.GetWeapon();
            MessageManager.Instance.PushMessages("GotWeapon");
        }
        else {
            MessageManager.Instance.PushMessages("AlreadyGotWeapon");
        }
    }
}
