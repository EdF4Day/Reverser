﻿
#pragma warning disable

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reverser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

namespace ReverserTests
{
    [TestClass()]
    public class ChangerTests
    {
        #region ChangeForward()

        [TestMethod()]
        public void ChangeForward__NoSuchFile__DoesNotFail()  /* working */
        {
            //**  Arrange.  **//
            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();
            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(false);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },
                IsRegex = false,
                From = "x",
                To = "y"
            };

            Exception actual = null;


            //**  Act.  **//
            try
            {
                target.ChangeForward(change);
            }
            catch (Exception thrown)
            {
                actual = thrown;
            }


            //**  Assert.  **//
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void ChangeForward__SingleFileNotRegex__SwapsFromForTo()  /* working */ 
        {
            //**  Arrange.  **//
            string file = "Some-File";
            string input = "a b c d a b c d a";

            string expected = "z b c d z b c d z";
            string actual = null;

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => actual = text);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },
                IsRegex = false,
                From = "a",
                To = "z"
            };


            //**  Act.  **//
            target.ChangeForward(change);


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ChangeForward__SingleFileIsRegex__SwapsFromForTo()  /* working */ 
        {
            //**  Arrange.  **//
            string file = "Some-File";
            string input = "a b c d a b c d";

            string expected = "#-# #-# #-# #-#";
            string actual = null;

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => actual = text);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },

                // The test condition.
                IsRegex = true,

                From = @"\w\s\w",
                To = "#-#"
            };


            //**  Act.  **//
            target.ChangeForward(change);


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ChangeForward__TwoFilesNotRegex__SwapsInBoth()  /* working */ 
        {
            //**  Arrange.  **//
            string file = "Some-File";
            List<string> inputs = new List<string> { "! a b c d a b c d a", "!! a b c d a b c d a" };

            List<string> expecteds = new List<string> { "! z b c d z b c d z", "!! z b c d z b c d z" };
            List<string> actuals = new List<string>();

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns<string>((_) => { string input = inputs[0]; inputs.RemoveAt(0); return input; });

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => { actuals.Add(text); });

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File-A.txt", @"X:\No\Such\File-B.txt" },
                IsRegex = false,
                From = "a",
                To = "z"
            };


            //**  Act.  **//
            target.ChangeForward(change);


            //**  Assert.  **//
            CollectionAssert.AreEqual(expecteds, actuals);
        }

        #endregion ChangeForward()


        #region ChangeBack()

        [TestMethod()]
        public void ChangeBack__NoSuchFile__DoesNotFail()  /* working */
        {
            //**  Arrange.  **//
            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();
            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(false);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },
                IsRegex = false,
                From = "x",
                To = "y"
            };

            Exception actual = null;


            //**  Act.  **//
            try
            {
                target.ChangeBack(change);
            }
            catch (Exception thrown)
            {
                actual = thrown;
            }


            //**  Assert.  **//
            Assert.AreEqual(null, actual);
        }

        [TestMethod()]
        public void ChangeBack__SingleFileNotRegex__SwapsToForFrom()  /* working */ 
        {
            //**  Arrange.  **//
            string file = "Some-File";
            string input = "z b c d z b c d z";

            string expected = "a b c d a b c d a";
            string actual = null;

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => actual = text);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },
                IsRegex = false,

                // These should be used in reverse.
                From = "a",
                To = "z"
            };


            //**  Act.  **//
            target.ChangeBack(change);


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ChangeBack__SingleFileIsRegex__SwapsFromForTo()  /* working */ 
        {
            /* This is not a realistic use of regex for reversing, 
             * but proves that the code path itself works right. */

            //**  Arrange.  **//
            string file = "Some-File";
            string input = "a b c d a b c d";

            string expected = "#-# #-# #-# #-#";
            string actual = null;

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => actual = text);

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File\Or\Even\Path\Exists\At\All.txt" },

                // The test condition.
                IsRegex = true,

                // These values are reversed to be usable
                // with regex in the reverse direction.
                From = "#-#" ,
                To = @"\w\s\w"
            };


            //**  Act.  **//
            target.ChangeBack(change);


            //**  Assert.  **//
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ChangeBack__TwoFilesNotRegex__SwapsInBoth()  /* working */ 
        {
            //**  Arrange.  **//
            string file = "Some-File";
            List<string> inputs = new List<string> { "! z b c d z b c d z", "!! z b c d z b c d z" };

            List<string> expecteds = new List<string> { "! a b c d a b c d a", "!! a b c d a b c d a" };
            List<string> actuals = new List<string>();

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns<string>((_) => { string input = inputs[0]; inputs.RemoveAt(0); return input; });

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((_, text) => { actuals.Add(text); });

            Changer target = new Changer(_storeMocker.Object);

            ContentChange change = new ContentChange
            {
                Files = new List<string> { @"X:\No\Such\File-A.txt", @"X:\No\Such\File-B.txt" },
                IsRegex = false,
                From = "a",
                To = "z"
            };


            //**  Act.  **//
            target.ChangeBack(change);


            //**  Assert.  **//
            CollectionAssert.AreEqual(expecteds, actuals);
        }

        #endregion ChangeBack()

    }
}