
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
    public class ContentReverserTests
    {
        [TestMethod()]
        public void ChangeAllForward__RepresentativeMaterial__CorrectCallsAndPassingsMade()
        {
            //**  Arrange.  **//
            Mock<IChangeSource> _sourceMocker = new Mock<IChangeSource>();
            Mock<IReversalParser> _parseMocker = new Mock<IReversalParser>();
            Mock<IChanger> _changeMocker = new Mock<IChanger>();

            _sourceMocker.Setup(x => x.SourceText)
                .Returns("source-text");

            ContentChange changeOne = new ContentChange(null, true, "from-1", "to-1");
            ContentChange changeTwo = new ContentChange(null, true, "from-2", "to-2");

            List<ContentChange> expecteds = new List<ContentChange> { changeOne, changeTwo };
            List<ContentChange> actuals = new List<ContentChange>();

            _parseMocker.Setup(x => x.ParseToChanges(It.Is<string>(text => text == "source-text")))
                .Returns(new List<ContentChange> { changeOne, changeTwo })
                .Verifiable();

            _changeMocker.Setup(x => x.ChangeForward(It.IsAny<ContentChange>()))
                .Callback<ContentChange>(arg => actuals.Add(arg));

            ContentReverser target = new ContentReverser(
                _sourceMocker.Object,
                _parseMocker.Object,
                _changeMocker.Object
            );


            //**  Act.  **//
            target.ChangeAllForward();


            //**  Assert.  **//
            _parseMocker.Verify();
            CollectionAssert.AreEqual(expecteds, actuals);
        }
    }
}