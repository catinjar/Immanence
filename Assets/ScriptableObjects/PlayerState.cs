using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/PlayerState")]
public class PlayerState : SingletonScriptableObject<PlayerState> {
    [Flags]
    public enum State {
        Free = 0,
        LockMove = 1,
        LockInteract = 2,
    }

    [NonSerialized]
    public State state = State.Free;
    [NonSerialized]
    public bool flipped = false;
    [NonSerialized]
    public bool paused = false;
    [NonSerialized]
    public bool cutscene = false;

    public void Free() {
        state = State.Free;
    }

    public void FreeInteract() {
        state &= ~State.LockInteract;
    }

    public void FreeMove() {
        state &= ~State.LockMove;
    }

    public void Lock() {
        state = State.LockMove | State.LockInteract;
    }

    public void LockMove() {
        state |= State.LockMove;
    }

    public void LockInteract() {
        state |= State.LockInteract;
    }

    public bool IsFree() {
        return state == State.Free;
    }

    public bool CanMove() {
        return !state.HasFlag(State.LockMove);
    }

    public bool CanInteract() {
        return !state.HasFlag(State.LockInteract);
    }

    public bool IsLocked() {
        return state == (State.LockMove | State.LockInteract);
    }
}
