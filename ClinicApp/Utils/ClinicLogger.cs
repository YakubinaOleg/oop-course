using System.Text;

namespace ClinicApp.Utils
{
    public class ClinicLogger
    {
        private readonly string _logPath;

        public ClinicLogger(string logPath = "clinic.log")
        {
            _logPath = logPath;
        }

        public void LogInfo(string message)
        {
            Write("INFO ", message);
        }

        public void LogWarning(string message)
        {
            Write("WARN ", message);
        }

        public void LogError(string message)
        {
            Write("ERROR", message);
        }

        public string[] GetLastLines(int n)
        {
            if (!File.Exists(_logPath) || n <= 0)
                return new string[0];

            string[] all = File.ReadAllLines(_logPath, Encoding.UTF8);
            int skip = all.Length > n ? all.Length - n : 0;

            string[] result = new string[all.Length - skip];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = all[skip + i];
            }
            return result;
        }

        public void Clear()
        {
            if (File.Exists(_logPath))
                File.Delete(_logPath);
        }

        public bool Exists()
        {
            return File.Exists(_logPath);
        }

        private void Write(string level, string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";
            File.AppendAllText(_logPath, line, Encoding.UTF8);
        }
    }
}