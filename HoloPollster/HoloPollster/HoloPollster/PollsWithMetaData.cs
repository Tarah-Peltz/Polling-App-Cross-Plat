using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster
{
    public class PollsWithMetaData : IEnumerable<PollData>
    {
        public List<PollData> questions { get; set; }
        public string PollName { get; set; }
        public string PollCreator { get; set; }
        public DateTime CreationTime { get; set; }
        void PollsWithMetadata()
        {
            this.questions = new List<PollData>();
            this.PollName = "DefaultName";
            PollCreator = "NoName";
            CreationTime = DateTime.Now;
                
        }

        public IEnumerator<PollData> GetEnumerator()
        {
            return questions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return questions.GetEnumerator();
        }
    }
}
