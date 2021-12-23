
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Reverser
{
    public class FileChangeSource : IChangeSource
    {
        #region Fields

        private string _path;
        private string _sourceText;

        #endregion Fields


        #region IChangeSource

        public string SourceText  /* passed */
        {
            get
            {
                return _sourceText ?? ReadSource();
            }
            set
            {
                _sourceText = value;
            }
        }

        #endregion IChangeSource


        #region Constructors

        public FileChangeSource(string path)
        {
            _path = path;
        }

        #endregion Constructors


        #region Dependencies of .SourceText

        private string ReadSource()
        {
            if (!File.Exists(_path))
            {
                return string.Empty;
            }

            string source = File.ReadAllText(_path);
            return source;
        }

        #endregion Dependencies of .SourceText
    }
}
