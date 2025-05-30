using System;
using System.IO;

namespace CoolRetroPowershellTerm
{
    public static class Logger
    {
        private static readonly string LogFilePath = "CoolRetroPowershellTerm.log";
        private static readonly object LockObj = new object();

        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public static void Error(string message, Exception? ex = null)
        {
            WriteLog("ERROR", message + (ex != null ? $"\nException: {ex}" : ""));
        }

        private static void WriteLog(string level, string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            lock (LockObj)
            {
                Console.WriteLine(logEntry);
                try
                {
                    File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
                }
                catch { /* Ignore file write errors */ }
            }
        }
    }
}
