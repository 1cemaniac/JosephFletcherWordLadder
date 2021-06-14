using BluePrismWordLadder.Data;
using System.Collections.Generic;

namespace BluePrismWordLadder.Logic
{
    public interface IWordProcessor
    {
        HashSet<string> Words { get; set; }
        List<WordNode> Results { get; set; }
        bool ForwardStep(WordNode node, string goalWord);
        bool SideStep(WordNode node, string goalWord);
        bool BackStep(WordNode node, string goalWord);
    }
}
