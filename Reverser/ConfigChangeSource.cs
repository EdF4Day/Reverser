
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ConfigChangeSource : IChangeSource
    {
        #region Fields

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

        public ConfigChangeSource()  /* ok */
        {
            /* No operations. */
        }

        #endregion Constructors


        #region Dependencies of .SourceText

        private string ReadSource()  /* verified */
        {
            string source = Properties.Settings.Default.Reversings;
            return source;
        }

        #endregion Dependencies of .SourceText
    }
}
