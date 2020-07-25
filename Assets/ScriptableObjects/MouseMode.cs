using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/MouseMode")]
public class MouseMode : SingletonScriptableObject<MouseMode> {
    public enum Mode {
        Use,
        Look,
        Weapon
    }

    [NonSerialized]
    public Mode mode;

    public MouseMode() {
        mode = Mode.Use;
    }

    public void Use() {
        mode = Mode.Use;
    }

    public void Look() {
        mode = Mode.Look;
    }

    public void Weapon() {
        mode = Mode.Weapon;
    }

    public bool Using() {
        return mode == Mode.Use;
    }

    public bool Looking() {
        return mode == Mode.Look;
    }

    public bool IsWeapon() {
        return mode == Mode.Weapon;
    }
}
