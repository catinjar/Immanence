public class FleshReaction : RGBPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case RGBPuzzle.Stage.s1GoCycle:
                // Nothing
                break;
            case RGBPuzzle.Stage.s2TalkFlower:
                MessageManager.Instance.PushMessages("WhatTheFuckFlesh");
                break;
            case RGBPuzzle.Stage.s3FindFlower:
                MessageManager.Instance.PushMessages("WhatTheFuckFlesh");
                break;
            case RGBPuzzle.Stage.s4FindFlesh:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("FoundFlesh");
                break;
            case RGBPuzzle.Stage.s5FindKnife:
                MessageManager.Instance.PushMessages("NeedPot");
                break;
            case RGBPuzzle.Stage.s6FindDrugs:
                MessageManager.Instance.PushMessages("NeedDrugsForFlesh");
                break;
            case RGBPuzzle.Stage.s7GetPot:
                ++puzzle.stage;
                MessageManager.Instance.PushMessages("GotPot");
                break;
            case RGBPuzzle.Stage.s8WashPot:
                MessageManager.Instance.PushMessages("EnoughFlesh");
                break;
            case RGBPuzzle.Stage.s9GetFlower:
                MessageManager.Instance.PushMessages("EnoughFlesh");
                break;
            case RGBPuzzle.Stage.s10GiveFlower:
                MessageManager.Instance.PushMessages("EnoughFlesh");
                break;
        }
    }
}
