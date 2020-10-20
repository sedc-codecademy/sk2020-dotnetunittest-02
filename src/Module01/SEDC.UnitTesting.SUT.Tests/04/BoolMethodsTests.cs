using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.UnitTesting.SUT.Tests._04
{
    [TestFixture(Author = "SEDC")]
    public class BoolMethodsTests
    {
        BoolMethods bm;

        [SetUp]
        public void Setup()
        {
            bm = new BoolMethods();
        }

        [Test]
        [Author("SEDC")]
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
        [Author("SEDC", "sedc@in.com")]
        public void IsLeapYear_YearIsNotLeap_TheResultShouldBeFalse()
        {
            //Arrange
            var year = 1997;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.False(result);
        }

        [Test(Author = "SEDC")]
        public void IsLeapYear_YearIsNegative_ShouldThrowArgumentException()
        {
            //Arrange
            var year = -1997;

            //Act+Assert
            Assert.Throws<ArgumentException>(() => bm.IsLeapYear(year));
        }

        [Test(Author = "SEDC")]
        public void IsLeapYear_YearIsNegative_ShouldCatchArgumentException()
        {
            //Arrange
            var year = -1997;

            //Act+Assert
            Assert.Catch<Exception>(() => bm.IsLeapYear(year));
        }

        [Test]
        [Author("SEDC", "sedc@in.com")]
        [Repeat(2)]
        public void IsLeapYear_RepeatYearIsNotLeap_TheResultShouldBeFalse()
        {
            //Arrange
            var year = 1997;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.False(result);
        }

        [Test]
        [Author("SEDC", "sedc@in.com")]
        public void IsLeapYear_ValuesYearIsNotLeap_TheResultShouldBeFalse([Values(1997, 1998, 1999)] int year, [Values(false)] bool expectedResult)
        {
            //Arrange

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }



        //TODO: Add test with[SetCulture]
    }
}
