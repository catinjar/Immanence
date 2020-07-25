using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cutscene))]
public class CutsceneEditor : Editor {
    private static readonly string[] options = { "Delay", "Change Scene", "Text Fade", "Say", "Open Phone", "Send", "Flip", "Reaction" };
    private int index = 0;

    public override void OnInspectorGUI() {
        var cutscene = target as Cutscene;

        cutscene.playOnAwake = GUILayout.Toggle(cutscene.playOnAwake, "Play on Awake");

        index = EditorGUILayout.Popup(index, options);

        if (cutscene.stages == null) {
            cutscene.stages = new List<CutsceneStage>();
        }

        if (cutscene.stages.Count == 0) {
            if (GUILayout.Button("Start")) {
                cutscene.stages.Add(new CutsceneStage((CutsceneStage.Type)index));
            }

            return;
        }

        Undo.RecordObject(target, "Cutscene changed");

        for (int i = 0; i < cutscene.stages.Count; ++i) {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField(options[cutscene.stages[i].GetStageType()], EditorStyles.boldLabel);

            cutscene.stages[i].GUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Stage")) {
                cutscene.stages.Insert(i + 1, new CutsceneStage((CutsceneStage.Type)index));
            }

            if (GUILayout.Button("Delete Stage")) {
                cutscene.stages.Remove(cutscene.stages[i]);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
