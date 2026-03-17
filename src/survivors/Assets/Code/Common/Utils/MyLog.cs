using System;
using System.IO;
using UnityEngine;

namespace Code.Common.Utils
{
    public static class MyLog
    {
        private const string LogFolder = "Assets/log~";

        private static readonly string SessionTimestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");

        private static string DebugFile => $"debugLog_{SessionTimestamp}.txt";
        private static string WarningFile => $"warningLog_{SessionTimestamp}.txt";
        private static string ErrorFile => $"errorLog_{SessionTimestamp}.txt";

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Info(string message, bool writeToFile = false)
        {
            Debug.Log(message);
            if (writeToFile)
                WriteToFile(DebugFile, message);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Warning(string message, bool writeToFile = false)
        {
            Debug.LogWarning(message);
            if (writeToFile)
                WriteToFile(WarningFile, message);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Error(string message, bool writeToFile = false)
        {
            Debug.LogError(message);
            if (writeToFile)
                WriteToFile(ErrorFile, message);
        }

        private static void WriteToFile(string fileName, string message)
        {
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);

            string path = Path.Combine(LogFolder, fileName);
            string line = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(path, line);
        }
    }
}