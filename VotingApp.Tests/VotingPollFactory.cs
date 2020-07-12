using System;
using System.Linq;

namespace VotingApp.Tests
{
    public class VotingPollFactory
    {
        public VotingPollFactory()
        {
        }

        public void Create(string[] names)
        {
            if (names.Length < 2)
                throw new ArgumentException();

            return new VotingPoll
            {
                Counters = names.Select(name => new Counter { Name = name })
            };
        }
    }
}