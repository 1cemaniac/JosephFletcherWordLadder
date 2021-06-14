using BluePrismWordLadder.Logic;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace BluePrismWordLadderTests.Tests
{
    [TestFixture]
    public class OutputTests
    {
        ResultOutputter ClassToTest;

        [OneTimeSetUp]
        public void Setup()
        {
            ClassToTest = new();
        }

        [Test]
        public void DoesNotError()
        {
            Stack<string> ladder = new(new[] { "cold", "cord", "card", "ward" });
            Assert.IsTrue(ClassToTest.Output(ladder, $"{Directory.GetCurrentDirectory()}\\TestFiles\\Output.txt"));
        }

        [Test]
        public void GeneratesFile()
        {
            Stack<string> ladder = new(new[] { "ward", "card", "cord", "cold" });
            var filelocation = $"{Directory.GetCurrentDirectory()}\\TestFiles\\Output.txt";
            ClassToTest.Output(ladder, filelocation);
            Assert.IsTrue(File.Exists(filelocation));
        }

        [Test]
        public void GeneratesFileWithExactMatch()
        {
            Stack<string> ladder = new(new[] { "ward", "card", "cord", "cold" });
            var filelocation = $"{Directory.GetCurrentDirectory()}\\TestFiles\\Output.txt";
            ClassToTest.Output(ladder,filelocation);
            var result = File.ReadAllText(filelocation);
            Assert.AreEqual(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\TestFiles\\ExpectedOutput.txt"), result);
        }
    }
}
