using System;
using UnityEngine;

/*
Progress:
1. Sort clips 1, 4, 3 -> 1, 3, 4
2. Get a new clip (Candles clip).
3. Sort clips 1, 2, 3, 4 and set right candles.
4. Get a new clip (Last clip).
5. Sort clips 1, 3, 4, 5.
6. Complete.
*/

[CreateAssetMenu(menuName = "Puzzles/VideoEditorPuzzle", order = 2)]
public class VideoEditorPuzzle : ScriptableObject {
    [Serializable]
    public class VideoSlot {
        public Sprite sprite;
        public bool active;
        public SceneField scene;
        public int order;
        public string tag;
    }

    public enum Stage {
        SortClips1,
        GetClip1,
        SortClips2,
        GetClip2,
        SortClips3,
        Complete
    }

    public VideoEditorPuzzle storedState;

    public Stage stage = Stage.SortClips1;

    public int selectedSlotIndex = -1;
    public int currentClip = 0;

    public SceneField editorScene;
    public SceneField getClipScene;
    public SceneField videoTransitionScene;

    public VideoSlot[] videoSlots;

    public bool[] candles;

    public VideoSlot GetSlot(int index) {
        return videoSlots[index];
    }

    public void SwapSlots(int index1, int index2) {
        var temp = videoSlots[index1];
        videoSlots[index1] = videoSlots[index2];
        videoSlots[index2] = temp;
    }

    public void PlayVideo() {
        selectedSlotIndex = -1;
        currentClip = 0;

        if (!videoSlots[0].active)
            return;
        
        Initiate.Fade(videoSlots[0].scene, Color.black, 0.75f);
    }

    public void NextClip() {
        ++currentClip;

        if (stage == Stage.SortClips1 && currentClip == 3 && videoSlots[currentClip - 1].order == 4) {
            ++stage;
            Initiate.Fade(getClipScene, Color.black, 0.75f);
            return;
        }

        if (stage == Stage.SortClips2 && currentClip == 4 && videoSlots[currentClip - 1].order == 4 &&
            !candles[0] && candles[1] && !candles[2] && candles[3] && candles[4]) {

            ++stage;
            Initiate.Fade(getClipScene, Color.black, 0.75f);
            return;
        }

        if (stage == Stage.SortClips3 && currentClip == 4 && videoSlots[currentClip - 1].order == 5 && videoSlots[currentClip - 2].order == 4) {
            ++stage;
            Initiate.Fade(videoTransitionScene, Color.black, 0.75f);
            return;
        }

        if (currentClip >= 4 ||
            !videoSlots[currentClip].active ||
            videoSlots[currentClip].order < videoSlots[currentClip - 1].order) {

            Initiate.Fade(editorScene, Color.black, 0.75f);

            for (int i = 0; i < candles.Length; ++i)
                candles[i] = true;
        }
        else {
            Initiate.Fade(videoSlots[currentClip].scene, Color.black, 0.75f);
        }
    }

    public void GetClip(string clipName) {
        ++stage;
        var slot = Array.Find(videoSlots, (x) => x.tag == clipName);
        slot.active = true;
    }

    public void FlipCandle(int index) {
        candles[index] = !candles[index];
    }

    public void RestoreState() {
        stage = Stage.SortClips1;
        selectedSlotIndex = -1;
        currentClip = 0;

        editorScene = storedState.editorScene;
        getClipScene = storedState.getClipScene;
        videoTransitionScene = storedState.videoTransitionScene;

        for (int i = 0; i < videoSlots.Length; ++i) {
            videoSlots[i].sprite = storedState.videoSlots[i].sprite;
            videoSlots[i].active = storedState.videoSlots[i].active;
            videoSlots[i].scene = storedState.videoSlots[i].scene;
            videoSlots[i].order = storedState.videoSlots[i].order;
            videoSlots[i].tag = storedState.videoSlots[i].tag;
        }

        for (int i = 0; i < candles.Length; ++i) {
            candles[i] = storedState.candles[i];
        }
    }
}