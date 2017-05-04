using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster 
{
    public class AllPollsCreated : IEnumerable<PollsWithMetaData>
    { //Creates a list of all the polls created
        public List<PollsWithMetaData> CreatedPolls { get; set; }
        public AllPollsCreated() {
            CreatedPolls = new List<PollsWithMetaData>();
        }
        public IEnumerator<PollsWithMetaData> GetEnumerator()
        {
            return CreatedPolls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return CreatedPolls.GetEnumerator();
        }

    }
    }
    
        
    


