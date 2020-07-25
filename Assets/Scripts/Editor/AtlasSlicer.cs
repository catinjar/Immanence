using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;

public class AtlasSlicer : EditorWindow {
    [MenuItem("Assets/Slice Atlas")]
    public static void SliceAtlas() {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string jsonPath = $"Assets\\Sprites\\Levels\\Data\\{Path.GetFileNameWithoutExtension(path)}.json";
        string jsonData = File.ReadAllText(jsonPath);

        var json = JObject.Parse(jsonData);

        var importer = AssetImporter.GetAtPath(path) as TextureImporter;

        (var width, var height) = GetTextureSize(importer);

        if (importer.spriteImportMode == SpriteImportMode.Multiple) {
            importer.spriteImportMode = SpriteImportMode.Single;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        importer.spriteImportMode = SpriteImportMode.Multiple;

        var metadata = new List<SpriteMetaData>();
        var frames   = json["frames"] as JContainer;

        foreach (var frame in frames.Children<JProperty>()) {
            var frameData = json["frames"][frame.Name]["frame"];

            var smd = new SpriteMetaData();

            smd.pivot = new Vector2(0.5f, 0.5f);
            smd.name  = frame.Name;
            smd.rect  = new Rect() {
                x      = (int)frameData["x"],
                y      = height - (int)frameData["y"] - (int)frameData["h"],
                width  = (int)frameData["w"],
                height = (int)frameData["h"],
            };

            metadata.Add(smd);
        }

        importer.spritesheet = metadata.ToArray();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }

    [MenuItem ("Assets/Build From Atlas")]
    public static void BuildFromAtlas() {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string jsonPath = $"Assets\\Sprites\\Levels\\Data\\{Path.GetFileNameWithoutExtension(path)}.json";
        string jsonData = File.ReadAllText(jsonPath);

        var json = JObject.Parse(jsonData);

        var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

        var backgroundFrame = json["frames"][$"{Path.GetFileNameWithoutExtension(path)} (Background).ase"]["spriteSourceSize"];

        var offset = new Vector3(
             (float)backgroundFrame["x"] - (float)backgroundFrame["w"] / 2.0f,
             (float)backgroundFrame["y"] + (float)backgroundFrame["h"] / 2.0f,
             0.0f
        );

        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Item.prefab", typeof(GameObject));

        foreach (var sprite in sprites) {
            var spriteSourceSize = json["frames"][sprite.name]["spriteSourceSize"];
            var frame            = json["frames"][sprite.name]["frame"];

            var clone = GameObject.Find(sprite.name.Split('(', ')')[1]);

            if (clone == null) {
                clone = Instantiate(prefab) as GameObject;
                clone.name = sprite.name.Split('(', ')')[1];
            }

            clone.transform.position = offset + new Vector3(
                     (float)spriteSourceSize["x"] + (float)spriteSourceSize["w"] / 2.0f,
                    -(float)spriteSourceSize["y"] - (float)spriteSourceSize["h"] / 2.0f,
                     0.0f
                );
            clone.transform.localScale = Vector3.one;
            clone.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public static (int width, int height) GetTextureSize(TextureImporter importer) {
        var method = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);

        var args = new object[2] { 0, 0 };
        method.Invoke(importer, args);

        return ((int)args[0], (int)args[1]);
    }
}
