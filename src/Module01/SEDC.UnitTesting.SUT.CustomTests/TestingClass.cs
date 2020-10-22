using NUnit.Framework;
using System.Threading;
[assembly: LevelOfParallelism(5)]

namespace SEDC.UnitTesting.SUT.CustomTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestingClass
    {
        [Test]
        [NonParallelizable]
        public void Test()
        {
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test1()
        {
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test2()
        {
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test3()
        {
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [Test]
        public void Test4()
        {

            Assert.IsTrue(true);
        }
    }
}
