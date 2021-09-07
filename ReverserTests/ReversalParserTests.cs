
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
        [TestMethod()]
        public void Experiment__ContentChange__SameContents__AreEqual()  /* working */ 
        {
            //**  Arrange.  **//
            ContentChange left = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                IsRegex = true,
                From = "abc zyx",
                To = "xyz cba"
            };

            ContentChange right = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                IsRegex = true,
                From = "abc zyx",
                To = "xyz cba"
            };


            //**  Act.  **//


            //**  Assert.  **//
            Assert.AreEqual(left, right);
        }


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

            CollectionAssert.AreEquivalent(expecteds, actuals);
        }

    }
}