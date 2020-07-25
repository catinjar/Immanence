public class FlowerReaction : RGBPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case RGBPuzzle.Stage.s1GoCycle:
                // Nothing
                break;
            case RGBPuzzle.Stage.s2TalkFlower:
                MessageManager.Instance.PushMessages("NiceFlower");
                break;
            case RGBPuzzle.Stage.s3FindFlower:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("NeedFlower");
                break;
            case RGBPuzzle.Stage.s4FindFlesh:
                MessageManager.Instance.PushMessages("NeedFlower");
                break;
            case RGBPuzzle.Stage.s5FindKnife:
                MessageManager.Instance.PushMessages("NeedFlower");
                break;
            case RGBPuzzle.Stage.s6FindDrugs:
                MessageManager.Instance.PushMessages("NeedFlower");
                break;
            case RGBPuzzle.Stage.s7GetPot:
                MessageManager.Instance.PushMessages("NeedFlower");
                break;
            case RGBPuzzle.Stage.s8WashPot:
                MessageManager.Instance.PushMessages("NeedToWashPot");
                break;
            case RGBPuzzle.Stage.s9GetFlower:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("GotFlower");
                break;
            case RGBPuzzle.Stage.s10GiveFlower:
                // Nothing
                break;
        }
    }
}
