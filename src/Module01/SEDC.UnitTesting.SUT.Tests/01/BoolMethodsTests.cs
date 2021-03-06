﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace SEDC.UnitTesting.SUT.Tests._01
{
    [TestFixture]
    public class BoolMethodsTests
    {
        [Test]
        public void IsLeapYear_YearIsLeap_TheResultShouldBeTrue()
        {
            //Arrange
            var bm = new BoolMethods();
            var year = 1996;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            //Assert.IsTrue(result);
            result.Should().BeTrue();
        }

        [Test]
        public void IsLeapYear_YearIsNotLeap_TheResultShouldBeFalse()
        {
            //Arrange
            var bm = new BoolMethods();
            var year = 1997;

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            //Assert.False(result);
            result.Should().BeFalse();
        }

        [Test]
        public void IsLeapYear_YearIsNegative_ShouldThrowArgumentException()
        {
            //Arrange
            var bm = new BoolMethods();
            var year = -1997;

            //Act+Assert
            //Assert.Throws<ArgumentException>(() => bm.IsLeapYear(year));
            Action result = () => bm.IsLeapYear(year);
            result.Should().Throw<ArgumentException>();

        }

        [Test]
        public void IsLeapYear_YearIsNegative_ShouldCatchArgumentException()
        {
            //Arrange
            var bm = new BoolMethods();
            var year = -1997;

            //Act+Assert
            Assert.Catch<Exception>(() => bm.IsLeapYear(year));
        }
    }
}
