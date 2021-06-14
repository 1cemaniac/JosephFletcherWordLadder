using System.Collections.Generic;

namespace BluePrismWordLadder.Logic
{
    public interface IDataLoader
    {
        HashSet<string> LoadStringsToHashSet(string filename);
    }
}
