using UnityEngine;
using UnityEngine.UI;

public class VideoSlot : MonoBehaviour {
    public VideoEditorPuzzle puzzle;
    public int videoSlotIndex;

    public GameObject selectedFrame;

    private Image image;

    public void Start() {
        image = GetComponent<Image>();
    }

    public void Update() {
        var slot = puzzle.GetSlot(videoSlotIndex);

        if (slot.active) {
            image.sprite = slot.sprite;
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else {
            image.sprite = null;
            image.color = new Color(105.0f / 255.0f, 105.0f / 255.0f, 105.0f / 255.0f, 1.0f);
        }

        selectedFrame.SetActive(videoSlotIndex == puzzle.selectedSlotIndex);
    }

    public void Select() {
        if (puzzle.selectedSlotIndex == -1) {
            puzzle.selectedSlotIndex = videoSlotIndex;
        }
        else {
            puzzle.SwapSlots(videoSlotIndex, puzzle.selectedSlotIndex);
            puzzle.selectedSlotIndex = -1;
        }
    }
}
