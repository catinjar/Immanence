public class GirlReaction : RGBPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case RGBPuzzle.Stage.s1GoCycle:
                // Nothing
                break;
            case RGBPuzzle.Stage.s2TalkFlower:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s3FindFlower:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s4FindFlesh:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s5FindKnife:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s6FindDrugs:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s7GetPot:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s8WashPot:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s9GetFlower:
                MessageManager.Instance.PushMessages("SomethingIsNotHere");
                break;
            case RGBPuzzle.Stage.s10GiveFlower:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("DoneHere");
                break;
        }
    }
}
