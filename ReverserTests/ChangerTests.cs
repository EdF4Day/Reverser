
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
            string output = "z b c d z b c d z";

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            // Test condition.
            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.Is<string>(arg => arg == output)))
                .Verifiable();

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
            _storeMocker.Verify();
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

            // Test condition: "a" to "z" is reversed, from "z" to "a".
            string input = "z b c d z b c d z";
            string output = "a b c d a b c d a";

            Mock<IFileStore> _storeMocker = new Mock<IFileStore>();

            _storeMocker.Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);
            _storeMocker.Setup(x => x.Read(It.IsAny<string>()))
              .Returns(input);

            _storeMocker.Setup(x => x.Write(It.IsAny<string>(), It.Is<string>(arg => arg == output)))
                .Verifiable();

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
            _storeMocker.Verify();
        }

        #endregion ChangeBack()

    }
}