using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Tests
{
    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<Counter>();  // same as new List<Counter> but List occupies space in memory
        }

        public IEnumerable<Counter> Counters { get; set; }
    }
}