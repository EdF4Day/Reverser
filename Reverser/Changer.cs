﻿
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reverser
{
    public class Changer : IChanger
    {
        #region Fields

        private IFileStore _store;

        #endregion Fields


        #region Constructors

        public Changer(IFileStore store = null)  /* verified */
        {
            _store = store ?? new FileStore();
        }

        #endregion Constructors


        #region IChanger

        public void ChangeForward(ContentChange change)  /* passed */
        {
            foreach (string file in change.Files)
            {
                Change(file, change.From, change.To, change.IsRegex);
            }
        }

        public void ChangeBack(ContentChange change)  /* passed */
        {
            foreach (string file in change.Files)
            {
                /* .From and .To reversed to change back. */
                Change(file, change.To, change.From, change.IsRegex);
            }
        }

        #endregion IChanger


        #region Shared dependencies

        private void Change(string file, string from, string to, bool isRegex)  /* verified */
        {
            if (!_store.Exists(file))
            {
                return;
            }

            string content = _store.Read(file);

            if (isRegex)
            {
                content = Regex.Replace(content, from, to);
            }
            else
            {
                content = content.Replace(from, to);
            }

            _store.Write(file, content);
        }

        #endregion Shared dependencies
    }
}
