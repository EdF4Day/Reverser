
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
    public class FileDuple
    {
        public string FullPath { get; set; }
        public string FriendlyName { get; set; }

        public FileDuple(string path)
        {
            FullPath = path;
            FriendlyName = Path.GetFileName(path);
        }

        public static List<FileDuple> InstancesFromPaths(List<string> paths)
        {
            List<FileDuple> instances = new List<FileDuple>();

            foreach (string path in paths)
            {
                FileDuple instance = new FileDuple(path);
                instances.Add(instance);
            }

            return instances;
        }
    }
}
