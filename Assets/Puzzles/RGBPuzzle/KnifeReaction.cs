public class KnifeReaction : RGBPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case RGBPuzzle.Stage.s1GoCycle:
                // Nothing
                break;
            case RGBPuzzle.Stage.s2TalkFlower:
                MessageManager.Instance.PushMessages("MayNeedSomething");
                break;
            case RGBPuzzle.Stage.s3FindFlower:
                MessageManager.Instance.PushMessages("MayNeedSomething");
                break;
            case RGBPuzzle.Stage.s4FindFlesh:
                MessageManager.Instance.PushMessages("MayNeedSomething");
                break;
            case RGBPuzzle.Stage.s5FindKnife:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("FoundKnife");
                break;
            case RGBPuzzle.Stage.s6FindDrugs:
                MessageManager.Instance.PushMessages("AlreadyWashedPot");
                break;
            case RGBPuzzle.Stage.s7GetPot:
                MessageManager.Instance.PushMessages("AlreadyWashedPot");
                break;
            case RGBPuzzle.Stage.s8WashPot:
                MessageManager.Instance.PushMessages("AlreadyWashedPot");
                break;
            case RGBPuzzle.Stage.s9GetFlower:
                MessageManager.Instance.PushMessages("AlreadyWashedPot");
                break;
            case RGBPuzzle.Stage.s10GiveFlower:
                MessageManager.Instance.PushMessages("AlreadyWashedPot");
                break;
        }
    }
}
