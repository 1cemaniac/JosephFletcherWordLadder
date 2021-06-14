using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BluePrismWordLadder.Logic
{
    public class DataLoader : IDataLoader
    {
        public HashSet<string> LoadStringsToHashSet(string fileLocation)
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
    }
}
