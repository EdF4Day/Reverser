
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Reverser
{
    /// <summary>
    /// A DTO that keeps a full path of a file and a friendly name for it together.
    /// Encapsulates the problem of keeping these together for display and file reading.
    /// </summary>
    public class FileDuples
    {
        public string FullPath { get; set; }
        public string FriendlyName { get; set; }

        public FileDuples(string path)
        {
            FullPath = path;
            FriendlyName = Path.GetFileName(path);
        }

        public static List<FileDuples> InstancesFromPaths(List<string> paths)
        {
            List<FileDuples> instances = new List<FileDuples>();

            foreach (string path in paths)
            {
                FileDuples instance = new FileDuples(path);
                instances.Add(instance);
            }

            return instances;
        }
    }
}
