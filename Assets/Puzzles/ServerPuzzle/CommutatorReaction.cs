using UnityEngine;

public class CommutatorReaction : ServerPuzzleReaction {
    public GameObject portsMenu;

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
                menu.SetActive(true);
                break;
            case ServerPuzzle.Stage.OpenPorts:
                portsMenu.SetActive(true);
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