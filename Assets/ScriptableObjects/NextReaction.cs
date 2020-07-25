using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/NextReaction")]
public class NextReaction : SingletonScriptableObject<NextReaction> {
    [NonSerialized]
    public Reaction[] reactions = null;

    public void ExecuteReaction() {
        if (reactions != null) {
            foreach (var r in reactions) {
                r.React();
            }
            reactions = null;
        }
    }
}
