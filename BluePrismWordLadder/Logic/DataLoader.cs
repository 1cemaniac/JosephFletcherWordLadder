using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BluePrismWordLadder.Logic
{
    public class DataLoader : IDataLoader
    {
        public HashSet<string> LoadDictionaryWords(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                return null;

            // Only 4 character words containing only letters to be used. All will be set to lowercase.
            Regex rx = new("^([a-z]){4}$");
            var loadedWords = File.ReadAllLines(fileLocation);
            var test = rx.IsMatch("cold");
            return loadedWords.Select(lw => lw.ToLower())
                              .Where(lw => rx.IsMatch(lw))
                              .ToHashSet();
        }

        public Dictionary<char, int> LoadPriorityTracker(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                return null;

            var data = File.ReadAllLines(fileLocation);
            Dictionary<char, int> priorityData = new();
            try
            {
                foreach (var entry in data)
                {
                    var splitEntry = entry.Split(":");
                    priorityData.Add(char.Parse(splitEntry[0]), int.Parse(splitEntry[1]));
                }
            }
            catch(Exception e)
            {
                return null;
            }
            return priorityData;
        }
    }
}
