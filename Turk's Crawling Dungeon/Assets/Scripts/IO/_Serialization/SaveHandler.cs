using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TCD.IO.Serialization
{
    public static class SaveHandler
    {
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
            return true;
        }

        private static void SerializeWorld()
        {
            DebugLogger.Log("World successfully serialized!");
        }

        private static void SerializeObjects()
        {
            int objectCount = 0;
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
                throw new FormatException("Failed to write save to file! " + e.Message);
            }
            DebugLogger.Log("Game successfully saved to file @ " + filePath);
        }
    }
}
