using BluePrismWordLadder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrismWordLadder.Logic
{
    public interface IPriorityTracker
    {
        void Setup(int wordLength);
        void Log(string word);
        List<WordNode> Prioritise(List<WordNode> wordsToPrioritise);
        void Save();
    }
}
