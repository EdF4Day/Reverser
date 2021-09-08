
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Reverser
{
    public class FileStore : IFileStore
    {
        public bool Exists(string file)
        {
            return File.Exists(file);
        }

        public string Read(string file)
        {
            return File.ReadAllText(file);
        }

        public void Write(string file, string content)
        {
            File.WriteAllText(file, content);
        }
    }
}
