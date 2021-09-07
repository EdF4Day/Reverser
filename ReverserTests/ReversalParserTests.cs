
#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reverser;

namespace ReverserTests
{
    [TestClass()]
    public class ReversalParserTests
    {
        /* parsing multiple files in one block */
        /* parsing two blocks */
        /* parsing blocks with white space amid them */
        /* parsing blocks with other text around and amid them */

        [TestMethod()]
        public void ParseToChanges__KnownSingleChangeBlock__CorrectOutputChangeObject()  /* working */ 
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
File/s:
X:\Some\Path\Reversible.txt

IsRegex: true
From: abc
To: xyz
";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expected = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                IsRegex = true,
                From = "abc",
                To = "xyz"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expected };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

        [TestMethod()]
        public void ParseToChanges__TwoChangeBlocks__CorrectOutputChangeObjects()  /* working */ 
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
File/s:
X:\Some\Path\Reversible.txt

IsRegex: true
From: abc
To: xyz

File/s:
X:\Some\Other\Path\AnotherReversible.txt

IsRegex: true
From: [qrs]{0:2}
To: abc

";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expFirst = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                IsRegex = true,
                From = "abc",
                To = "xyz"
            };

            ContentChange expSecond = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Other\Path\AnotherReversible.txt" },
                IsRegex = true,
                From = "[qrs]{0:2}",
                To = "abc"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expFirst, expSecond };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

    }
}