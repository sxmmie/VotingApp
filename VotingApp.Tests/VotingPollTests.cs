using System;
using Xunit;

namespace VotingApp.Tests
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCountersWhenCreated()
        {
            var poll = new VotingPoll();

            Assert.Empty(poll.Counters);
        }
    }

    // Test suite for voting poll factory
    public class VotingPollFactoryTests
    {
        [Fact]
        public void Create_ThrowWhenLessThanTwoCounterNames()
        {
            var names = new[] { "name" };
            var factory = new VotingPollFactory();

            Assert.Throws<ArgumentException>(() => factory.Create(names));
        }
    }
}
