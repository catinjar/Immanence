using UnityEngine;

public class EnableReaction : Reaction {
    public GameObject item;

    public override void React()
        => item.SetActive(true);
}
