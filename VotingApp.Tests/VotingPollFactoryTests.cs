using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace VotingApp.Tests
{
    public class VotingPollFactoryTests
    {
        private VotingPollFactory _factory = new VotingPollFactory();
        private string[] _names = new[] { "name1", "names2" };
        private string _title = "title";
        private string _description = "description";

        [Fact]
        public void Create_ThrowsWhenLessThanTwoCounterNames()
        {
            Throws<ArgumentException>(() => _factory.Create(_title, _description, new[] { "name" }));
            Throws<ArgumentException>(() => _factory.Create(_title, _description, new string[] { }));
        }

        [Fact]
        public void Create_AddsCounterToThePollForEachName()
        {
            var poll = _factory.Create(_title, _description, _names);

            foreach(var names in names)
            {

            }
        }
    }
}
