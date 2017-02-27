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
        public string AnswerType { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        public PollData()
        {
            //Because labels bind to these values, set them to an empty string to  
            //ensure that the label appears on all platforms by default.  
            this.QuestionText = "Question...";
            this.Wind = " ";
            this.Humidity = " ";
            this.Visibility = " ";
            this.Sunrise = " ";
            this.Sunset = " ";
        }

    }
}
