
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

From: abc
To: xyz
:End
";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expected = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                From = "abc",
                To = "xyz"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expected };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

        [TestMethod()]
        public void ParseToChanges__SingleChangeBlockWithMultilineChange__CorrectOutputChangeObject()  /* working */ 
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
File/s:
X:\Some\Path\Reversible.txt
Y:\Some\Path\Reversible.txt

From:abc
def
ghi

To:
rst
uvw
xyz

:End
";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expected = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt", @"Y:\Some\Path\Reversible.txt" },
                From = "abc\r\ndef\r\nghi\r\n",
                To = "\r\nrst\r\nuvw\r\nxyz\r\n"
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

From: abc
To: xyz
:End
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

From: abc
To: xyz
:End

File/s:
X:\Some\Other\Path\AnotherReversible.txt

From: [qrs]{0:2}
To: abc
:End

";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expFirst = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                From = "abc",
                To = "xyz"
            };

            ContentChange expSecond = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Other\Path\AnotherReversible.txt" },
                From = "[qrs]{0:2}",
                To = "abc"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expFirst, expSecond };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

        [TestMethod()]
        public void ParseToChanges__TwoChangeBlocksMultilineChanges__CorrectOutputChangeObjects()  /* working */ 
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
File/s:
X:\Some\Path\Reversible.txt

From:
abc
123

To:456
xyz
:End

File/s:
X:\Some\Other\Path\AnotherReversible.txt

From:[qrs]{0:2}
(?<=abc)xyz
To:
abc
123

:End

";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expFirst = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                From = "\r\nabc\r\n123\r\n",
                To = "456\r\nxyz"
            };

            ContentChange expSecond = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Other\Path\AnotherReversible.txt" },
                From = "[qrs]{0:2}\r\n(?<=abc)xyz",
                To = "\r\nabc\r\n123\r\n"
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

From: abc
To: xyz
:End
File/s:

X:\Some\Other\Path\AnotherReversible.txt
From:  [qrs]{0:2}
To: abc
:End";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expFirst = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                From = "abc",
                To = "xyz"
            };

            ContentChange expSecond = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Other\Path\AnotherReversible.txt" },
                From = "[qrs]{0:2}",
                To = "abc"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expFirst, expSecond };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

        [TestMethod()]
        public void ParseToChanges__TwoChangeBlocksCommentsBetween__CorrectOutputChangeObjects()  /* working */
        {
            //**  Arrange.  **//
            ReversalParser target = new ReversalParser();
            string source =
@"
# This initial comment should be ignored.

File/s:
X:\Some\Path\Reversible.txt

From: abc
To: xyz
:End

# These comments say something.
# Even if there are multiple lines of these,
and they don't all have leading #s, 
they should be completely ignored.

File/s:
X:\Some\Other\Path\AnotherReversible.txt

From: [qrs]{0:2}
To: abc
:End
# And this should also be ignored.
";


            //**  Act.  **//
            List<ContentChange> actuals = target.ParseToChanges(source);


            //**  Assert.  **//
            ContentChange expFirst = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Path\Reversible.txt" },
                From = "abc",
                To = "xyz"
            };

            ContentChange expSecond = new ContentChange()
            {
                Files = new List<string> { @"X:\Some\Other\Path\AnotherReversible.txt" },
                From = "[qrs]{0:2}",
                To = "abc"
            };

            List<ContentChange> expecteds = new List<ContentChange> { expFirst, expSecond };

            CollectionAssert.AreEqual(expecteds, actuals);
        }

        #endregion ParseToChanges()

    }
}