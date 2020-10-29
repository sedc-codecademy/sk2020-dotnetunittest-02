using Moq;
using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Tests.DataFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SEDC.Travel.Service.Tests._03
{
    public class PricingServiceTests : IClassFixture<PricingFixtureData>
    {
        PricingFixtureData _pricingFixtureData;
        Mock<IPricingRepository> _mockedPricingRepository;
        public PricingServiceTests(PricingFixtureData pricingFixtureData)
        {
            _mockedPricingRepository = new Mock<IPricingRepository>();
            _pricingFixtureData = pricingFixtureData;
        }


        [Fact]
        [Trait("Category", "BUGS")]
        public void GetPricingPercent_PeriodsAreNotDefined_ResultShouldBe13()
        {
            var expResult = 13;
            var fromDate = DateTime.Now.AddDays(10);
            var toDate = DateTime.Now.AddDays(15);
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(new List<Pricing>());

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.GetPricingPercent(fromDate, toDate);

            Assert.Equal(expResult, result);
        }

        [Fact]
        [Trait("Category", "BUGS")]
        public void GetPricingPercent_PeriodsAreDefinedAndHaveMatchWithinOnePeriod_ResultShouldBe13()
        {
            var expResult = 10;
            var fromDate = new DateTime(2020, 01, 15);
            var toDate = new DateTime(2020, 01, 25);
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.GetPricingPercent(fromDate, toDate);

            Assert.Equal(expResult, result);
        }

        [Fact]
        [Trait("Category", "BUGS")]
        public void GetPricingPercent_PeriodsAreDefinedAndHaveOverlaping_ResultShouldBe13()
        {
            var expResult = 12;
            var fromDate = new DateTime(2020, 03, 25);
            var toDate = new DateTime(2020, 04, 05);
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.GetPricingPercent(fromDate, toDate);

            Assert.Equal(expResult, result);
        }

        [Fact]
        [Trait("Category", "BUGS")]
        public void GetPricingPercent_PeriodsAreDefinedHaveOnlyMatchForOne_ResultShouldBe13()
        {
            var expResult = 13;
            var fromDate = new DateTime(2020, 12, 25);
            var toDate = new DateTime(2021, 01, 05);
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.GetPricingPercent(fromDate, toDate);

            Assert.Equal(expResult, result);
        }

        [Theory]
        [InlineData("2020-01-15", "2020-01-25", 100, 110)]
        [InlineData("2020-03-25", "2020-04-05", 100, 112)]
        [InlineData("2020-12-25", "2021-01-05", 100, 113)]
        [Trait("Category", "BUGS")]
        public void CalculatePrice_HasValidPeriods_ResultShouldBeCorrect(string checkIn, string checkOut, decimal price, decimal expResult)
        {
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.CalculatePrice(DateTime.Parse(checkIn), DateTime.Parse(checkOut), price);

            Assert.Equal(expResult, result);
        }

        [Theory]
        [MemberData(nameof(PricingTestMemberCases))]
        [Trait("Category", "BUGS")]
        public void CalculatePrice_WithMemberDataHasValidPeriods_ResultShouldBeCorrect(DateTime checkIn, DateTime checkOut, decimal price, decimal expResult)
        {
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.CalculatePrice(checkIn, checkOut, price);

            Assert.Equal(expResult, result);
        }

        [Theory]
        [ClassData(typeof(PricingTestData))]
        [Trait("Category", "BUGS")]
        public void CalculatePrice_WithClassDataHasValidPeriods_ResultShouldBeCorrect(PriceTestCases testCase)
        {
            _mockedPricingRepository.Setup(x => x.GetPricings()).Returns(_pricingFixtureData.MockedPricings);

            var pricingSerice = new PricingService(_mockedPricingRepository.Object);
            var result = pricingSerice.CalculatePrice(testCase.CheckIn, testCase.CheckOut, testCase.Price);

            Assert.Equal(testCase.ExpPrice, result);
        }



        public static IEnumerable<object[]> PricingTestMemberCases = new List<object[]>
        {
            new object[] { new DateTime(2020, 01, 15) , new DateTime(2020, 01, 25), 100,  110 },
            new object[] { new DateTime(2020, 03, 25) , new DateTime(2020, 04, 15), 100,  112 },
            new object[] { new DateTime(2020, 12, 25) , new DateTime(2021, 01, 05), 100,  113 },
        };
    }
}
