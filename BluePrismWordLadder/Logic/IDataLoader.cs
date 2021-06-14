using System.Collections.Generic;

namespace BluePrismWordLadder.Logic
{
    public interface IDataLoader
    {
        HashSet<string> LoadDictionaryWords(string filename);
        Dictionary<char, int> LoadPriorityTracker(string fileLocation);
    }
}
