using System;
using System.Collections.Generic;

using NUnit.Framework;


namespace SEDC.UnitTesting.SUT.Tests._02
{
    [TestFixture]
    public class IntegerMethodsTests
    {
        IntegerMethods im;
        List<int> listNumbers;

        [OneTimeSetUp]
        public void Init()
        {
            im = new IntegerMethods();
            listNumbers = new List<int>();
        }

        [TearDown]
        public void TearDown()
        {
            listNumbers.Clear();
        }


        [Test]
        public void IntegerMethods_GivenListWithValue_ShouldReturnCorrectResult()
        {
            //Arrange
            listNumbers.Add(1); listNumbers.Add(2); listNumbers.Add(3); listNumbers.Add(4); listNumbers.Add(5);
            var nthLargestNumber = 2;
            var expectedResult = 4;

            //Act
            var result = im.FindNthLargestNumber(listNumbers, nthLargestNumber);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [Ignore("FindNthLargestNumber method needs to be refactored", Until = "2020-10-24")]
        public void IntegerMethods_GivenEmptyList_ShouldReturnCorrectResult()
        {
            //Arrange
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
            listNumbers.Add(1); listNumbers.Add(2); listNumbers.Add(3); listNumbers.Add(4); listNumbers.Add(5);
            var nthLargestNumber = 7;

            //Act+Assert
            Assert.Catch<Exception>(() => im.FindNthLargestNumber(listNumbers, nthLargestNumber));
        }

    }
}
