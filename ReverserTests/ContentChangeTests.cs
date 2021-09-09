
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
    public class ContentChangeTests
    {
        [TestMethod()]
        public void Equals__NotAContentChange__ReturnsFalse()  /* working */
        {
            //**  Arrange.  **//
            ContentChange left = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                From = "abc zyx",
                To = "xyz cba"
            };

            object right = new StringBuilder();


            //**  Act.  **//
            bool actual = left.Equals(right);


            //**  Assert.  **//
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Equals__DifferentContents__ReturnsFalse()  /* working */ 
        {
            //**  Arrange.  **//
            ContentChange left = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                From = "abc zyx",
                To = "xyz cba"
            };

            ContentChange right = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },

                // These are the test condition.
                From = "abcd zyxw",
                To = "wxyz dcba"
            };


            //**  Act.  **//
            bool actual = left.Equals(right);


            //**  Assert.  **//
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Equals__SameContents__ReturnsTrue()  /* working */
        {
            //**  Arrange.  **//
            ContentChange left = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                From = "abc zyx",
                To = "xyz cba"
            };

            ContentChange right = new ContentChange
            {
                Files = new List<string> { "a", "b", "t" },
                From = "abc zyx",
                To = "xyz cba"
            };


            //**  Act.  **//
            bool actual = left.Equals(right);


            //**  Assert.  **//
            Assert.AreEqual(true, actual);
        }

    }
}