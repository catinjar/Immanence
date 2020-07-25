using UnityEngine;

public class ChangeAnimatorReaction : Reaction {
    public GameObject player;
    public RuntimeAnimatorController controller;

    public override void React()
        => player.GetComponent<Animator>().runtimeAnimatorController = controller;
}
