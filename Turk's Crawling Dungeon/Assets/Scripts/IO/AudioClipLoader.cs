using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using RuntimeAudioClipLoader;

namespace TCD.IO
{
    public class AudioClipLoader 
    {
        private List<string> audioPaths = new List<string>();

        public IEnumerator LoadCoreAudioClips()
        {
            string corePath = Application.streamingAssetsPath + "/Audio/";
            audioPaths.Clear();
            LoadDirectory(corePath);
            DebugLogger.Log($"Loading {audioPaths.Count} sounds from StreamingAssets.");
            foreach (string path in audioPaths)
                yield return LoadAudioClip(path);
        }

        private void LoadDirectory(string directory)
        {
            string[] paths = Directory.GetFiles(directory, "*.wav");
            foreach (string path in paths)
                audioPaths.Add(path);
            string[] directories = Directory.GetDirectories(directory);
            foreach (string dir in directories)
                LoadDirectory(dir);
        }

        private IEnumerator LoadAudioClip(string path)
        {
            string name = GetAudioClipNameFromPath(path);
            var www = UnityWebRequest.Get(path);
            AudioLoader audioLoader = null;
            yield return www.SendWebRequest();
            try
            {
                AudioLoaderConfig audioLoaderConfig = new AudioLoaderConfig(new MemoryStream(www.downloadHandler.data));
                audioLoader = new AudioLoader(audioLoaderConfig);
                audioLoader.StartLoading();
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(new Exception("Audio clip " + path + " failed to load! " + e.Message));
            }
            audioLoader.OnLoadingDone += () => { FinishLoadingAudioClip(name, audioLoader); };
            audioLoader.OnLoadingFailed += e => { LoadingAudioClipException(name, e); };
            audioLoader.OnLoadingAborted+= () => { LoadingAudioClipAborted(name); };
            while (!audioLoader.IsLoadingDone)
                yield return null;
        }

        private void FinishLoadingAudioClip(string name, AudioLoader audioLoader) => Assets.Add(name, audioLoader.AudioClip);
        

        private void LoadingAudioClipException(string name, Exception e)
        {
            Exception exception = new Exception("Failed to load audio file " + name + ". - " + e.Message);
            ExceptionHandler.Handle(exception);
        }

        private void LoadingAudioClipAborted(string name)
        {
            Exception exception = new Exception("Failed to load audio file " + name + ". (Process aborted.)");
            ExceptionHandler.Handle(exception);
        }

        private string GetAudioClipNameFromPath(string path)
        {
            string[] splitBySlash = path.Split(new char[] { '/', '\\' });
            string nameWithExtension = splitBySlash[splitBySlash.Length - 1];
            string[] splitByPeriod = nameWithExtension.Split('.');
            return splitByPeriod[0];
        }
    }
}
