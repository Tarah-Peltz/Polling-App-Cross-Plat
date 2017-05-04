using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;


namespace HoloPollster
{
    
    public class PollData 
    {
        public string QuestionText { get; set; }
        public enum AnswerType { Default = 0, TextBox = 0, RadioButton = 1 }; //This would allow for expansion to other types of questions in the future
        public AnswerType type;
       

        public PollData()
        {
            //Because labels bind to these values, set them to an empty string to  
            //ensure that the label appears on all platforms by default.  
            this.QuestionText = "Question...";
            type = AnswerType.Default;
        }

    }
}
