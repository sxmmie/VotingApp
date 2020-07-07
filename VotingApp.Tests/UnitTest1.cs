using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace VotingApp.Tests
{
    public interface ITestOne
    {
        int Add(int a, int b);
        void Out(string msg);
    }

    public class MathOne : ITestOne
    {
        private readonly ITestOne _testOne;

        public MathOne(ITestOne testOne)
        {
            _testOne = testOne;
        }

        public int Add(int a, int b) => _testOne.Add(a, b);

        public void Out(string msg) => _testOne.Out(msg);
    }

    public class TestOne : ITestOne
    {
        public int Add(int a, int b) => a + b;

        public void Out(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    // Tests        
    public class MathOneTests
    {
        [Fact]
        public void MathOneAddsTwoNumbers()
        {
            // Mocking the behaviour of the interface
            var testOnMock = new Mock<ITestOne>();
            testOnMock.Setup(x => x.Add(10, 10)).Returns(20);
            var mathOne = new MathOne(testOnMock.Object);       // extract the object from the mock object

            Assert.Equal(20, mathOne.Add(10, 10));
        }

        [Fact]
        public void VerifyFunctionHasBeenCalled()
        {
            // Mocking the behaviour of the interface
            var testOnMock = new Mock<ITestOne>();
            var msg = "Hello Web";

            var mathOne = new MathOne(testOnMock.Object);
            mathOne.Out(msg);

            testOnMock.Verify(x => x.Out(msg), Times.Once);
        }
    }

    public class TestOneTests
    {
        [Fact]
        public void Add_TwoNumbersTogether()
        {
            var result = new TestOne().Add(1, 2);
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        public void Add_TwoNumbersTogether_Theory(int a, int b, int expected)
        {
            var result = new TestOne().Add(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestListContainsValue()
        {
            var list = new List<int> { 1, 2, 3, 4 };
            Assert.Contains(1, list);
        }
    }
}
