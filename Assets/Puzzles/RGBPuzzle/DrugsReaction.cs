public class DrugsReaction : RGBPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case RGBPuzzle.Stage.s1GoCycle:
                // Nothing
                break;
            case RGBPuzzle.Stage.s2TalkFlower:
                MessageManager.Instance.PushMessages("SomeMeds");
                break;
            case RGBPuzzle.Stage.s3FindFlower:
                MessageManager.Instance.PushMessages("SomeMeds");
                break;
            case RGBPuzzle.Stage.s4FindFlesh:
                MessageManager.Instance.PushMessages("SomeMeds");
                break;
            case RGBPuzzle.Stage.s5FindKnife:
                MessageManager.Instance.PushMessages("SomeMeds");
                break;
            case RGBPuzzle.Stage.s6FindDrugs:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("GotDrugs");
                break;
            case RGBPuzzle.Stage.s7GetPot:
                // Nothing
                break;
            case RGBPuzzle.Stage.s8WashPot:
                // Nothing
                break;
            case RGBPuzzle.Stage.s9GetFlower:
                // Nothing
                break;
            case RGBPuzzle.Stage.s10GiveFlower:
                // Nothing
                break;
        }
    }
}
