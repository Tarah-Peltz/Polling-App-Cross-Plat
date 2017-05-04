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
    [DataContract(Name="MetaPoll", Namespace = "holopollster")] //Necessary to serialize and deserialize data when working with cloud
    public class PollsWithMetaData : IEnumerable<PollData>
    {
        [DataMember()] //Necessary for serialziation
        public List<PollData> questions { get; set; } //List of questions
        [DataMember()]
        public string PollName { get; set; } //Name of poll
        [DataMember()]
        public string PollCreator { get; set; } //Creator of poll
        [DataMember()]
        public DateTime CreationTime { get; set; } //Time poll was created
        void PollsWithMetadata()
        { //Sets defaults when an instance is initialized
            this.questions = new List<PollData>();
            this.PollName = "DefaultName";
            PollCreator = "NoName";
            CreationTime = DateTime.Now;
                
        }

        public IEnumerator<PollData> GetEnumerator() //These allow us to iterate through the questions
        {
            return questions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return questions.GetEnumerator();
        }

        public void Add(PollData p)
        {
            p.QuestionText = "random text"; //Sets default text when a polldata is added
           
        }

    }
}
