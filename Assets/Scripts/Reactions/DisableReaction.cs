using UnityEngine;

public class DisableReaction : Reaction {
    public GameObject item;

    public override void React()
        => item.SetActive(false);
}
