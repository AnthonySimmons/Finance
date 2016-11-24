

using System;
using System.IO;

namespace Logger
{
    public class Logs
    {
        private Logs()
        {

        }

        private readonly static object _lock = new object();
        private static Logs _instance;

        public static Logs Instance
        {
            get
            {
                lock(_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new Logs();
                    }
                }
                return _instance;
            }
        }

        public static string LogFilePath
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, "Log.txt");
            }
        }

        public void Log(string message)
        {
            Console.Out.WriteLine(message);
            File.AppendAllText(LogFilePath, $"{DateTime.Now.ToString()}{Environment.NewLine}{message}{Environment.NewLine}");
        }

        public void Log(Exception ex)
        {
            string message = ex?.Message;
            message += Environment.NewLine + ex?.InnerException?.Message;
            Log(message);
        }

    }
}
