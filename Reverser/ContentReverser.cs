
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
        IChangeSource _source;
        IReversalParser _parser;
        IChanger _changer;

        public ContentReverser(IChangeSource source = null, IReversalParser parser = null, IChanger changer = null)
        {
            _source = source ?? new ChangeSource();
            _parser = parser ?? new ReversalParser();
            _changer = changer ?? new Changer();
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
