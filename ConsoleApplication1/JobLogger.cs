using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace JobLogger
{
    public class JobLogger
    {
        const string ConstInsertlog = "Insert into Log(message, category) Values('{0}', {1})";

        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;

        private static JobLogger _instance;

        public static JobLogger Instance
        {
            get { return _instance ?? (_instance = new JobLogger()); }
        }


        private JobLogger()
        {
            var logToFile = Convert.ToBoolean(ConfigurationManager.AppSettings["logToFile"]);
            var logToConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["logToConsole"]);
            var logToDatabase = Convert.ToBoolean(ConfigurationManager.AppSettings["logToDatabase"]);
            var logMessage = Convert.ToBoolean(ConfigurationManager.AppSettings["logMessage"]);
            var logWarning = Convert.ToBoolean(ConfigurationManager.AppSettings["logWarning"]);
            var logError = Convert.ToBoolean(ConfigurationManager.AppSettings["logError"]);

            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        private enum ItemType
        {
            Message = 1,
            Warning = 2,
            Error = 3
        }


        public void LogMessage(string strmessage)
        {
            if (_logMessage)
                LogItem(strmessage, ItemType.Message);
        }

        public void LogWarning(string strmessage)
        {
            if (_logWarning)
                LogItem(strmessage, ItemType.Warning);
        }


        public void LogError(string strmessage)
        {
            if (_logError)
                LogItem(strmessage, ItemType.Error);
        }

        public void Clean()
        {
            _instance = null;
        }

        static void LogItem(string strmessage, ItemType itemType)
        {
            if (string.IsNullOrWhiteSpace(strmessage))
            {
                return;
            }

            strmessage = strmessage.Trim();

            if (_logToDatabase)
            {
                LogToDatabase(strmessage, itemType);
            }

            if (_logToFile)
            {
                LogToFile(strmessage);
            }

            if (_logToConsole)
            {
                LogToConsole(strmessage, itemType);
            }
        }

        private static void LogToConsole(string strmessage, ItemType itemType)
        {
            var color = ConsoleColor.White;

            switch (itemType)
            {
                case ItemType.Message:
                    color = ConsoleColor.White;
                    break;
                case ItemType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case ItemType.Error:
                    color = ConsoleColor.Red;
                    break;
            }

            Console.ForegroundColor = color;

            Console.WriteLine("{0}\t{1}", DateTime.Now.ToShortDateString(), strmessage);
        }

        private static void LogToFile(string strmessage)
        {
            var l = DateTime.Now.ToShortDateString() + "\t" + strmessage;
            var logDirectory = ConfigurationManager.AppSettings["LogFileDirectory"];
            var logFile = Path.Combine(logDirectory, "LogFile_" + DateTime.Now.ToString("yy-MM-dd") + ".txt");
            if (!File.Exists(logFile))
            {
                Directory.CreateDirectory(logDirectory);
            }

            File.AppendAllText(logFile, l);
        }

        private static void LogToDatabase(string strmessage, ItemType itemType)
        {
            var cmd = string.Format(ConstInsertlog, strmessage, Convert.ToInt16(itemType));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogDatabaseConnectionString"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(cmd, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

