using System;

namespace BluePrismWordLadder.Logic
{
    public class WordLadderLogic : IWordLadderLogic
    {
        private IDataLoader _dataLoader { get; set; }
        private ILadderGenerator _ladderGenerator { get; set; }
        private IResultOutputter _resultOutputter { get; set; }

        public WordLadderLogic(IDataLoader dataLoader, ILadderGenerator ladderGenerator, IResultOutputter resultOutputter) => (_dataLoader, _ladderGenerator, _resultOutputter) = (dataLoader, ladderGenerator, resultOutputter);

        public bool FindWordLadder(string filenameIn, string filenameOut, string startWord, string endWord)
        {
            try
            {
                var data = _dataLoader.LoadDictionaryWords(filenameIn);
                if (data == null)
                    return false;
                var wordLadder = _ladderGenerator.GenerateLadder(data, startWord, endWord);
                return _resultOutputter.Output(wordLadder, filenameOut);
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
    }
}
