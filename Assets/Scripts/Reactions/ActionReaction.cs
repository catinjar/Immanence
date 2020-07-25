using UnityEngine.Events;

public class ActionReaction : Reaction {
    public UnityEvent action;

    public override void React()
        => action.Invoke();
}
