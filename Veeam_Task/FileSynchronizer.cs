using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veeam_Task
{
    public class FileSynchronizer
    {
        private readonly Logger _logger;

        public FileSynchronizer(Logger logger)
        {
            _logger = logger;
        }

        public void Sync(string sourcePath, string replicaPath)
        {
            string[] sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);

            foreach (string sourceFilePath in sourceFiles)
            {
                string relativePath = Path.GetRelativePath(sourcePath, sourceFilePath);
                string targetFilePath = Path.Combine(replicaPath, relativePath);

                Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath)!);

                if (!File.Exists(targetFilePath))
                {
                    File.Copy(sourceFilePath, targetFilePath);
                    _logger.Log("Copied (new): " + targetFilePath);
                }
                else
                {
                    DateTime sourceTime = File.GetLastWriteTimeUtc(sourceFilePath);
                    DateTime targetTime = File.GetLastWriteTimeUtc(targetFilePath);

                    if (sourceTime > targetTime)
                    {
                        File.Copy(sourceFilePath, targetFilePath, overwrite: true);
                        _logger.Log("Updated (overwrite): " + targetFilePath);
                    }
                }
            }

            string[] replicaFiles = Directory.GetFiles(replicaPath, "*", SearchOption.AllDirectories);
            foreach (string replicaFilePath in replicaFiles)
            {
                string relativePath = Path.GetRelativePath(replicaPath, replicaFilePath);
                string correspondingSource = Path.Combine(sourcePath, relativePath);

                if (!File.Exists(correspondingSource))
                {
                    File.Delete(replicaFilePath);
                    _logger.Log("Deleted from replica: " + replicaFilePath);
                }
            }
        }
    }
}
