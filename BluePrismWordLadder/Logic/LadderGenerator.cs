using BluePrismWordLadder.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BluePrismWordLadder.Logic
{
    public class LadderGenerator : ILadderGenerator
    {
        private IWordProcessor _wordProcessor { get;set;}
        private IPriorityTracker _priorityTracker { get; set; }
        public LadderGenerator(IWordProcessor wordProcessor, IPriorityTracker priorityTracker) => (_wordProcessor,_priorityTracker) = (wordProcessor,priorityTracker);

        private List<WordNode> SearchForward;
        private List<WordNode> SearchSideways;
        private List<WordNode> SearchBack;

        public Stack<string> GenerateLadder(HashSet<string> words, string startWord, string endWord)
        {
            Setup(words, startWord, endWord);
            var finalNode = FindLadder(endWord);
            return Output(finalNode);
        }

        private void Setup(HashSet<string> words, string startWord, string endWord)
        {
            WordNode startingNode = new(startWord, null, endWord);
            SearchForward = new() { startingNode };
            SearchSideways = new();
            SearchBack= new();
            _wordProcessor.Results = new();
            _wordProcessor.Words = words;
            _priorityTracker.Setup(endWord.Length);
        }

        private WordNode FindLadder(string endWord)
        {
            while (SearchForward.Any() || SearchSideways.Any() || SearchBack.Any())
            {
                if (SearchForward.Any(wn => _wordProcessor.ForwardStep(wn, endWord)))
                    break;

                if (SearchSideways.Any(wn => _wordProcessor.SideStep(wn, endWord)))
                    break;

                if (SearchBack.Any(wn => _wordProcessor.BackStep(wn, endWord)))
                    break;

                _wordProcessor.Results.ForEach(sr => _priorityTracker.Log(sr.Word));

                SearchBack = SearchSideways;
                SearchSideways = SearchForward;
                SearchForward = _priorityTracker.Prioritise(_wordProcessor.Results);
                _wordProcessor.Results = new();
            }
            _priorityTracker.Save();
            return _wordProcessor.Results.Where(r => r.Word == endWord).FirstOrDefault();
        }

        private Stack<string> Output(WordNode nodeToStack)
        {
            Stack<string> ladder = new();

            while (nodeToStack != null)
            {
                ladder.Push(nodeToStack.Word);
                nodeToStack = nodeToStack.PrevNode;
            }

            return ladder;
        }
    }
}
