namespace BluePrismWordLadder.Data
{
    public class WordNode
    {
        public string Word { get; set; }
        public int NumMatchingChars { get; set; }
        public WordNode PrevNode { get; set; }
        public WordNode(string word, WordNode prevNode, string goalWord)
        {
            Word = word;
            PrevNode = prevNode;
            NumMatchingChars = 0;
            for (int i = 0; i < goalWord.Length && i < Word.Length; i++)
            {
                if(goalWord[i] == Word[i])
                    NumMatchingChars++;
            }
        }
    }
}
