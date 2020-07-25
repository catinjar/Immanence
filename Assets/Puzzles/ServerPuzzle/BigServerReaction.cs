public class BigServerReaction : ServerPuzzleReaction {
    public override void React() {
        switch (puzzle.stage) {
            case ServerPuzzle.Stage.DisableMainServer:
                MessageManager.Instance.PushMessages("NeedToDisableMainServer");
                break;
            case ServerPuzzle.Stage.EnableGenerator:
                MessageManager.Instance.PushMessages("NoPower");
                break;
            case ServerPuzzle.Stage.GeneratorPower:
                MessageManager.Instance.PushMessages("NoPower");
                break;
            case ServerPuzzle.Stage.EnableCommutator:
                MessageManager.Instance.PushMessages("NeedToEnableCommutator");
                break;
            case ServerPuzzle.Stage.OpenPorts:
                MessageManager.Instance.PushMessages("NoPower");
                break;
            case ServerPuzzle.Stage.EnableBigServer:
                menu.SetActive(true);
                break;
            case ServerPuzzle.Stage.EnableBoundServers:
                MessageManager.Instance.PushMessages("AlrightHere");
                break;
            case ServerPuzzle.Stage.EnableMainServer:
                MessageManager.Instance.PushMessages("AlrightHere");
                break;
            case ServerPuzzle.Stage.Complete:
                MessageManager.Instance.PushMessages("AlrightHere");
                break;
        }
    }
}
