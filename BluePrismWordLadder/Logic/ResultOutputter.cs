using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BluePrismWordLadder.Logic
{
    public class ResultOutputter : IResultOutputter
    {
        public bool Output(IEnumerable<string> objectsToOutput, string outputLocation)
        {
            if (!objectsToOutput.Any())
                return false;
            File.Delete(outputLocation);
            File.WriteAllLines(outputLocation, objectsToOutput);
            return true;
        }
    }
}
