using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class TexturePropCreator : AssetPostprocessor
{
        private const string ASSET_DIRECTORY = "Assets\\Sprites\\Props";
        private const string PREFAB_DESTINATION_DIRECTORY = "Assets\\Prefabs\\Props";

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
                foreach (string assetPath in importedAssets.Concat(movedAssets).ToArray())
                {
                        if (!Path.GetDirectoryName(assetPath).Equals(ASSET_DIRECTORY))
                        {
                                continue;
                        }

                        var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                        CreatePropPrefab(assetPath, texture);
                }
        }

        private static void CreatePropPrefab(string assetPath, Texture2D texture)
        {
                EnsureDirectoryExists(PREFAB_DESTINATION_DIRECTORY);

                string fileName = Path.GetFileNameWithoutExtension(assetPath);
                string destinationPath = PREFAB_DESTINATION_DIRECTORY + "/" + fileName + ".prefab";

                if (File.Exists(destinationPath))
                {
                        GameObject prefab = PrefabUtility.LoadPrefabContents(destinationPath);
                        SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
                        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                        PrefabUtility.SaveAsPrefabAsset(prefab, destinationPath);
                        GameObject.DestroyImmediate(prefab);
                }
                else
                {
                        GameObject empty = new GameObject();
                        SpriteRenderer renderer = (SpriteRenderer)empty.AddComponent(typeof(SpriteRenderer));
                        renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                        Rigidbody2D rigidbody = (Rigidbody2D)empty.AddComponent(typeof(Rigidbody2D));
                        rigidbody.bodyType = RigidbodyType2D.Kinematic;
                        var collider = empty.AddComponent(typeof(PolygonCollider2D));
                        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(empty, destinationPath);
                        GameObject.DestroyImmediate(empty);
                }
        }

        private static void EnsureDirectoryExists(string directory)
        {
                if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
        }
}
