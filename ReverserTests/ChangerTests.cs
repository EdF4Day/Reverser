
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
            Assert.IsNull(actual);
        }

    }
}