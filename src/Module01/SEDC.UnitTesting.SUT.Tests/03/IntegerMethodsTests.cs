using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.UnitTesting.SUT.Tests._03
{
    [TestFixture(Description = "Add description")]
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
        [Description("Add description")]
        [Order(1)]
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

        [Test(Description ="Add description")]
        [Order(3)]
        public void IntegerMethods_GivenEmptyList_ShouldThrowException()
        {
            //Arrange
            var nthLargestNumber = 2;
            var msg = "SEDC ex";

            //Act+Assert
            var result = Assert.Throws<Exception>(() => im.FindNthLargestNumber(listNumbers, nthLargestNumber));
            Assert.AreEqual(msg, result.Message);
        }

        [Test(Description = "Add description")]
        [Order(2)]
        public void IntegerMethods_GivenListWithCount5AndNthLargestNumberIsBigger_ShouldThrowException()
        {
            //Arrange
            listNumbers.Add(1); listNumbers.Add(2); listNumbers.Add(3); listNumbers.Add(4); listNumbers.Add(5);
            var nthLargestNumber = 7;

            //Act+Assert
            Assert.Catch<Exception>(() => im.FindNthLargestNumber(listNumbers, nthLargestNumber));
        }

        [Test]
        [Description("Add description")]
        public void IntegerMethods_GivenListWithValue_ShouldReturnCorrectResultCase1()
        {
            //Arrange
            listNumbers.Add(1); listNumbers.Add(2); listNumbers.Add(3); listNumbers.Add(4); listNumbers.Add(5); listNumbers.Add(6); listNumbers.Add(7);
            var nthLargestNumber = 3;
            var expectedResult = 5;

            //Act
            var result = im.FindNthLargestNumber(listNumbers, nthLargestNumber);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

    }
}
