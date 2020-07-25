using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/SelectedItem")]
public class SelectedItem : SingletonScriptableObject<SelectedItem> {
    [NonSerialized]
    public Item selected = null;
}
