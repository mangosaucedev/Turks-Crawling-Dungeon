using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.IO.Serialization
{
    public static class SaveHandler
    {
        public static bool wasLastSaveSuccessful;

        private static BinaryFormatter binaryFormatter;
        private static SavedGame currentSavedGame;

        private static string SavePath => Application.persistentDataPath + "/Saves/";

        public static bool SaveGame()
        {
            binaryFormatter = new BinaryFormatter();
            currentSavedGame = new SavedGame();
            SerializeWorld();
            SerializeObjects();
            WriteSaveGameToFile();
            wasLastSaveSuccessful = true;
            return true;
        }

        private static void SerializeWorld()
        {
            DebugLogger.Log("World successfully serialized!");
        }

        private static void SerializeObjects()
        {
            GameObject root = GameObject.Find("--- Objects ---");
            BaseObject[] objects = root.GetComponentsInChildren<BaseObject>();
            foreach (BaseObject obj in objects)
                currentSavedGame.savedObjects.Add(new SavedObject(obj));
            int objectCount = objects.Length;
            DebugLogger.Log($"{objectCount} objects successfully serialized!");
        }

        private static void WriteSaveGameToFile()
        {
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);
            string filePath = SavePath + "save.sav";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            try
            {
                binaryFormatter.Serialize(fileStream, currentSavedGame);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(new FormatException("Failed to write save to file! " + e.Message));
            }
            DebugLogger.Log("Game successfully saved to file @ " + filePath);
        }
    }
}
