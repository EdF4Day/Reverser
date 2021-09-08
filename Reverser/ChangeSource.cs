
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ChangeSource : IChangeSource
    {
        private string _sourceText;
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

        public ChangeSource()  /* ok */
        {
            /* No operations. */
        }

        private string ReadSource()  /* verified */
        {
            string source = Properties.Settings.Default.Reversings;
            return source;
        }
    }
}
