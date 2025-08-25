using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veeam_Task
{
    public class AppConfig
    {
        public string SourcePath { get; set; } = "";
        public string ReplicaPath { get; set; } = "";
        public string LogPath { get; set; } = "";
        public int IntervalSeconds { get; set; }
    }
}
