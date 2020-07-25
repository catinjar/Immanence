using UnityEngine;

public class Enemy : MonoBehaviour {
    public SceneField restartScene;
    public NyctophiliaPuzzle.Enemy parent;
    public AudioSource sound;

    public float velocity = 15.0f;
    public int health = 5;

    private GameObject player;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Start() {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        var direction = player.transform.position - transform.position;
        direction.y = 0;
        direction.z = 0;

        transform.position += direction.normalized * velocity * Time.deltaTime;
        parent.position = transform.position.x;

        boxCollider.enabled = parent.alive;
        sound.volume = parent.alive ? Mathf.Clamp(1.0f - direction.magnitude / 100.0f, 0.3f, 1.0f) : 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!parent.alive)
            return;

        if (collision.gameObject.name == "Player") {
            Initiate.Fade(restartScene, Color.black, 0.75f);
        }

        if (collision.gameObject.name.StartsWith("Bullet")) {
            Destroy(collision.gameObject);
            health -= Random.Range(2, 3);

            if (health <= 0) {
                transform.position -= new Vector3(0.0f, 2.0f, 0.0f);
                animator.SetBool("Dead", true);
                enabled = false;
                parent.alive = false;
                sound.volume = 0.0f;
                GetComponent<Item>().enabled = false;
                MouseMode.Instance.Use();
            }
        }
    }
}
