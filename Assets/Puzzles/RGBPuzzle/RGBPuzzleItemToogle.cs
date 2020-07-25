using UnityEngine;

public class RGBPuzzleItemToogle : MonoBehaviour {
    public RGBPuzzle puzzle;

    public GameObject drugs = null;
    public GameObject pot = null;
    public GameObject flower = null;
    public GameObject flowerOnWindow = null;
    public GameObject girl = null;

    private void Update() {
        if (drugs != null) {
            drugs.SetActive(puzzle.stage <= RGBPuzzle.Stage.s6FindDrugs);
        }

        if (pot != null) {
            pot.SetActive(puzzle.stage <= RGBPuzzle.Stage.s7GetPot);
        }

        if (flower != null) {
            flower.SetActive(puzzle.stage <= RGBPuzzle.Stage.s9GetFlower);
        }

        if (flowerOnWindow != null) {
            flowerOnWindow.SetActive(puzzle.stage > RGBPuzzle.Stage.s10GiveFlower);
        }

        if (girl != null) {
            girl.SetActive(puzzle.stage != RGBPuzzle.Stage.Complete);
        }
    }
}
