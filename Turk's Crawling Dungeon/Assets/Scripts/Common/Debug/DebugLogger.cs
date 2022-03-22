using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TCD
{
    public static class DebugLogger
    {
        private const int MAX_LOG_MESSAGES = 2048;

        public static List<string> log = new List<string>();
        public static List<string> consoleLog = new List<string>();
        public static DebugLoggerPrefix prefix =
            DebugLoggerPrefix.TimeAndFrame;
        public static DebugLoggerTrace trace =
            DebugLoggerTrace.Verbose;

        private static int comments;
        private static int errors;
        private static int exceptions;

        private static string LogFilePath => Application.persistentDataPath + "/TCDLog.txt";

        static DebugLogger()
        {
            if (File.Exists(LogFilePath))
                File.WriteAllText(LogFilePath, string.Empty);
        }

        public static void Log(string message)
        {
            string finalMessage = InsertFrameTimeInfo(message);
            ConsoleLogEvent(finalMessage);
            finalMessage = InsertStackTrace(finalMessage);
#if UNITY_EDITOR
            Debug.Log(finalMessage);
#endif
            LogMessage(finalMessage);
        }

        private static string InsertFrameTimeInfo(string message, bool forceInfo = false)
        {
            string finalMessage = message;
            if (prefix.HasFlag(DebugLoggerPrefix.Frame) || forceInfo)
                finalMessage = finalMessage.Insert(0, $"Frame {Time.frameCount}: ");
            if (prefix.HasFlag(DebugLoggerPrefix.Time) || forceInfo)
                finalMessage = finalMessage.Insert(0, $"[{DateTime.Now}] ");
            return finalMessage;
        }

        private static void ConsoleLogEvent(string message)
        {
            consoleLog.Add(message);
            while (consoleLog.Count > MAX_LOG_MESSAGES)
                consoleLog.RemoveAt(0);
            EventManager.Send(new ConsoleLogEntryAddedEvent(message));
        }

        private static string InsertStackTrace(string message, bool forceInfo = false)
        {
            string finalMessage = message;
            if (trace == DebugLoggerTrace.Verbose || forceInfo)
                finalMessage = finalMessage + "\n\t" + StackTraceUtility.ExtractStackTrace();
            return finalMessage;
        }

        public static void LogError(string message)
        {
            string finalMessage = InsertFrameTimeInfo(message);
            ConsoleLogEvent(finalMessage);
            finalMessage = InsertStackTrace(finalMessage);
#if UNITY_EDITOR
            Debug.LogError(finalMessage);
#endif
            LogErrorMessage(finalMessage);
        }

        public static void LogException(string message)
        {
            string finalMessage = InsertFrameTimeInfo(message);
            ConsoleLogEvent(finalMessage);
            finalMessage = InsertStackTrace(finalMessage);
#if UNITY_EDITOR
            Debug.LogError(finalMessage);
#endif
            LogExceptionMessage(finalMessage);
        }

        private static void LogMessage(string message)
        {
            TrimLog();
            comments++;
            WriteToLog(message);
        }

        private static void LogErrorMessage(string message)
        {
            TrimLog();
            message = message.Insert(0, "(! ERROR !) ");
            errors++;
            WriteToLog(message);
        }

        private static void LogExceptionMessage(string message)
        {
            TrimLog();
            message = message.Insert(0, "(!!! EXCEPTION !!!) ");
            exceptions++;
            WriteToLog(message);
        }

        private static void TrimLog()
        {
            while (log.Count >= MAX_LOG_MESSAGES)
                log.RemoveAt(0);
        }

        private static void WriteToLog(string message) =>
            log.Add(message);

        private static StreamWriter GetLogFileWriter() => new StreamWriter(LogFilePath, true);
    
        public static void DumpToLog()
        {
            using (StreamWriter writer = GetLogFileWriter())
            {
                string startMessage = "=== NEW SESSION BEGINS ===";
                startMessage = InsertFrameTimeInfo(startMessage, true);
                writer.WriteLine(startMessage);
                writer.WriteLine("\nComments: " + comments + " | Errors: " + errors + " | Exceptions: " + exceptions);
                writer.WriteLine("\nTotal logged messages:" + (comments + errors + exceptions));
                writer.WriteLine("\n");
                for (int i = 0; i < log.Count; i++)
                {
                    string logMessage = log[i];
                    writer.WriteLine($"Message_{i}: {logMessage}");
                }
                string endMessage = "=== END OF LOG ===";
                endMessage = InsertFrameTimeInfo(endMessage, true);
                endMessage = InsertStackTrace(endMessage, true);
                writer.WriteLine(endMessage);
            }
        }
    }
}