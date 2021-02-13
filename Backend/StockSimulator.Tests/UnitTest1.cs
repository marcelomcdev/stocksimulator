using NUnit.Framework;

namespace StockSimulator.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestFalse()
        {
            Assert.False(0 == 1);
        }
    }
}