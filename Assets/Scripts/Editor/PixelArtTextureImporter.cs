using UnityEngine;
using UnityEditor;
using System.IO;

public class PixelArtTextureImporter : AssetPostprocessor {
    private void OnPostprocessTexture(Texture2D texture) {
        var importer = assetImporter as TextureImporter;

        if (importer.spritePixelsPerUnit == 100) {
            importer.spritePixelsPerUnit = 1;
        }

        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.isReadable = true;

        if (Path.GetFileNameWithoutExtension(assetPath).EndsWith("Normal") && importer.textureType != TextureImporterType.NormalMap) {
            var defaultImporter = AssetImporter.GetAtPath("Assets/Sprites/DefaultNormal.png") as TextureImporter;
            var defaultSettings = new TextureImporterSettings();

            defaultImporter.ReadTextureSettings(defaultSettings);
            importer.SetTextureSettings(defaultSettings);
        }
    }
}
