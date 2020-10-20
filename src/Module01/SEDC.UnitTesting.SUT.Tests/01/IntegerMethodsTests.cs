using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace SEDC.UnitTesting.SUT.Tests._01
{
    [TestFixture]
    public class IntegerMethodsTests
    {
        [Test]
        public void IntegerMethods_GivenListWithValue_ShouldReturnCorrectResult()
        {
            //Arrange
            var im = new IntegerMethods();
            var listNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var nthLargestNumber = 2;
            var expectedResult = 4;

            //Act
            var result = im.FindNthLargestNumber(listNumbers, nthLargestNumber);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [Ignore("FindNthLargestNumber method needs to be refactored", Until ="2020-10-24")]
        public void IntegerMethods_GivenEmptyList_ShouldReturnCorrectResult()
        {
            //Arrange
            var im = new IntegerMethods();
            var listNumbers = new List<int>();
            var nthLargestNumber = 2;
            var expectedResult = 4;

            //Act
            var result = im.FindNthLargestNumber(listNumbers, nthLargestNumber);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void IntegerMethods_GivenEmptyList_ShouldThrowException()
        {
            //Arrange
            var im = new IntegerMethods();
            var listNumbers = new List<int>();
            var nthLargestNumber = 2;
            var msg = "SEDC ex";

            //Act+Assert
            var result = Assert.Throws<Exception>(() => im.FindNthLargestNumber(listNumbers, nthLargestNumber));
            Assert.AreEqual(msg, result.Message);
        }

        [Test]
        public void IntegerMethods_GivenListWithCount5AndNthLargestNumberIsBigger_ShouldThrowException()
        {
            //Arrange
            var im = new IntegerMethods();
            var listNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var nthLargestNumber = 7;

            //Act+Assert
            Assert.Catch<Exception>(() => im.FindNthLargestNumber(listNumbers, nthLargestNumber));
        }

    }
}
