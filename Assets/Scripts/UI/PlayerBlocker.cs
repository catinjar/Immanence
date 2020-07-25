using UnityEngine;

public class PlayerBlocker : MonoBehaviour {
    private void OnEnable()
        => PlayerState.Instance.LockMove();

    private void OnDisable()
        => PlayerState.Instance.Free();
}
