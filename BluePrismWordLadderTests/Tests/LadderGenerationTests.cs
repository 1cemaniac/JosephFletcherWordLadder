using BluePrismWordLadder.Data;
using BluePrismWordLadder.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class LadderGenerationTests
    {
        LadderGenerator ClassToTest;
        Mock<IWordProcessor> WordProcessorMock;
        Mock<IPriorityTracker> PriorityTrackerMock;

        [OneTimeSetUp]
        public void Setup()
        {
            WordProcessorMock = new();

            PriorityTrackerMock = new();
            PriorityTrackerMock.Setup(x => x.Log(It.IsAny<string>()));
            PriorityTrackerMock.Setup(x => x.Setup(It.IsAny<int>()));
            PriorityTrackerMock.Setup(x => x.Save());
            PriorityTrackerMock.Setup(x => x.Prioritise(It.IsAny<List<WordNode>>()))
                               .Returns<List<WordNode>>(x => x);

            ClassToTest = new(WordProcessorMock.Object, PriorityTrackerMock.Object);
        }

        [Test]
        public void GenerateLadderBasic()
        {
            List<WordNode> wordProcessorOutput = new();
            WordNode node1 = new("cold", null, "ward");
            wordProcessorOutput.Add(node1);
            WordNode node2 = new("cord", node1, "ward");
            wordProcessorOutput.Add(node2);
            WordNode node3 = new("card", node2, "ward");
            wordProcessorOutput.Add(node3);
            WordNode node4 = new("ward", node3, "ward");
            wordProcessorOutput.Add(node4);

            WordProcessorMock.Setup(x => x.ForwardStep(It.Is<WordNode>(wn => wn.Word == "cold"), It.Is<string>(s => s == "ward")))
                             .Returns(true);
            WordProcessorMock.Setup(x => x.Results).Returns(wordProcessorOutput);

            HashSet<string> HashSet = new() {  "cord", "card", "ward" };
            var result = ClassToTest.GenerateLadder(HashSet, "cold", "ward");
            Stack<string> ladder = new(new[] { "ward", "card", "cord", "cold" });
            Assert.AreEqual(ladder, result);
        }

        [Test]
        public void GenerateLadderComplex()
        {
            List<WordNode> wordProcessorOutput = new();
            WordNode node1 = new("cold", null, "ward");
            wordProcessorOutput.Add(node1);
            WordNode node2 = new("cord", node1, "ward");
            wordProcessorOutput.Add(node2);
            WordNode node3 = new("core", node2, "ward");
            wordProcessorOutput.Add(node3);
            WordNode node4 = new("wore", node3, "ward");
            wordProcessorOutput.Add(node4);
            WordNode node5 = new("ware", node4, "ward");
            wordProcessorOutput.Add(node5);
            WordNode node6 = new("ward", node5, "ward");
            wordProcessorOutput.Add(node6);

            List<WordNode> currentNodes = new();
            currentNodes.Add(new("cord", null, "ward"));

            WordProcessorMock.Setup(x => x.ForwardStep(It.Is<WordNode>(wn => wn.Word != "core"), It.IsAny<string>()))
                             .Returns(false);

            WordProcessorMock.Setup(x => x.SideStep(It.Is<WordNode>(wn => wn.Word == "cord"), It.Is<string>(s => s == "ward")))
                             .Returns(true);

            WordProcessorMock.Setup(x => x.SideStep(It.Is<WordNode>(wn => wn.Word != "cord"), It.IsAny<string>()))
                             .Returns(false);

            WordProcessorMock.Setup(x => x.BackStep(It.IsAny<WordNode>(), It.IsAny<string>()))
                             .Returns(false);

            WordProcessorMock.Setup(x => x.Results).Returns(wordProcessorOutput);

            HashSet<string> HashSet = new() { "cold", "cord", "core", "wore", "ware", "ward", "bath", "sore", "door" };
            var result = ClassToTest.GenerateLadder(HashSet, "cold", "ward");
            Stack<string> ladder = new(new[] { "ward", "ware", "wore", "core", "cord", "cold" });
            Assert.AreEqual(ladder, result);
        }
    }
}
