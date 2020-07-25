using UnityEngine;

public class FlipReaction : Reaction {
    public GameObject entity;

    public override void React()
        => entity.SetActive(!entity.activeSelf);
}