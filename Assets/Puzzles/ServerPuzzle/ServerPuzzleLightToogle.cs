using UnityEngine;

public class ServerPuzzleLightToogle : MonoBehaviour {
    public ServerPuzzle puzzle;

    public GameObject bigServer = null;
    public GameObject commutator = null;
    public GameObject generator = null;
    public GameObject generatorServer = null;
    public GameObject leftServer = null;
    public GameObject rightServer = null;
    public GameObject mainServer = null;

    private void Update() {
        if (generator != null) {
            generator.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.stage > ServerPuzzle.Stage.EnableGenerator);

            foreach (Transform t in generator.transform) {
                var light = t.GetComponent<Light>();
                light.intensity = puzzle.stage == ServerPuzzle.Stage.DisableMainServer ? 17.0f : 5.0f + puzzle.generatorPower * 2.0f;
            }
        }

        if (generatorServer != null) {
            generatorServer.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.stage > ServerPuzzle.Stage.EnableGenerator);
        }

        if (commutator != null) {
            commutator.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.stage > ServerPuzzle.Stage.EnableCommutator);
        }

        if (bigServer != null) {
            bigServer.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.stage > ServerPuzzle.Stage.EnableBigServer);
        }

        if (leftServer != null) {
            leftServer.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.leftServer);
        }

        if (rightServer != null) {
            rightServer.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.rightServer);
        }

        if (mainServer != null) {
            mainServer.SetActive(puzzle.stage == ServerPuzzle.Stage.DisableMainServer || puzzle.stage > ServerPuzzle.Stage.EnableMainServer);
        }
    }
}
