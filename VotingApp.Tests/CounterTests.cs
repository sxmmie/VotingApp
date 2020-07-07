using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace VotingApp.Tests
{
    public class CounterTests
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter { Name = CounterName, Count = 5 };

        // Counter should have a name for it to be displayed
        [Fact]
        public void HasName()
        {
            // Expects name to exist Counter
            Assert.Equal(CounterName, _counter.Name);
        }

        // Get Counter Info
        [Fact]
        public void GetStatistics_IncludesNames()
        {
            // statistics is the info about the counter in regards to other counters
            var statistics = _counter.GetStatistics(5);

            Assert.Equal(CounterName, statistics.Name);
        }

        [Fact]
        public void GetStatistics_IncludesCount()
        {
            // statistics is the info about the counter in regards to other counters
            var statistics = _counter.GetStatistics(5);

            Assert.Equal(5, statistics.Count);
        }

        // to get the percentage for the counters, we need other counters. (50%)
        [Theory]
        [InlineData(5, 10, 50)]
        public void Statistics_ShowsPercentageUpToTwoBasedOnTotalCount(int count, int total, double expected)
        {
            _counter.Count = count;

            var statistics = _counter.GetStatistics(total);

            Assert.Equal(expected, statistics.Percent);      // this is just one counter
        }

        [Fact]
        public void ResolveExcess_DoesntAddExcessWhenAllCountersAreEqual()
        {
            // All counters got equal votes
            var counter1 = new Counter { Count = 1, Percent = 33.33 };
            var counter2 = new Counter { Count = 1, Percent = 33.33 };
            var counter3 = new Counter { Count = 1, Percent = 33.33 };

            var counters = new List<Counter> { counter1, counter2, counter3 };
            new CounterManager().ResolveExcess(counters);

            Assert.Equal(33.33, counter1.Percent);
            Assert.Equal(33.33, counter2.Percent);
            Assert.Equal(33.33, counter3.Percent);
        }

        [Fact]
        public void ResolvesExcess_AddExcessToHighestCounter()
        {
            var counter1 = new Counter { Count = 2, Percent = 66.66 };
            var counter2 = new Counter { Count = 1, Percent = 33.33 };

            var counters = new List<Counter> { counter1, counter2 };
            new CounterManager().ResolveExcess(counters);

            Assert.Equal(66.67, counter1.Percent);
            Assert.Equal(33.33, counter2.Percent);
        }

        [Fact]
        public void ResolvesExcess_AddExcessToLowestCounterWhenMoreThanOneHighestCounters()
        {
            var counter1 = new Counter { Count = 2, Percent = 44.44 };
            var counter2 = new Counter { Count = 2, Percent = 44.44 };
            var counter3 = new Counter { Count = 1, Percent = 11.11 };
            var counters = new List<Counter> { counter1, counter2, counter3 };

            new CounterManager().ResolveExcess(counters);

            Assert.Equal(44.44, counter1.Percent);
            Assert.Equal(44.44, counter2.Percent);
            Assert.Equal(11.11, counter2.Percent);
        }

        // If percent is 100%, do not resolve excess
        ///public void 
    }
}
