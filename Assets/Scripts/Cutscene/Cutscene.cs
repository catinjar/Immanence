using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {
    private enum State {
        Stop,
        Run
    }

    public bool playOnAwake = true;
    public List<CutsceneStage> stages;

    private State state = State.Stop;
    private int stageIndex;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (playOnAwake) {
            Run();
        }

        stageIndex = 0;
    }

    private void LateUpdate() {
        if (state != State.Run ||
            MessageManager.Instance.HasMessages ||
            stages.Count == 0) {
            return;
        }

        PlayerState.Instance.Lock();

        if (stages[stageIndex].Play()) {
            ++stageIndex;

            bool finished = stageIndex >= stages.Count;
            if (finished) {
                Stop();
                Destroy(gameObject);
            }
        }
    }

    public void Run() {
        state = State.Run;
        PlayerState.Instance.Lock();
        PlayerState.Instance.cutscene = true;
    }

    public void Stop() {
        state = State.Stop;
        if (!MessageManager.Instance.HasMessages) {
            PlayerState.Instance.Free();
            PlayerState.Instance.cutscene = false;
        }
    }
}
