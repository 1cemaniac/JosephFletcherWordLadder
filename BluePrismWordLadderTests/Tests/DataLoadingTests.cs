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
        public void LoadWordsWithoutErrors()
        {
            var result = ClassToTest.LoadStringsToHashSet($"{Directory.GetCurrentDirectory()}\\TestFiles\\ValidAndInvalidFourLettersOnly.txt");
            Assert.IsNotNull(result);
        }

        [TestCase("InvalidOnly", ExpectedResult = 0)]
        [TestCase("ValidAndInvalidFourLettersOnly", ExpectedResult = 5)]
        [TestCase("ValidAndInvalidVaryingLength", ExpectedResult = 6)]
        [TestCase("OnlyLinkWords", ExpectedResult = 4)]
        [TestCase("LinkWordsWithExtra", ExpectedResult = 7)]
        public int LoadCorrectAmountOfWords(string file)
        {
            var result = ClassToTest.LoadStringsToHashSet($"{Directory.GetCurrentDirectory()}\\TestFiles\\{file}.txt");
            return result.Count;
        }

        [Test]
        public void LoadWordsWithExactMatch()
        {
            var result = ClassToTest.LoadStringsToHashSet($"{Directory.GetCurrentDirectory()}\\TestFiles\\OnlyLinkWords.txt");
            HashSet<string> correctHashSet = new() { "cold", "cord", "card", "ward" };
            Assert.AreEqual(correctHashSet,result);
        }
    }
}
