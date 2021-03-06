﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Tests
{
    public class CounterManager
    {
        public Counter GetStatistics(Counter counter, int totalCount)
        {
            counter.Percent = RoundUp(counter.Count * 100.0 / totalCount);
            return counter;
        }

        public void ResolveExcess(List<Counter> counters)
        {
            if (counters.Sum(x => x.Percent) == 100) return;

            var highestPercent = counters.Max(x => x.Percent);  // grab the counter with the highest percent
            var highestCounters = counters.Where(x => x.Percent == highestPercent).ToList();
            
            // Only when there one highest counter
            if (highestCounters.Count == 1)
            {
                counters[0].Percent += 0.01;
            } else if (highestCounters.Count < counters.Count)
            {
                var lowestPercent = counters.Min(x => x.Percent);  // grab the counter with the lowest percent
                counters.First(x => x.Percent == lowestPercent).Percent += 0.1;
            }
        }

        private static double RoundUp(double num) => Math.Round(num, 2);
    }
}
