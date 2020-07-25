using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 30.0f;
    public RandomAudioSource stepSound;

    public GameObject phoneLight;

    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    private Vector2 destination;
    private bool collided = false;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        var spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var spawnPoint in spawnPoints) {
            if (spawnPoint.Contains(PlayerPrefs.GetString("PreviousScene"))) {
                transform.position = spawnPoint.transform.position;
            }
        }

        body.MovePosition(transform.position);
        destination = body.position;

        PlayerState.Instance.paused = false;
    }

    private void Update() {
        if (PlayerState.Instance.paused) {
            boxCollider.enabled = false;
            return;
        }

        boxCollider.enabled = true;

        if (Input.GetButtonDown("Fire1")) {
            bool locked =
                Input.mousePosition.y < 45.0f ||
                !PlayerState.Instance.CanMove() ||
                MouseMode.Instance.Looking() ||
                MouseMode.Instance.IsWeapon() ||
                PhoneState.Instance.opened;

            if (locked) {
                return;
            }

            Vector2 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination.x = clickPoint.x;
            if (SelectedItem.Instance.selected == null) {
                NextReaction.Instance.reactions = null;
            }
        }
    }

    private void FixedUpdate() {
        if (PlayerState.Instance.state.HasFlag(PlayerState.State.LockMove)) {
            destination.x = body.position.x;
        }

        body.MovePosition(Vector2.MoveTowards(body.position, destination, speed * Time.deltaTime));
        UpdateSprite(destination.x - body.position.x);

        if (Mathf.Abs(destination.x - body.position.x) < 5.0f) {
            NextReaction.Instance.ExecuteReaction();
        }
    }

    private void UpdateSprite(float direction) {
        if (phoneLight != null) {
            phoneLight.SetActive(PhoneState.Instance.opened);
        }

        if (Mathf.Abs(direction) < 0.01f || collided) {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsShooting", MouseMode.Instance.IsWeapon());
            animator.SetBool("IsPhone", PhoneState.Instance.opened);
            return;
        }

        bool flipped = direction != 0.0f ? (direction > 0.0f ? false : true) : sprite.flipX;
        PlayerState.Instance.flipped = flipped;
        animator.SetBool("IsFlipped", flipped);

        animator.SetBool("IsWalking", true);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsPhone", false);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        destination = body.position;
        collided = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collided = false;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        animator.SetBool("IsWalking", false);
    }

    public void Stop() {
        destination = body.position;
    }

    public void OnStep() {
        stepSound.Play();
    }
} 
