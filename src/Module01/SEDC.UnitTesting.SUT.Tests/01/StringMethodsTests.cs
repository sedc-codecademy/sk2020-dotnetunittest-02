using FluentAssertions;
using NUnit.Framework;

namespace SEDC.UnitTesting.SUT.Tests._01
{
    [TestFixture]
    public class StringMethodsTests
    {
        [Test]
        [Category("StringMethods")]
        public void Reverse_StringIsNotEmpty_ResultShouldBeAsExpected()
        {
            //Arrange
            var sm = new StringMethods();
            var str = "SEDC";
            var expStr = "CDES";

            //Act
            var result = sm.Reverse(str);

            //Assert
            //Assert.AreEqual(expStr, result);
            result.Should().Be(expStr);

        }
    }
}
