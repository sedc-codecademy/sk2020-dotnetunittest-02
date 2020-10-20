using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.UnitTesting.SUT.Tests._03
{
    [TestFixture]
    [Order(1)]
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

        [Test]
        [Category("StringMethods")]
        [Retry(3)]
        public void Reverse_RetryStringIsNotEmpty_ResultShouldBeAsExpected()
        {
            //Arrange
            var str = "SEDC";
            var expStr = "CDE";

            //Act
            var result = sm.Reverse(str);

            //Assert
            Assert.AreEqual(expStr, result);
        }

        [Test]
        [Category("StringMethods")]
        [MaxTime(20000)]
        public void Reverse_StringIsNotEmpty_ResultShouldBeReturnedMaxTo20Seconds()
        {
            //Arrange
            var str = "SEDC";
            var expStr = "CDES";

            //Act
            var result = sm.Reverse(str);

            //Assert
            Assert.AreEqual(expStr, result);
        }

        [Test]
        [Category("StringMethods")]
        [Timeout(20000)]
        public void Reverse_StringIsNotEmpty_ResultShouldBeReturnedMaxTo20SecondsCase1()
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
