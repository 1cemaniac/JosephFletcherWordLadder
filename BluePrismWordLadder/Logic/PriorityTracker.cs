using BluePrismWordLadder.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrismWordLadder.Logic
{
    public class PriorityTracker : IPriorityTracker
    {
        private IDataLoader _dataLoader { get; set; }
        private IResultOutputter _resultOutputter { get; set; }
        private Dictionary<char, int> PriorityData;
        private string FileLocation;
        public PriorityTracker(IDataLoader dataLoader, IResultOutputter resultOutputter) => (_dataLoader, _resultOutputter) = (dataLoader,resultOutputter);

        public void Setup(int wordLength)
        {
            FileLocation = $"{Directory.GetCurrentDirectory()}\\Priority_List_For_Words_Of_Length_{wordLength}.txt";
            PriorityData = _dataLoader.LoadPriorityTracker(FileLocation);
            if (PriorityData == null || PriorityData.Count == 0)
                PriorityData = CreateNewPriorityData();
        }

        private Dictionary<char, int> CreateNewPriorityData()
        {
            Dictionary<char, int> priorityData = new();
            for (int i = 97; i < 123; i++)
            {
                priorityData.Add((char)i, 0);
            }
            return priorityData;
        }

        public void Log(string word)
        {
            foreach(var c in word)
            {
                PriorityData[c]++;
            }
        }

        public List<WordNode> Prioritise(List<WordNode> wordsToPrioritise)
        {
            return wordsToPrioritise.OrderByDescending(w => w.Word.Sum(c => PriorityData[c])).ToList();
        }

        public void Save()
        {
            var test = PriorityData.Select(pd => $"{ pd.Key}:{pd.Value}");
            _resultOutputter.Output(test, FileLocation);
        }
    }
}
