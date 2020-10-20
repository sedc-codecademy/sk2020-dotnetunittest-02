using System;

using NUnit.Framework;


namespace SEDC.UnitTesting.SUT.Tests._02
{
    [TestFixture]
    public class BoolMethodsTests
    {
        BoolMethods bm;

        [SetUp]
        public void Setup()
        {
            bm = new BoolMethods();
        }

        [Test]
        public void IsLeapYear_YearIsLeap_TheResultShouldBeTrue()
        {
            //Arrange
            var year = 1996;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsLeapYear_YearIsNotLeap_TheResultShouldBeFalse()
        {
            //Arrange
            var year = 1997;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.False(result);
        }

        [Test]
        public void IsLeapYear_YearIsNegative_ShouldThrowArgumentException()
        {
            //Arrange
            var year = -1997;

            //Act+Assert
            Assert.Throws<ArgumentException>(() => bm.IsLeapYear(year));
        }

        [Test]
        public void IsLeapYear_YearIsNegative_ShouldCatchArgumentException()
        {
            //Arrange
            var year = -1997;

            //Act+Assert
            Assert.Catch<Exception>(() => bm.IsLeapYear(year));
        }
    }
}
