using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ContentChange
    {
        List<string> Files { get; set; }
        bool IsRegex { get; set; }
        string From { get; set; }
        string To { get; set; }
    }
}
