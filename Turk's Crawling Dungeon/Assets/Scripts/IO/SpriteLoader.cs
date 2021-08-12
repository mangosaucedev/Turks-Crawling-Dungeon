using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TCD.IO
{
    public class SpriteLoader 
    {
        private const float DEFAULT_PIXELS_PER_UNIT = 100;
        private List<string> spritePaths = new List<string>();

        public IEnumerator LoadCoreSprites()
        {
            string corePath = Application.streamingAssetsPath + "/Sprites/";
            spritePaths.Clear();
            LoadDirectory(corePath);
            DebugLogger.Log($"Loading {spritePaths.Count} sprites from StreamingAssets.");
            foreach (string path in spritePaths)
                LoadSprite(path);
            yield break;
        }

        private void LoadDirectory(string directory)
        {
            string[] paths = Directory.GetFiles(directory, "*.png");
            foreach (string path in paths)
                spritePaths.Add(path);
            string[] directories = Directory.GetDirectories(directory);
            foreach (string dir in directories)
                LoadDirectory(dir);
        }

        private void LoadSprite(string path)
        {
            Texture2D texture = LoadTexture(path);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.one / 2f, DEFAULT_PIXELS_PER_UNIT);
            string name = GetSpriteNameFromPath(path);
            Assets.Add(name, sprite);
        }

        private Texture2D LoadTexture(string path)
        {
            if (File.Exists(path))
            {
                byte[]  bytes = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(bytes))
                    return texture;
            }
            throw new Exception($"SpriteLoader failed - invalid texture @ {path}!");
        }

        private string GetSpriteNameFromPath(string path)
        {
            string[] splitBySlash = path.Split(new char[] { '/', '\\' });
            string nameWithExtension = splitBySlash[splitBySlash.Length - 1];
            string[] splitByPeriod = nameWithExtension.Split('.');
            return splitByPeriod[0];
        }
    }
}
