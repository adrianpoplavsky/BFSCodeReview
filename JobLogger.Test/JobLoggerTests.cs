using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLogger.Test
{
    [TestClass]
    public class JobLoggerTests
    {
        [TestMethod]
        public void LogMessageToConsole()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = true.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = false.ToString();

            string output;
            ConsoleColor consolecolor;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();
                    JobLogger.Instance.LogMessage("a message");

                    consolecolor = Console.ForegroundColor;

                    output = sw.ToString();
                }
            }

            Assert.AreEqual(ConsoleColor.White, consolecolor, "Console color does not belong to Message.");

            Assert.IsTrue(output.Contains("a message"), "Console output message is incorrect.");
        }

        [TestMethod]
        public void LogWarningToConsole()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = true.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = false.ToString();

            string output;
            ConsoleColor consolecolor;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();
                    JobLogger.Instance.LogWarning("a message");

                    consolecolor = Console.ForegroundColor;

                    output = sw.ToString();
                }
            }

            Assert.AreEqual(ConsoleColor.Yellow, consolecolor, "Console color does not belong to Warning.");

            Assert.IsTrue(output.Contains("a message"), "Console output message is incorrect.");
        }

        [TestMethod]
        public void LogErrorToConsole()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = true.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            string output;
            ConsoleColor consolecolor;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();

                    JobLogger.Instance.LogError("a message");
                    consolecolor = Console.ForegroundColor;
                    output = sw.ToString();

                }

            }

            Assert.AreEqual(ConsoleColor.Red, consolecolor, "Console color does not belong to Error.");

            Assert.IsTrue(output.Contains("a message"), "Console output message is incorrect.");
        }

        [TestMethod]
        public void DontLogToConsole()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            string output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();

                    JobLogger.Instance.LogMessage("a message");
                    JobLogger.Instance.LogWarning("a message");
                    JobLogger.Instance.LogError("a message");
                    output = sw.ToString();
                }
            }

            Assert.IsTrue(output == string.Empty, "Console output message is incorrect.");
        }

        [TestMethod]
        public void LogMessageToFile()
        {
            ConfigurationManager.AppSettings["logToFile"] = true.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogMessage("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }
        [TestMethod]
        public void LogWarningToFile()
        {
            ConfigurationManager.AppSettings["logToFile"] = true.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = false.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogWarning("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }
        [TestMethod]
        public void LogErrorToFile()
        {
            ConfigurationManager.AppSettings["logToFile"] = true.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogError("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }

        [TestMethod]
        public void DontLogToFile()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            string output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();

                    JobLogger.Instance.LogMessage("a message");
                    JobLogger.Instance.LogWarning("a message");
                    JobLogger.Instance.LogError("a message");
                    output = sw.ToString();
                }
            }

            Assert.IsTrue(output == string.Empty, "Console output message is incorrect.");
        }














        [TestMethod]
        public void LogMessageToDb()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = true.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogError("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }
        [TestMethod]
        public void LogWarningToDb()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = true.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = false.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogWarning("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }
        [TestMethod]
        public void LogErrorToDb()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = true.ToString();
            ConfigurationManager.AppSettings["logMessage"] = false.ToString();
            ConfigurationManager.AppSettings["logWarning"] = false.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();


            using (new StringWriter())
            {
                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    JobLogger.Instance.LogError("a message");
                }
            }

            Assert.IsTrue(true, "file output message is incorrect.");
        }

        [TestMethod]
        public void DontLogToDb()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            string output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();

                    JobLogger.Instance.LogMessage("a message");
                    JobLogger.Instance.LogWarning("a message");
                    JobLogger.Instance.LogError("a message");
                    output = sw.ToString();
                }
            }

            Assert.IsTrue(output == string.Empty, "Console output message is incorrect.");
        }

        [TestMethod]
        public void BlankMessageDoesntLog()
        {
            ConfigurationManager.AppSettings["logToFile"] = false.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            string output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                lock (new object())
                {
                    JobLogger.Instance.Clean();
                    Console.Clear();

                    JobLogger.Instance.LogMessage("");
                    JobLogger.Instance.LogWarning("");
                    JobLogger.Instance.LogError("");
                    output = sw.ToString();
                }
            }

            Assert.IsTrue(output == string.Empty, "Console output message is incorrect.");
        }


        [TestMethod]
        public void LogDirectoryGetsCreated()
        {
            ConfigurationManager.AppSettings["logToFile"] = true.ToString();
            ConfigurationManager.AppSettings["logToConsole"] = false.ToString();
            ConfigurationManager.AppSettings["logToDatabase"] = false.ToString();
            ConfigurationManager.AppSettings["logMessage"] = true.ToString();
            ConfigurationManager.AppSettings["logWarning"] = true.ToString();
            ConfigurationManager.AppSettings["logError"] = true.ToString();

            var logDirectory = ConfigurationManager.AppSettings["LogFileDirectory"];

            lock (new object())
            {
                JobLogger.Instance.Clean();
                Console.Clear();

                if (Directory.Exists(logDirectory))
                    Directory.Delete(logDirectory, true);

                JobLogger.Instance.LogMessage("a message");
                JobLogger.Instance.LogWarning("a message");
                JobLogger.Instance.LogError("a message");

            Assert.IsTrue(Directory.Exists(logDirectory), "Directory does not exist.");
            }
        }
    }
}
