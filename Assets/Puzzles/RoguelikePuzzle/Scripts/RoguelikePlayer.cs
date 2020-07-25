using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoguelikePlayer : MonoBehaviour {
    public RoguelikePuzzle puzzle;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI swordText;
    public TextMeshProUGUI keyText;

    private RoguelikeHealth health;

    private bool moving = false;
    private Coroutine currentMove;

    private int keys = 0;
    private float damage = 1.0f;

    private int shields = 0;
    private int swords = 0;

    private void Start() {
        health = GetComponent<RoguelikeHealth>();

        healthText = GameObject.Find("HP").GetComponent<TextMeshProUGUI>();
        shieldText = GameObject.Find("Shield").GetComponent<TextMeshProUGUI>();
        swordText = GameObject.Find("Sword").GetComponent<TextMeshProUGUI>();
        keyText = GameObject.Find("Key").GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (moving) {
            return;
        }

        UpdateInput();
    }

    private void UpdateInput() {
        int horizontal = 0;
        int vertical = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));
        }

        if (horizontal != 0) {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0) {
            currentMove = StartCoroutine(Move(horizontal, -vertical, 75.0f));
        }
    }

    private IEnumerator Move(int x, int y, float magnitude) {
        moving = true;

        var newPosition = puzzle.Move(x, y);

        var target = new Vector3(
            newPosition.x * transform.localScale.x,
            -newPosition.y * transform.localScale.y,
            0.0f
        );

        while ((transform.position - target).sqrMagnitude > 0.001f) {
            transform.position = Vector3.MoveTowards(transform.position, target, magnitude * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        transform.position = target;

        moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.StartsWith("Wall")) {
            GoBack();
        }

        if (collision.gameObject.name.StartsWith("Enemy")) {
            if (!collision.GetComponent<RoguelikeHealth>().TakeDamage(damage)) {
                GoBack();
            }

            health.TakeDamage(1.0f);
        }

        if (collision.gameObject.name.StartsWith("HP")) {
            health.Heal(2.0f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.StartsWith("Key")) {
            ++keys;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.StartsWith("Door")) {
            if (keys > 0) {
                --keys;
                Destroy(collision.gameObject); 
            }
            else {
                GoBack();
            }
        }

        if (collision.gameObject.name.StartsWith("Boss")) {
            if (!collision.GetComponent<RoguelikeHealth>().TakeDamage(damage)) {
                GoBack();
            }

            health.TakeDamage(2.0f);
        }

        if (collision.gameObject.name.StartsWith("End")) {
            Initiate.Fade("AfterVideo", Color.black, 0.75f);
        }

        if (collision.gameObject.name.StartsWith("Shield")) {
            health.AddMaxHP(1.0f);
            ++shields;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.StartsWith("Sword")) {
            ++damage;
            ++swords;
            Destroy(collision.gameObject);
        }

        healthText.text = health.hp + "/" + health.maxHP;
        shieldText.text = shields.ToString();
        swordText.text = swords.ToString();
        keyText.text = keys.ToString();
    }

    private void GoBack() {
        StopCoroutine(currentMove);
        var direction = puzzle.previousPosition - puzzle.position;
        StartCoroutine(Move(direction.x, direction.y, 25.0f));
    }
}