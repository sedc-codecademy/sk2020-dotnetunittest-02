using NUnit.Framework;

namespace SEDC.UnitTesting.SUT.Tests._02
{
    [TestFixture]
    public class StringMethodsTests
    {
        StringMethods sm;

        [SetUp]
        public void Setup()
        {
            sm = new StringMethods();
        }


        [Test]
        [Category("StringMethods")]
        public void Reverse_StringIsNotEmpty_ResultShouldBeAsExpected()
        {
            //Arrange
            var str = "SEDC";
            var expStr = "CDES";

            //Act
            var result = sm.Reverse(str);

            //Assert
            Assert.AreEqual(expStr, result);
        }
    }
}
