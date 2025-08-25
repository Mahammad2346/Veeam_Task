using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veeam_Task
{
    public class Logger : IDisposable
    {
        private StreamWriter _logWriter;

        public Logger(string logPath)
        {
            _logWriter = new StreamWriter(logPath, append: true);
        }

        public void Log(string message)
        {
            string timestamped = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine(timestamped);
            _logWriter.WriteLine(timestamped);
        }

        public void Dispose()
        {
            _logWriter?.Dispose();
        }
    }
}
