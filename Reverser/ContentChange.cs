
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ContentChange
    {
        #region Properties

        public List<string> Files { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        #endregion Properties


        #region Constructors

        public ContentChange()  /* ok */
        {
            /* No operations. */
        }

        public ContentChange(List<string> files, string from, string to)  /* ok */
        {
            Files = files;
            From = from;
            To = to;
        }

        #endregion Constructors


        #region Overrides ( Equals() )

        public override bool Equals(object obj)  /* passed */
        {
            // Is a ContentChange.  Objects equal if all values equal.
            if (obj is ContentChange that)
            {
                bool filesAreEqual = this.Files.SequenceEqual(that.Files);

                return filesAreEqual
                    && this.From == that.From
                    && this.To == that.To
                    ;
            }

            // Not a ContentChange.
            return false;
        }

        #endregion Overrides ( Equals() )
    }
}
