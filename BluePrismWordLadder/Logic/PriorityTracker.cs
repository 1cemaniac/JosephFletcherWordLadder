using BluePrismWordLadder.Data;
using System;
using System.Collections.Generic;
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
            FileLocation = $"Priority_List_For_Words_Of_Length_{wordLength}";
            var fileData = _dataLoader.LoadStringsToHashSet(FileLocation);
            if (fileData == null)
                PriorityData = CreateNewPriorityData();
            else
                PriorityData = ParseExistingPriorityData(fileData);
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

        private Dictionary<char, int> ParseExistingPriorityData(HashSet<string> fileData)
        {
            Dictionary<char, int> priorityData = new();
            foreach(var entry in fileData)
            {
                var splitEntry = entry.Split(":");
                priorityData.Add(char.Parse(splitEntry[0]), int.Parse(splitEntry[1]));
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
