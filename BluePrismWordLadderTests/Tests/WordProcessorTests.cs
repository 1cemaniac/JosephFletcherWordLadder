using BluePrismWordLadder.Data;
using BluePrismWordLadder.Logic;
using NUnit.Framework;
using System.Collections.Generic;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class WordProcessorTests
    {
        WordProcessor ClassToTest;

        [OneTimeSetUp]
        public void Setup()
        {
            ClassToTest = new();
            ClassToTest.Results = new();
        }

        [Test]
        public void ForwardStep()
        {
            HashSet<string> wordHashSet = new() { "bord", "curd", "core", "ward", "cord", "lane", "card" };
            ClassToTest.Words = wordHashSet;
            var goalWord = "ward";
            WordNode node = new("cold",null, goalWord);
            List<WordNode> wordNodes = new() { };
            Assert.IsTrue(ClassToTest.ForwardStep(node, goalWord));
        }

        [Test]
        public void SideStep()
        {
            HashSet<string> wordHashSet = new() { "cold", "curd", "core", "ward", "cord", "lane", "card" };
            ClassToTest.Words = wordHashSet;
            var goalWord = "ward";
            WordNode node = new("bold", null, goalWord);
            List<WordNode> wordNodes = new() { };
            Assert.IsTrue(ClassToTest.SideStep(node, goalWord));
        }
        [Test]
        public void BackStep()
        {
            HashSet<string> wordHashSet = new() { "cold", "curd", "core", "ward", "cord", "lane", "card" };
            ClassToTest.Words = wordHashSet;
            var goalWord = "ward";
            WordNode node = new("wold", null, goalWord);
            List<WordNode> wordNodes = new() { };
            Assert.IsTrue(ClassToTest.BackStep(node, goalWord));
        }
    }
}
