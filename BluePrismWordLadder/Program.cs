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
    var filenameIn = Console.ReadLine();
    Console.WriteLine("");
    Console.WriteLine("Please input the word to start the ladder at.");
    var startWord = Console.ReadLine();
    Console.WriteLine("");
    Console.WriteLine("Please input the word to end the ladder at.");
    var endWord = Console.ReadLine();
    Console.WriteLine("");
    Console.WriteLine("Please input the file location you wish to save to.");
    var filenameOut = Console.ReadLine();

    Stopwatch s = new();
    s.Start();
    var wordLadderLogic = scope.Resolve<IWordLadderLogic>();
    s.Stop();

    if (wordLadderLogic.FindWordLadder(filenameIn, filenameOut, startWord, endWord))
    {
        Console.WriteLine($"The ladder was successfully generated in {s.ElapsedMilliseconds}ms");
        Console.WriteLine("Please see the output file for the completed list.");
    }
    else
        Console.WriteLine("The ladder failed to generate. Please check the input file exists and that the provided words are valid.");
}

