using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster 
{
    public class AllPollsCreated : IEnumerable<PollsWithMetaData>
    {
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
    
        
    


