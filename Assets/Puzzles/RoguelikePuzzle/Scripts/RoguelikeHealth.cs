using UnityEngine;

public class RoguelikeHealth : MonoBehaviour {
    public Transform healthSprite;
    public float maxHP;

    public float hp;
    private float maxScale;

    private void Start() {
        hp = maxHP;
        maxScale = healthSprite.localScale.x;
    }

    private void Update() {
        healthSprite.localScale = new Vector3(
            maxScale * hp / maxHP,
            healthSprite.localScale.y,
            healthSprite.localScale.z
        );
    }

    public bool TakeDamage(float damage) {
        hp -= damage;

        if (hp <= 0.0f) {
            if (gameObject.name.StartsWith("Player")) {
                Initiate.Fade("RoguelikePuzzle", Color.black, 0.75f);
            }

            Destroy(gameObject);
            return true;
        }

        return false;
    }

    public void Heal(float heal) {
        hp += heal;

        if (hp > maxHP) {
            hp = maxHP;
        }
    }

    public void AddMaxHP(float add) {
        maxHP += add;
        hp = maxHP;
    }
}
