using BluePrismWordLadder.Logic;
using NUnit.Framework;
using System.IO;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class StartToEndTests
    {
        WordLadderLogic ClassToTest;

        [OneTimeSetUp]
        public void Setup()
        {
            ClassToTest = new(new DataLoader(),new LadderGenerator(new WordProcessor(), new PriorityTracker(new DataLoader(), new ResultOutputter())),new ResultOutputter());
        }

        [TestCase("OnlyLinkWords", ExpectedResult = true)]
        [TestCase("LinkWordsWithExtra", ExpectedResult = true)]
        [TestCase("InvalidOnly", ExpectedResult = false)]
        public bool StartToEndTestCanFindResult(string testFile)
        {
            return ClassToTest.FindWordLadder($"{Directory.GetCurrentDirectory()}\\TestFiles\\{testFile}.txt", "cold", "ward");
        }

        [TestCase("OnlyLinkWords","ExpectedOutput")]
        [TestCase("LinkWordsWithExtra", "ExpectedOutput")]
        public void StartToEndTestCorrectResult(string testFile, string outputFile)
        {
           ClassToTest.FindWordLadder($"{Directory.GetCurrentDirectory()}\\TestFiles\\{testFile}.txt","cold","ward");
            var result = File.ReadAllText($"Output.txt");
            Assert.AreEqual(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\TestFiles\\{outputFile}.txt"), result);
        }
    }
}
