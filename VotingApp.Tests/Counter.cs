using System;
using System.Collections.Generic;

namespace VotingApp.Tests
{
    public class Counter
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Percent { get; set; }

        // more generic 
        internal Counter GetStatistics(int totalCount)
        {
            Percent = Math.Round(Count * 100.0 / totalCount, 2);
            return this;
        }
    }
}