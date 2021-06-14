using System.Collections.Generic;

namespace BluePrismWordLadder.Logic
{
    public interface ILadderGenerator
    {
        Stack<string> GenerateLadder(HashSet<string> words, string startWord, string endWord);
    }
}
