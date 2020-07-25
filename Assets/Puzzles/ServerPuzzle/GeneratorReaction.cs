using UnityEngine;

public class GeneratorReaction : ServerPuzzleReaction {
    public GameObject powerMenu;

    public override void React() {
        switch (puzzle.stage) {
            case ServerPuzzle.Stage.DisableMainServer:
                MessageManager.Instance.PushMessages("NeedToDisableMainServer");
                break;
            case ServerPuzzle.Stage.EnableGenerator:
                menu.SetActive(true);
                break;
            case ServerPuzzle.Stage.GeneratorPower:
                powerMenu.SetActive(true);
                break;
            case ServerPuzzle.Stage.EnableCommutator:
                MessageManager.Instance.PushMessages("AlrightHere");
                break;
            case ServerPuzzle.Stage.OpenPorts:
                MessageManager.Instance.PushMessages("AlrightHere");
                break;
            case ServerPuzzle.Stage.EnableBigServer:
                MessageManager.Instance.PushMessages("AlrightHere");
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
