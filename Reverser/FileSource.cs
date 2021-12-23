
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

namespace Reverser
{
    public class FileSource
    {
        public List<string> FilesFromDragEvent(DragEventArgs e)
        {
            // No-files path: empty return.
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return new List<string>();
            }

            // Step-by-step progress to a list of strings, 
            // then returning any that are valid files.
            object raw = e.Data.GetData(DataFormats.FileDrop);

            if (raw is object[] items)
            {
                List<string> texts = items
                    .Select(x => x as string)
                    .ToList();

                return FilesFromTexts(texts);
            }

            // Not-an-object[] path: empty return.
            return new List<string>();
        }

        public List<string> FilesFromOpeningArgs()
        {
            List<string> realArgs = Environment.GetCommandLineArgs()
                .Skip(1)
                .ToList();

            return FilesFromTexts(realArgs);
        }

        private List<string> FilesFromTexts(List<string> texts)
        {
            // .Exists() is basically failproof, 
            // so Linq can be used safely.
            List<string> files = texts
                .Where(x => File.Exists(x))
                .ToList();
            
            return files;
        }
    }
}
