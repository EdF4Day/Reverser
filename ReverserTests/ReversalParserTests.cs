
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
        #region ParseToChanges()

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
        public void ParseToChanges__MultiFileChangeBlock__CorrectOutputChangeObject()  /* working */
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
File/s:
X:\Some\Path\Reversible-A.txt
X:\Some\Other\Path\Reversible-B.txt
X:\Some\Path\Reversible-C.txt
X:\Yet\Another\Path\Of\Some\Kind\Reversible-D.txt

IsRegex: true
From: abc
To: xyz
";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expected = new ContentChange()
            {
                Files = new List<string> {
                    @"X:\Some\Path\Reversible-A.txt",
                    @"X:\Some\Other\Path\Reversible-B.txt",
                    @"X:\Some\Path\Reversible-C.txt",
                    @"X:\Yet\Another\Path\Of\Some\Kind\Reversible-D.txt"
                },
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

        [TestMethod()]
        public void ParseToChanges__TwoChangeBlocksMixedSpacing__CorrectOutputChangeObjects()  /* working */
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"File/s:


X:\Some\Path\Reversible.txt

IsRegex:           true   

From: abc
To: xyz
File/s:
X:\Some\Other\Path\AnotherReversible.txt

IsRegex: true      
From:  [qrs]{0:2}






To: abc";


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

        [TestMethod()]
        public void ParseToChanges__TwoChangeBlocksIntermixedOtherText__CorrectOutputChangeObjects()  /* working */
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"#First block:
File/s:


X:\Some\Path\Reversible.txt

IsRegex:           true   

This is the text to be found and changed:
From: abc


# This text replaces whatever is found in ""From"":
To: xyz

/* Second block: */
File/s:
X:\Some\Other\Path\AnotherReversible.txt

IsRegex: true      
From:  [qrs]{0:2}

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

        #endregion ParseToChanges()

    }
}