using SEDC.Travel.Service.CustomTests.CustomAttributes;
using Xunit;

namespace SEDC.Travel.Service.CustomTests
{
    [TestCaseOrderer("SEDC.Travel.Service.CustomTests.CustomAttributes.PriorityOrderer", "SEDC.Travel.Service.CustomTests")]
    public class InitTest
    {
        [Fact]
        [Priority(1)]
        public void Test()
        {
            Assert.True(true);
        }

        [Priority(3)]
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Priority(4)]
        [Fact]
        public void Test2()
        {
            Assert.True(true);
        }

        [Priority(2)]
        [Fact]
        public void Test3()
        {
            Assert.True(true);
        }
    }
}
