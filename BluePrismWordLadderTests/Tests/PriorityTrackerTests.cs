using BluePrismWordLadder.Data;
using BluePrismWordLadder.Logic;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class PriorityTrackerTests
    {
        PriorityTracker ClassToTest;
        Mock<IResultOutputter> ResultOutputterMock;
        Mock<IDataLoader> DataLoaderMock;

        [OneTimeSetUp]
        public void Setup()
        {
            DataLoaderMock = new();
            Dictionary<char,int> data = new()
            {
                { 'a', 1 },
                { 'b', 3 },
                { 'c', 0 }
            };
            DataLoaderMock.Setup(x => x.LoadPriorityTracker(It.IsAny<string>()))
                          .Returns(data);
            DataLoaderMock.Setup(x => x.LoadPriorityTracker(It.Is<string>(s => s.Contains("2"))))
                          .Returns(() => { return null; });

            ResultOutputterMock = new();
            ResultOutputterMock.Setup(x => x.Output(It.IsAny<IEnumerable<string>>(),It.IsAny<string>()))
                               .Returns(true);

            ClassToTest = new(DataLoaderMock.Object, ResultOutputterMock.Object);
        }

        [Test]
        public void PrioritiseCheckFreshSet()
        {
            List<WordNode> wordNodes = new()
            {
                new("a",null,"z"),
                new("b", null, "z"),
                new("c", null, "z"),
            };

            ClassToTest.Setup(2);
            var result = ClassToTest.Prioritise(wordNodes);

            Assert.AreEqual(wordNodes, result);
        }

        [Test]
        public void PrioritiseCheckPremadeSet()
        {
            List<WordNode> wordNodes = new()
            {
                new("a", null, "z"),
                new("b", null, "z"),
                new("c", null, "z"),
            };

            ClassToTest.Setup(1);
            var result = ClassToTest.Prioritise(wordNodes);

            List<string> expected = new()
            {
                "b",
                "a",
                "c"
            };

            DataLoaderMock.Verify(dlm => dlm.LoadPriorityTracker(It.Is<string>(s => s.Contains("1"))));
            Assert.AreEqual(expected, result.Select(r => r.Word).ToList());
        }

        [Test]
        public void LogTest()
        {
            ClassToTest.Setup(1);

            List<string> expected = new()
            {
                "a:2",
                "b:4",
                "c:1"
            };

            ClassToTest.Log("abc");
            ClassToTest.Save();

            ResultOutputterMock.Verify(dlm => dlm.Output(It.Is<IEnumerable<string>>(s => expected.All(e => s.Contains(e))),It.IsAny<string>()));
        }
    }
}
