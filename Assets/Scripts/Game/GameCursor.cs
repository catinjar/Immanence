using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCursor : MonoBehaviour {
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        Cursor.visible = false;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (MouseMode.Instance != null && MouseMode.Instance.Looking()) {
            animator.SetInteger("CurrentMode", (int)MouseMode.Instance.mode);
        }
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            animator.SetBool("UsePhone", true);
            return;
        }

        animator.SetInteger("CurrentMode", (int)MouseMode.Instance.mode);
        animator.SetBool("UsePhone", PhoneState.Instance.opened);
        animator.SetBool("RedEye", PlayerProgress.Instance.redEye);
        spriteRenderer.enabled = PlayerState.Instance.CanInteract() && !MessageManager.Instance.HasMessages;
    }

    private void FixedUpdate() {
        var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0.0f;
        transform.position = cursorPosition;
    }
}
