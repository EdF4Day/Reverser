
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

        private const string BLOCK_REGEX = @"\s*File/s:(.*\r\n)+?\s*To:.*";
        private const string FILES_REGEX = @"(?<=\s*File/s:.*\r\n)(.*\r\n)*?(?=\s*IsRegex:.*)";
        private const string ISREGEX_REGEX = @"(?<=\s*IsRegex:).*";
        private const string FROM_REGEX = @"(?<=\s*From:).*";
        private const string TO_REGEX = @"(?<=\s*To:).*";

        private static readonly string[] FILE_SPLIT_LITERAL = { Environment.NewLine };
        private const string VALUE_REGEX = @":.*";

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
            MatchCollection blocks = Regex.Matches(source, BLOCK_REGEX);

            List<ContentChange> changes = new List<ContentChange>();

            foreach (Match block in blocks)
            {
                string text = block.Value;

                ContentChange change = ParseBlock(text);
                changes.Add(change);
            }

            return changes;
        }

        #endregion IReversalParser


        #region Dependencies of ParseToChanges()

        private ContentChange ParseBlock(string block)  /* verified */
        {
            ContentChange change = new ContentChange();
            change.Files = ParseFiles(block);
            change.IsRegex = ParseIsRegex(block);
            change.From = ParseFrom(block);
            change.To = ParseTo(block);

            return change;
        }

        private List<string> ParseFiles(string block)  /* verified */
        {
            Match section = Regex.Match(block, FILES_REGEX);
            string text = section.Value;
            string[] files = text.Split(FILE_SPLIT_LITERAL, StringSplitOptions.RemoveEmptyEntries);

            return files.ToList();
        }

        private bool ParseIsRegex(string block)  /* verified */
        {
            string text = Regex.Match(block, ISREGEX_REGEX)
                .Value
                .Trim();

            bool value = bool.Parse(text);
            return value;
        }

        private string ParseFrom(string block)  /* verified */
        {
            string text = Regex.Match(block, FROM_REGEX)
                .Value
                .Trim();

            return text;
        }

        private string ParseTo(string block)  /* verified */
        {
            string text = Regex.Match(block, TO_REGEX)
                .Value
                .Trim();

            return text;
        }

        #endregion Dependencies of ParseToChanges()

    }
}
