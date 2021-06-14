using BluePrismWordLadder.Data;
using System.Collections.Generic;
using System.Text;

namespace BluePrismWordLadder.Logic
{
    public class WordProcessor : IWordProcessor
    {
        public HashSet<string> Words { get; set; }
        public List<WordNode> Results { get; set; }

        public bool ForwardStep(WordNode node, string goalWord)
        {
            for(int charPos = 0; charPos < goalWord.Length && charPos < node.Word.Length; charPos++)
            {
                if (goalWord[charPos] == node.Word[charPos])
                    continue;

                var newWord = node.Word.Remove(charPos, 1).Insert(charPos, goalWord.Substring(charPos, 1));
                if (Words.Remove(newWord))
                {
                    WordNode newNode = new(newWord, node, goalWord);
                    Results.Add(newNode);

                    if (newNode.NumMatchingChars == goalWord.Length - 1)
                    {
                        WordNode goalNode = new(goalWord, newNode, goalWord);
                        Results.Add(goalNode);
                        return true;
                    }

                    if (ForwardStep(newNode, goalWord))
                        return true;
                }
            }

            return false;
        }

        public bool SideStep(WordNode node, string goalWord)
        {
            for (int charPos = 0; charPos < goalWord.Length && charPos < node.Word.Length; charPos++)
            {
                if (goalWord[charPos] == node.Word[charPos])
                    continue;

                StringBuilder sb = new(node.Word);

                for(int i = 97; i < 123; i++)
                {
                    sb[charPos] = (char)i;
                    var newWord = sb.ToString();
                    if (Words.Remove(newWord))
                    {
                        WordNode newNode = new(newWord, node, goalWord);
                        Results.Add(newNode);

                        if (ForwardStep(newNode, goalWord))
                            return true;
                    }
                }
            }

            return false;
        }

        public bool BackStep(WordNode node, string goalWord)
        {
            for (int charPos = 0; charPos < goalWord.Length && charPos < node.Word.Length; charPos++)
            {
                if (goalWord[charPos] != node.Word[charPos])
                    continue;

                StringBuilder sb = new(node.Word);

                for (int i = 97; i < 123; i++)
                {
                    sb[charPos] = (char)i;
                    var newWord = sb.ToString();
                    if (Words.Remove(newWord))
                    {
                        WordNode newNode = new(newWord, node, goalWord);
                        Results.Add(newNode);

                        if (ForwardStep(newNode, goalWord))
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
