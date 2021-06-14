using System.Collections.Generic;

namespace BluePrismWordLadder.Logic
{
    public interface IResultOutputter
    {
        bool Output(IEnumerable<string> objectsToOutput, string outputLocation);
    }
}
