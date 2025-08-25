using System;
using System.Threading;
using Veeam_Task;

class Program
{
    static int Main(string[] args)
    {
        var config = ArgumentParser.Parse(args);
        if (config == null)
        {
            return 1;
        }

        while (true)
        {
            try
            {
                using var logger = new Logger(config.LogPath);
                var synchronizer = new FileSynchronizer(logger);
                synchronizer.Sync(config.SourcePath, config.ReplicaPath);
                logger.Log("Sync completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }

            Thread.Sleep(config.IntervalSeconds * 1000);
        }
    }
}
