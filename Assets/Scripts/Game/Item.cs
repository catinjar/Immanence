using UnityEngine;

public class Item : MonoBehaviour {
    public bool inspectable = true;

    private bool selected = false;
    private Reaction[] reactions;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake() {
        reactions = GetComponents<Reaction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (boxCollider == null) {
            return;
        }

        if (!PlayerState.Instance.CanMove() || PhoneState.Instance.opened || PlayerState.Instance.paused) {
            boxCollider.enabled = false;
        }
        else {
            boxCollider.enabled = true;
        }
    }

    private void OnMouseOver() {
        if (!PlayerState.Instance.CanMove() || PhoneState.Instance.opened || PlayerState.Instance.paused) {
            return;
        }

        if (spriteRenderer == null || spriteRenderer.sprite == null || IsSpriteSelected()) {
            selected = true;
            SelectedItem.Instance.selected = this;
        }
        else {
            selected = false;
        }

        if (selected) {
            if (MouseMode.Instance.mode != MouseMode.Mode.Weapon) {
                if (reactions.Length != 0) {
                    MouseMode.Instance.Use();
                }
                else if (inspectable) {
                    MouseMode.Instance.Look();
                }
                else if (GetComponent<Enemy>() != null && PlayerProgress.Instance.hasWeapon) {
                    MouseMode.Instance.Weapon();
                }
                else {
                    MouseMode.Instance.Use();
                }
            }
        }
        else {
            MouseMode.Instance.Use();
        }
    }

    private void OnMouseUp() {
        if (selected == false ||
            !PlayerState.Instance.CanMove() ||
            GetComponent<Enemy>() != null) {

            return;
        }

        NextReaction.Instance.reactions = null;

        if (reactions.Length != 0) {
            NextReaction.Instance.reactions = reactions;
        }
        else if (inspectable) {
            MessageManager.Instance.PushMessages(TextManager.Instance.AboutItem(name));
        }
    }

    private void OnMouseExit() {
        MouseMode.Instance.Use();
    }

    private bool IsSpriteSelected() {
        var sprite = GetComponent<SpriteRenderer>().sprite;

        var pixelPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pixelPosition = transform.InverseTransformPoint(pixelPosition);
        pixelPosition += sprite.pivot;
        pixelPosition += new Vector2(sprite.textureRect.x, sprite.textureRect.y);

        int pixelX = Mathf.RoundToInt(pixelPosition.x);
        int pixelY = Mathf.RoundToInt(pixelPosition.y);

        var color = sprite.texture.GetPixel(pixelX, pixelY);

        if (color.a != 0.0f) {
            return true;
        }

        return false;
    }
}