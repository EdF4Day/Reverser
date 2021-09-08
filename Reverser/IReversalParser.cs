using System.Collections.Generic;

namespace Reverser
{
    public interface IReversalParser
    {
        List<ContentChange> ParseToChanges(string source);
    }
}