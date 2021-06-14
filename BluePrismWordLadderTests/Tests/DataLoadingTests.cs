using BluePrismWordLadder.Logic;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class DataLoadingTests
    {
        DataLoader ClassToTest;

        [OneTimeSetUp]
        public void Setup()
        {
            ClassToTest = new();
        }

        [Test]
        public void LoadDictionaryWordsWithoutErrors()
        {
            var result = ClassToTest.LoadDictionaryWords($"{Directory.GetCurrentDirectory()}\\TestFiles\\ValidAndInvalidFourLettersOnly.txt");
            Assert.IsNotNull(result);
        }

        [TestCase("InvalidOnly", ExpectedResult = 0)]
        [TestCase("ValidAndInvalidFourLettersOnly", ExpectedResult = 5)]
        [TestCase("ValidAndInvalidVaryingLength", ExpectedResult = 6)]
        [TestCase("OnlyLinkWords", ExpectedResult = 4)]
        [TestCase("LinkWordsWithExtra", ExpectedResult = 7)]
        public int LoadDictionaryWordsCorrectAmountOfWords(string file)
        {
            var result = ClassToTest.LoadDictionaryWords($"{Directory.GetCurrentDirectory()}\\TestFiles\\{file}.txt");
            return result.Count;
        }

        [Test]
        public void LoadDictionaryWordsWithExactMatch()
        {
            var result = ClassToTest.LoadDictionaryWords($"{Directory.GetCurrentDirectory()}\\TestFiles\\OnlyLinkWords.txt");
            HashSet<string> correctHashSet = new() { "cold", "cord", "card", "ward" };
            Assert.AreEqual(correctHashSet,result);
        }

        [Test]
        public void LoadPriorityTrackerWithoutErrors()
        {
            var result = ClassToTest.LoadPriorityTracker($"{Directory.GetCurrentDirectory()}\\TestFiles\\PriorityTrackerTest.txt");
            Assert.IsNotNull(result);
        }

        [Test]
        public void LoadPriorityTrackerExactMatch()
        {
            var result = ClassToTest.LoadPriorityTracker($"{Directory.GetCurrentDirectory()}\\TestFiles\\PriorityTrackerTest.txt");
            Dictionary<char, int> correctData = new()
            {
                { 'a', 1 },
                { 'b', 3 },
                { 'c', 0 }
            };
            Assert.AreEqual(correctData, result);
        }
    }
}
