namespace BluePrismWordLadder.Logic
{
    public interface IWordLadderLogic
    {
        public bool FindWordLadder(string filenameIn, string filenameOut, string startWord, string endWord);
    }
}
