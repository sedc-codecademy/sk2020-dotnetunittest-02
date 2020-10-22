using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Test, Sequential]
        [Author("SEDC", "sedc@in.com")]
        public void IsLeapYear_SequentialValuesYearIsNotLeap_TheResultShouldBeFalse([Values(1997, 1998, 1996)] int year, [Values(false, false, true)] bool expectedResult)
        {
            //Arrange

            //Act
            var result = bm.IsLeapYear(year);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1996, true)]
        [TestCase(1997, false)]
        [TestCase(1998, false)]
        [TestCase(1999, false)]
        public void IsLeapYear_WithGivenTestCases_TheReturnedResultShouldBeCorrect(int year, bool expResult)
        {
            var result = bm.IsLeapYear(year);

            Assert.AreEqual(expResult, result);
        }

        [TestCase(1996, ExpectedResult = true)]
        [TestCase(1997, ExpectedResult = false)]
        [TestCase(1998, ExpectedResult = false)]
        [TestCase(1999, ExpectedResult = false)]
        public bool IsLeapYear_WithGivenTestCasesWithExpectedResult_TheReturnedResultShouldBeCorrect(int year)
        {
            return bm.IsLeapYear(year);
        }

        [TestCaseSource("TestCase")]
        public void IsLeapYear_WithGivenTestCaseSource_TheReturnedResultShouldBeCorrect(int year, bool expResult)
        {
            var result = bm.IsLeapYear(year);

            Assert.AreEqual(expResult, result);
        }

        [TestCaseSource(typeof(BoolCaseData), "TestCases")]
        public void IsLeapYear_WithGivenTestCaseSourceWithClass_TheReturnedResultShouldBeCorrect(int year, bool expResult)
        {
            var result = bm.IsLeapYear(year);

            Assert.AreEqual(expResult, result);
        }

        [TestCaseSource("CsvData")]
        public bool IsLeapYear_WithGivenTestCaseSourceInCsvFile_TheReturnedResultShouldBeCorrect(int year)
        {
            return bm.IsLeapYear(year);
        }

        public static List<TestCaseData> CsvData
        {
            get
            {
                var testCases = new List<TestCaseData>();

                using (var fs = File.OpenRead(@"C:\Users\j_mar\Desktop\GitProjects\sedc-codecademy\group02\src\Module01\SEDC.UnitTesting.SUT.Tests\04\test.csv"))
                using (var sr = new StreamReader(fs))
                {
                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ',' },
                                StringSplitOptions.None);

                            int year = Convert.ToInt32(split[0]);
                            bool expected = Convert.ToBoolean(split[1]);

                            var testCase = new TestCaseData(year).Returns(expected);
                            testCases.Add(testCase);
                        }
                    }
                }

                return testCases;
            }
        }

        static object[] TestCase =
        {
            new object[] { 1996 , true},
            new object[] { 1999 , false},
            new object[] { 2001 , false},
        };


        //TODO: Add test with[SetCulture]
    }
}
