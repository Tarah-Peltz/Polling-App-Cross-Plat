using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster
{
    public class PollData
    {
        public string QuestionText { get; set; }
        public enum AnswerType { Default = 0, TextBox = 0, RadioButton = 1 };
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
