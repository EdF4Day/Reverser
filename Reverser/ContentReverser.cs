
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverser
{
    public class ContentReverser
    {
        ChangeSource _source;
        ReversalParser _parser;
        Changer _changer;

        public ContentReverser()
        {
            _source = new ChangeSource();
            _parser = new ReversalParser();
            _changer = new Changer();
        }

        public void ChangeAllForward()
        {
            string source = _source.SourceText;
            List<ContentChange> changes = _parser.ParseToChanges(source);

            foreach (ContentChange change in changes)
            {
                _changer.ChangeForward(change);
            }
        }

        public void ChangeAllBack()
        {
            string source = _source.SourceText;
            List<ContentChange> changes = _parser.ParseToChanges(source);

            foreach (ContentChange change in changes)
            {
                _changer.ChangeBack(change);
            }
        }
    }
}
