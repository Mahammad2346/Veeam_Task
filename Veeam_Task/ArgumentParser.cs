using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veeam_Task
{

    public static class ArgumentParser
    {
        public static AppConfig? Parse(string[] args)
        {
            var config = new AppConfig();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--source":
                        config.SourcePath = GetArgValue(args, ref i);
                        break;
                    case "--replica":
                        config.ReplicaPath = GetArgValue(args, ref i);
                        break;
                    case "--log":
                        config.LogPath = GetArgValue(args, ref i);
                        break;
                    case "--interval":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int val) && val > 0)
                        {
                            config.IntervalSeconds = val;
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: --interval must be a positive number");
                            return null;
                        }
                        break;
                }
            }

            if (string.IsNullOrWhiteSpace(config.SourcePath) ||
                string.IsNullOrWhiteSpace(config.ReplicaPath) ||
                string.IsNullOrWhiteSpace(config.LogPath) ||
                config.IntervalSeconds <= 0)
            {
                Console.WriteLine("ERROR: Missing or invalid arguments.\n");
                PrintUsage();
                return null;
            }

            return config;
        }

        private static string GetArgValue(string[] args, ref int i)
        {
            if (i + 1 < args.Length)
            {
                i++;
                return args[i];
            }

            return string.Empty;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  FolderSync --source \"<path>\" --replica \"<path>\" --interval <seconds> --log \"<path>\"");
            Console.WriteLine("Example:");
            Console.WriteLine("  FolderSync --source \"C:\\Test\\src\" --replica \"C:\\Test\\replica\" --interval 30 --log \"C:\\Logs\\sync.log\"");
        }
    }
}
