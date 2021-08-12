using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class DebugLogger
    {
        private const int MAX_LOG_MESSAGES = 2048;

        public static List<string> log = new List<string>();
        public static DebugLoggerVerbosity verbosity =
            DebugLoggerVerbosity.TimeAndFrame;

        public static void Log(string message)
        {
#if UNITY_EDITOR
            string finalMessage = InsertFrameTimeInfo(message);
            Debug.Log(finalMessage);
#endif
            LogMessage(message);
        }

        private static string InsertFrameTimeInfo(string message, bool forceInfo = false)
        {
            string finalMessage = message;
            if (verbosity.HasFlag(DebugLoggerVerbosity.Frame) || forceInfo)
                finalMessage = finalMessage.Insert(0, $"Frame {Time.frameCount}: ");
            if (verbosity.HasFlag(DebugLoggerVerbosity.Time) || forceInfo)
                finalMessage = finalMessage.Insert(0, $"[{DateTime.Now}] ");
            return finalMessage;
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            string finalMessage = InsertFrameTimeInfo(message);
            Debug.LogError(finalMessage);
#endif
            LogErrorMessage(message);
        }

        private static void LogMessage(string message)
        {
            TrimLog();
            string finalMessage = InsertFrameTimeInfo(message, true);
            log.Add(finalMessage);
        }

        private static void LogErrorMessage(string message)
        {
            TrimLog();
            string finalMessage = InsertFrameTimeInfo(message, true);
            finalMessage = finalMessage.Insert(0, "(!!! ERROR !!!) ");
            log.Add(finalMessage);
        }

        private static void TrimLog()
        {
            while (log.Count >= MAX_LOG_MESSAGES)
                log.RemoveAt(0);
        }
    }
}