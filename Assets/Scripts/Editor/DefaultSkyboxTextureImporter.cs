using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DefaultSkyboxTextureImporter : AssetPostprocessor
{
    private static readonly string textureDir = "Assets/Textures";

    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        if (importer.assetPath.Contains(textureDir))
        {
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.maxTextureSize = 8192;
            importer.SaveAndReimport();
        }
    }
}
