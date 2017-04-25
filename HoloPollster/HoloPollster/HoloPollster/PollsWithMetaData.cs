using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace HoloPollster
{
    [DataContract(Name="MetaPoll", Namespace = "holopollster")]
    public class PollsWithMetaData : IEnumerable<PollData>
    {
        [DataMember()]
        public List<PollData> questions { get; set; }
        [DataMember()]
        public string PollName { get; set; }
        [DataMember()]
        public string PollCreator { get; set; }
        [DataMember()]
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

        public void Add(PollData p)
        {
            p.QuestionText = "random text";
            //questions.Add(p);
        }

    }
}
