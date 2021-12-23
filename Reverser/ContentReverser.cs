
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
        #region Components

        private IReversalParser _parser;
        private IChanger _changer;

        #endregion Components


        #region Properties

        public List<IChangeSource> ChangeSources { get; set; } = new List<IChangeSource>();

        #endregion Properties


        #region Constructors

        public ContentReverser(List<IChangeSource> sources = null, IReversalParser parser = null, IChanger changer = null)
        {
            ChangeSources = sources ?? new List<IChangeSource> { new ConfigChangeSource() };
            _parser = parser ?? new ReversalParser();
            _changer = changer ?? new Changer();
        }

        #endregion Constructors


        #region Key methods

        public void ChangeAllForward()
        {
            foreach (IChangeSource changeSource in ChangeSources)
            {
                string source = changeSource.SourceText;
                List<ContentChange> changes = _parser.ParseToChanges(source);

                foreach (ContentChange change in changes)
                {
                    _changer.ChangeForward(change);
                }
            }
        }

        public void ChangeAllBack()
        {
            foreach (IChangeSource changeSource in ChangeSources)
            {
                string source = changeSource.SourceText;
                List<ContentChange> changes = _parser.ParseToChanges(source);

                foreach (ContentChange change in changes)
                {
                    _changer.ChangeBack(change);
                }
            }
        }

        #endregion Key methods
    }
}
