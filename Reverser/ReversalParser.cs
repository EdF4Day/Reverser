
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reverser
{
    public class ReversalParser : IReversalParser
    {
        #region Definitions

        // A block is everything from a `File/s:` through an `:End`.
        private const string BLOCK_REGEX = @"(?<=(\A|\n))File/s:.*?:End";

        // The files are everything after the `File/s:` but before the `From:`.
        private const string FILES_REGEX = @"(?<=File/s:.*\r\n).*?(?=\r\nFrom:.*)";

        // The from is everything between `From:` and `To:`, trimmed if one line.
        private const string FROM_REGEX = @"(?<=\s*From:).*?(?=\r\nTo:)";

        // The to is everything between `To:` and `:End`, trimmed if one line.
        private const string TO_REGEX = @"(?<=\s*To:).*(?=\r\n:End)";

        private static readonly string[] LINE_SPLIT_LITERAL = { Environment.NewLine };

        #endregion Definitions


        #region Constructors

        public ReversalParser()
        {
            /* No operations. */
        }

        #endregion Constructors


        #region IReversalParser

        public List<ContentChange> ParseToChanges(string source)  /* passed */
        {
            try
            {
                MatchCollection blocks = Regex.Matches(source, BLOCK_REGEX, RegexOptions.Singleline);

                List<ContentChange> changes = new List<ContentChange>();

                foreach (Match block in blocks)
                {
                    string text = block.Value;

                    ContentChange change = ParseBlock(text);
                    changes.Add(change);
                }

                return changes;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }

            return null;
        }

        #endregion IReversalParser


        #region Dependencies of ParseToChanges()

        private ContentChange ParseBlock(string block)  /* verified */
        {
            ContentChange change = new ContentChange();
            change.Files = ParseFiles(block);
            change.From = ParseFrom(block);
            change.To = ParseTo(block);

            return change;
        }

        private List<string> ParseFiles(string block)  /* verified */
        {
            Match section = Regex.Match(block, FILES_REGEX, RegexOptions.Singleline);
            string text = section.Value;
            string[] files = text.Split(LINE_SPLIT_LITERAL, StringSplitOptions.RemoveEmptyEntries);

            return files.ToList();
        }

        private string ParseFrom(string block)  /* verified */
        {
            string text = Regex.Match(block, FROM_REGEX, RegexOptions.Singleline)
                .Value;

            text = SimplifyIfSingleLine(text);

            return text;
        }

        private string ParseTo(string block)  /* verified */
        {
            string text = Regex.Match(block, TO_REGEX, RegexOptions.Singleline)
                .Value;

            text = SimplifyIfSingleLine(text);

            return text;
        }

        private string SimplifyIfSingleLine(string text)  /* verified */ 
        {
            if (!text.Contains(Environment.NewLine))
            {
                text = text.Trim();
            }

            return text;
        }

        #endregion Dependencies of ParseToChanges()

    }
}
