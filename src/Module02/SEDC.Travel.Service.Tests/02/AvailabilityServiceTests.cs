using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using SEDC.Travel.Service.Tests.DataFixtures;
using SEDC.Travel.Domain.Contract;
using Moq;
using SEDC.Travel.Service.ThirdParty;
using SEDC.Travel.Service.Contract;
using SEDC.Travel.Service.Model.ThirdParty;
using System.Linq;
using FluentAssertions;

[assembly: CollectionBehavior(MaxParallelThreads =5)]

namespace SEDC.Travel.Service.Tests._02
{
    [Collection("SearchCollectionData")]
    public class AvailabilityServiceTests : IClassFixture<SearchFixtureData>
    {
        SearchServiceFixtureData _searchServiceFixtureData;
        SearchFixtureData _searchFixtureData;
        Mock<IHotelRepository> _mockedHotelRepository;
        Mock<IHotelAvailability> _mockedHotelAvailability;
        Mock<IPricingService> _mockedPricingService;

        public AvailabilityServiceTests(SearchServiceFixtureData searchServiceFixtureData, SearchFixtureData searchFixtureData)
        {
            _searchServiceFixtureData = searchServiceFixtureData;
            _searchFixtureData = searchFixtureData;
            _mockedHotelRepository = new Mock<IHotelRepository>();
            _mockedHotelAvailability = new Mock<IHotelAvailability>();
            _mockedPricingService = new Mock<IPricingService>();
        }

        [Fact]
        public void ValidateSearchRequest_InvalidDateFrom_ShouldThrowException()
        {
            //Arrange

            //Act
            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);

            //Arrange
            Assert.Throws<Exception>(() => availabilityService.ValidatedSearchRequest(_searchFixtureData.RequestCase1));
        }

        [Fact]
        public void ValidateSearchRequest_InvalidToDate_ShouldThrowException()
        {
            //Arrange
            var expMsg = "ToDate must have value";
            //Act
            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);

            //Arrange
            var result = Assert.Throws<Exception>(() => availabilityService.ValidatedSearchRequest(_searchFixtureData.RequestCase2));
            Assert.Equal(expMsg, result.Message);
        }

        [Fact]
        public void ValidateSearchRequest_FromDateIsBiggerThanToDate_ShouldThrowException()
        {
            //Arrange

            //Act
            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);

            //Arrange
            Assert.Throws<Exception>(() => availabilityService.ValidatedSearchRequest(_searchFixtureData.RequestCase3));
        }

        [Fact]
        public void ValidateSearchRequest_OneAdultAndTwoRooms_ShouldThrowException()
        {
            //Arrange

            //Act
            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);

            //Arrange
            Assert.Throws<Exception>(() => availabilityService.ValidatedSearchRequest(_searchFixtureData.RequestCase4));
        }

        [Fact]
        public void ValidateSearchRequest_OnlyChildren_ShouldThrowException()
        {
            //Arrange

            //Act
            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);

            //Arrange
            Assert.Throws<Exception>(() => availabilityService.ValidatedSearchRequest(_searchFixtureData.RequestCase5));
        }

        [Fact]
        public void Request_SearchRequestIsValid_TheReturnedResultShouldBeOfTypeHotelAvailabilityRequest()
        {

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.Request(_searchFixtureData.ValidRequestCase1);

            Assert.IsType<HotelAvailabilityRequest>(result);

        }

        [Fact]
        public void Request_SearchRequestIsValid_ShouldHaveCorrectData()
        {

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.Request(_searchFixtureData.ValidRequestCase1);

            Assert.Equal(_searchFixtureData.HotelAvailabilityRequestCase1.CheckIn, result.CheckIn);
            Assert.Equal(_searchFixtureData.HotelAvailabilityRequestCase1.CheckOut, result.CheckOut);
            Assert.Equal(_searchFixtureData.HotelAvailabilityRequestCase1.Nights, result.Nights);
            Assert.Equal(_searchFixtureData.HotelAvailabilityRequestCase1.Adults, result.Adults);
            Assert.Equal(_searchFixtureData.HotelAvailabilityRequestCase1.Children, result.Children);

        }

        [Fact]
        public void CheckAvailability_ValidSearchRequestAndTheServiceReturnedTwoHotels_ResultShouldContainTwoHotels()
        {
            _mockedHotelAvailability.Setup(x => x.SearchHotelAvailability(It.IsAny<HotelAvailabilityRequest>())).Returns(_searchServiceFixtureData.MockedHotelAvailabilityResponse);
            int expCount = 2;

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.CheckAvailability(_searchFixtureData.ValidRequestCase1, It.IsAny<List<string>>());

            Assert.Equal(expCount, result.Count);

        }

        [Fact]
        public void CheckAvailability_ValidSearchRequestAndTheServiceReturnedTwoHotels_TheNewPriceShouldHaveValue()
        {
            _mockedHotelAvailability.Setup(x => x.SearchHotelAvailability(It.IsAny<HotelAvailabilityRequest>())).Returns(_searchServiceFixtureData.MockedHotelAvailabilityResponse);
            _mockedPricingService.Setup(x => x.CalculatePrice(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>())).Returns(150);

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.CheckAvailability(_searchFixtureData.ValidRequestCase1, It.IsAny<List<string>>());

            foreach (var item in result.AvailableHotels)
            {
                item.AvailableRooms.Select(x => x.NewPrice).Should().NotContainNulls();
                //foreach (var room in item.AvailableRooms)
                //{
                //    Assert.True(room.NewPrice > 0);
                //}
            }

        }

        [Fact]
        public void CheckAvailability_ValidSearchRequestAndTheServiceReturnedTwoHotelsWithSetupSequence_TheNewPriceShouldHaveValue()
        {
            _mockedHotelAvailability.Setup(x => x.SearchHotelAvailability(It.IsAny<HotelAvailabilityRequest>())).Returns(_searchServiceFixtureData.MockedHotelAvailabilityResponse);
            _mockedPricingService.SetupSequence(x => x.CalculatePrice(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
                .Returns(120)
                .Returns(150);

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.CheckAvailability(_searchFixtureData.ValidRequestCase1, It.IsAny<List<string>>());

            foreach (var item in result.AvailableHotels)
            {
                item.AvailableRooms.Select(x => x.NewPrice).Should().NotContainNulls();
                foreach (var room in item.AvailableRooms)
                {
                    Assert.True(room.NewPrice > 0);
                }
            }

        }

        [Fact]
        public void CheckAvailability_ValidSearchRequestAndTheServiceReturnedTwoHotelsWithSetupSequence_TheNewPriceShouldBeBiggerThanPrice()
        {
            _mockedHotelAvailability.Setup(x => x.SearchHotelAvailability(It.IsAny<HotelAvailabilityRequest>())).Returns(_searchServiceFixtureData.MockedHotelAvailabilityResponse);
            _mockedPricingService.SetupSequence(x => x.CalculatePrice(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
                .Returns(120)
                .Returns(180);

            var availabilityService = new AvailabilityService(_mockedHotelRepository.Object, _mockedHotelAvailability.Object, _mockedPricingService.Object);
            var result = availabilityService.CheckAvailability(_searchFixtureData.ValidRequestCase1, It.IsAny<List<string>>());

            foreach (var item in result.AvailableHotels)
            {
                //item.AvailableRooms.Select(x => x.NewPrice).Should().HaveCountGreaterThan();
                foreach (var room in item.AvailableRooms)
                {
                    Assert.True(room.NewPrice > room.Price);
                }
            }

        }


    }
}
