
#pragma warning disable

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reverser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverserTests
{
    [TestClass()]
    public class ChangeSourceTests
    {
        #region .SourceText

        [TestMethod()]
        public void SourceText__ValueSet__SameValueReturned()  /* working */
        {
            //**  Arrange.  **//
            ChangeSource target = new ChangeSource();

            string expected = "Expected-source-text";


            //**  Act.  **//
            target.SourceText = expected;

            string actual = target.SourceText;


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SourceText__ValueNotSet__SettingsValueReturned()  /* working */
        {
            //**  Arrange.  **//
            ChangeSource target = new ChangeSource();

            // This is the design-time value set in the model code's settings.
            string expected =
@"
File/s:
C:\Users\Ed\Desktop\Reversible.txt

From: abc
To: xyz
";


            //**  Act.  **//
            string actual = target.SourceText;


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        #endregion .SourceText

    }
}