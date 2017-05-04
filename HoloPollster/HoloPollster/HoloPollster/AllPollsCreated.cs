using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster 
{
    /// <summary>
    /// Class AllPollsCreated.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{HoloPollster.PollsWithMetaData}" />
    public class AllPollsCreated : IEnumerable<PollsWithMetaData>
    { ///Creates a list of all the polls created
        public List<PollsWithMetaData> CreatedPolls { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AllPollsCreated"/> class.
        /// </summary>
        public AllPollsCreated() {
            CreatedPolls = new List<PollsWithMetaData>();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<PollsWithMetaData> GetEnumerator()
        {
            return CreatedPolls.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return CreatedPolls.GetEnumerator();
        }

    }
    }
    
        
    


