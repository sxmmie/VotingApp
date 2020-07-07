using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    /// <summary>
    /// For every person that says yes increment counter
    /// For every person that says no increment counter
    /// Yes counter wins or no counter wins or a draw
    /// </summary>
    public class Counter
    {
        private double? _percentage;

        // Decision and number of people tha made that decision
        public Counter(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; }
        public int Count { get; private set; }

        /// <summary>
        /// Percentage of vote (either yes or no)
        /// Divide the count by the total to get the ratio, multiply by 100, gets a percentage, using the count to calculate the yes or no percentage
        /// </summary>
        /// <param name="total">total number of count</param>
        /// <returns>return value If percentage is not null</returns>
        public double GetPercent(int total) => _percentage ?? (_percentage = Math.Round(Count * 100.0 / total, 2)).Value;

        public void AddExcess(double excess) => _percentage += excess;
    }

    // Someone who is going manage counters
    // Derive total, handle excess and find actual winner
    public class CounterManager
    {
        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }

        public List<Counter> Counters { get; set; }

        // Retrieve totals from counters using linq/lambda (total number of counts, both yes and no)
        public int Total() => Counters.Sum(x => x.Count);

        // Total percentage of the the total number of counts (both yes and no)
        public double TotalPercent() => Counters.Sum(x => x.GetPercent(Total()));

        public void AnnouceWinner()
        {
            var excess = Math.Round(100 - TotalPercent(), 2);
            Console.WriteLine($"Excess: {excess}");

            var biggestAmountOfVotes = Counters.Max(x => x.Count);  // Max amount of count, biggest amount of votes anyone can have
            var winners = Counters.Where(x => x.Count == biggestAmountOfVotes).ToList();

            // If we have one winner
            if (winners.Count == 1)
            {
                var winner = winners.First();
                winner.AddExcess(excess);
                Console.WriteLine($"{winner.Name} won!");
            }
            else
            {
                if (winners.Count != Counters.Count)
                {
                    // add excess (to lowest percentage)
                    var lowestAmountOfVotes = Counters.Min(x => x.Count);
                    var lowest = Counters.First(x => x.Count == lowestAmountOfVotes);
                    lowest.AddExcess(excess);
                }

                string.Join($" -DRAW- ", winners.Select(x => x.Name));
            }

            foreach(var c in Counters)
            {
                Console.WriteLine($"{c.Name} Counts: {c.Count}, Percentaage: {c.GetPercent(Total())}%");
            }

            Console.WriteLine($"Total Percentage: {Math.Round(TotalPercent(), 2)}%");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Yes counter wins or no counter wins or a draw
            // Yes or No
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 2);
            var maybe = new Counter("Maybe", 3);

            var manager = new CounterManager(yes, no, maybe);
            manager.AnnouceWinner();
        }
    }
}
