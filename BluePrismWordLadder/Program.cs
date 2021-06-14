using Autofac;
using BluePrismWordLadder;
using BluePrismWordLadder.Logic;
using System;
using System.Diagnostics;

var container = ContainerConfig.Configure();
using (var scope = container.BeginLifetimeScope())
{
    Console.WriteLine("Welcome to the World Ladder Generator.");
    Console.WriteLine("Please input the file containing the list of words to be searched.");
    var filename = Console.ReadLine();
    Console.WriteLine("");
    Console.WriteLine("Please input the word to start the ladder at.");
    var startWord = Console.ReadLine();
    Console.WriteLine("");
    Console.WriteLine("Please input the word to end the ladder at.");
    var endWord = Console.ReadLine();

    Stopwatch s = new();
    s.Start();
    var wordLadderLogic = scope.Resolve<IWordLadderLogic>();
    s.Stop();

    Console.WriteLine($"The ladder was {(wordLadderLogic.FindWordLadder(filename, startWord, endWord) ? "" : "un")}successfully generated. In {s.ElapsedMilliseconds}ms");
    Console.WriteLine("Please see the Output.txt file for the completed list.");
    
}

