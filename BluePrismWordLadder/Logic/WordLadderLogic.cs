using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrismWordLadder.Logic
{
    public class WordLadderLogic : IWordLadderLogic
    {
        private IDataLoader _dataLoader { get; set; }
        private ILadderGenerator _ladderGenerator { get; set; }
        private IResultOutputter _resultOutputter { get; set; }

        public WordLadderLogic(IDataLoader dataLoader, ILadderGenerator ladderGenerator, IResultOutputter resultOutputter) => (_dataLoader, _ladderGenerator, _resultOutputter) = (dataLoader, ladderGenerator, resultOutputter);

        public bool FindWordLadder(string filename, string startWord, string endWord)
        {
            try
            {
                var data = _dataLoader.LoadStringsToHashSet(filename);
                var wordLadder = _ladderGenerator.GenerateLadder(data, startWord, endWord);
                return _resultOutputter.Output(wordLadder,"Output.txt");
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
    }
}
