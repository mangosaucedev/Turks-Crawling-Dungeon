using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TCD.Zones.Templates;

namespace TCD.IO
{
    [AssetLoader]
    public class ZoneTemplateLoader : IAssetLoader
    {
        private int templatesToLoad;
        private int templatesLoaded;

        private static string Path => Application.streamingAssetsPath + "/ZoneTemplates/";

        public float Progress
        {
            get
            {
                if (templatesToLoad == 0)
                    return 1f;
                return (float) templatesLoaded / templatesToLoad;
            }
        }

        public IEnumerator LoadAll()
        {
            string[] paths = GetTemplatePaths();
            foreach (string path in paths)
                yield return LoadTemplateFromPath(path);
        }

        private string[] GetTemplatePaths()
        {
            var files = Directory.GetFiles(Path, "*.txt", SearchOption.AllDirectories);
            List<string> paths = new List<string>();
            foreach (var file in files)
            {
                if (file.EndsWith(".meta"))
                    continue;
                paths.Add(file);
                templatesToLoad++;
            }
            return paths.ToArray();
        }

        private IEnumerator LoadTemplateFromPath(string path)
        {
            try
            {
                ReadTemplate(path);
                templatesLoaded++;
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleMessage($"Could not load zone template @{path}: {e.Message}");
            }
            yield return null;
        }

        private void ReadTemplate(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    ReadTemplate(path, reader);
                }
            }
        }

        private void ReadTemplate(string path, StreamReader reader)
        {
            List<string> lines = new List<string>();
            string line;
            int width = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#"))
                    continue;
                lines.Add(line);
                width = Mathf.Max(width, line.Length);
            }

            int height = lines.Count;

            if (width == 0 || height == 0)
                return;

            TGrid<char> characters = new TGrid<char>(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    var characterLine = lines[y];
                    if (x < characterLine.Length)
                        characters[x, height - y - 1] = characterLine[x];
                    else
                        characters[x, height - y - 1] = ZoneTemplate.EMPTY_CHAR;
                }

            Assets.Add(System.IO.Path.GetFileNameWithoutExtension(path), new ZoneTemplate(characters));
        }
    }
}
