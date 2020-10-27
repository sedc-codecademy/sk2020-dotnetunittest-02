using System;
using System.Collections.Generic;

using Moq;
using Xunit;
using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model.DTO;
using SEDC.Travel.Service.Tests.DataFixtures;

namespace SEDC.Travel.Service.Tests._02
{
    [Collection("SearchCollectionData")]
    public class SearchServiceTests //: IClassFixture<SearchServiceFixtureData>
    {
        Mock<ICountryRepository> mockContryRepository;
        Mock<IHotelRepository> mockHotelRepository;
        SearchServiceFixtureData _searchServiceFixtureData;
        public SearchServiceTests(SearchServiceFixtureData searchServiceFixtureData)
        {
            mockContryRepository = new Mock<ICountryRepository>();
            mockHotelRepository = new Mock<IHotelRepository>();
            _searchServiceFixtureData = searchServiceFixtureData;
        }

        #region GetHotels
        [Fact]
        [Trait("Add name", "Add vaue")]
        public void GetHotels_NoExistingHotels_ReturnedResultShouldBeEmpty()
        {
            //Arrange

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.GetHotels();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetHotels_NoExistingHotels_ReturnedResultShouldBeOfTypeListOfHotelDto()
        {
            //Arrange

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.GetHotels();

            //Assert
            Assert.IsType<List<HotelDto>>(result);
        }

        [Fact]
        public void GetHotels_NoExistingHotels_ReturnedResultShouldBeOfTypeListOfHotelDtoCase1()
        {
            //Arrange
            var expected = typeof(List<HotelDto>);

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.GetHotels();

            //Assert
            Assert.IsType(expected, result);
        }

        [Fact]
        public void GetHotels_TwoExistingHotels_ReturnedResultShouldBeTwo()
        {
            //Arrange

            mockHotelRepository.Setup(x => x.GetHotels()).Returns(_searchServiceFixtureData.MockedHotels);
            mockHotelRepository.Setup(x => x.GetHotelCategory(It.IsAny<int>())).Returns(_searchServiceFixtureData.MockedHotelCategory);

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.GetHotels();

            //Assert
            Assert.Equal(_searchServiceFixtureData.MockedHotels.Count, result.Count);
        }

        [Fact]
        public void GetHotels_TwoExistingHotels_ShouldReturnExceptionBecouseOfTheGetCountryNameMethod()
        {
            //Arrange
            var expMsg = "Something went wrong.";

            mockHotelRepository.Setup(x => x.GetHotels()).Returns(_searchServiceFixtureData.MockedHotels);
            mockHotelRepository.Setup(x => x.GetHotelCategory(It.IsAny<int>())).Returns(_searchServiceFixtureData.MockedHotelCategory);
            mockContryRepository.Setup(x => x.GetCountryName(It.IsAny<int>())).Throws(new Exception());

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);

            //Assert
            var result = Assert.Throws<Exception>(() => searchService.GetHotels());
            Assert.Equal(expMsg, result.Message);
        }



        #endregion

        #region GetHotel
        [Fact]
        public void GetHotel_NoExistingHotel_ShouldReturnException()
        {
            //Arrange

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);

            //Assert
            Assert.Throws<Exception>(() => searchService.GetHotel(It.IsAny<int>()));

        }

        #endregion

        [Fact]
        public void MapHotelData_HotelExist_AllDataMustBeCorrect()
        {
            //Arrange

            var mockedCountry = "Macedonia";

            mockContryRepository.Setup(x => x.GetCountryName(It.IsAny<int>())).Returns(mockedCountry);
            mockHotelRepository.Setup(x => x.GetHotelCategory(It.IsAny<int>())).Returns(_searchServiceFixtureData.MockedHotelCategorySecond);
            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.MapHotelData(_searchServiceFixtureData.MockedHotel);

            //Assert
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Id, result.Id);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Code, result.Code);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Name, result.Name);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Description, result.Description);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.City, result.City);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Address, result.Address);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Email, result.Email);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.CountryId, result.CountryId);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.HotelCategoryId, result.HotelCategoryId);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.CountryName, result.CountryName);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.HotelCategory, result.HotelCategory);
            Assert.Equal(_searchServiceFixtureData.MockedExpectedHotel.Web, result.Web);
        }

        [Fact]
        public void MapHotelData_HotelExistWithBadWebAddress_TheWebAddressShouldBeEmpty()
        {
            //Arrange

            var mockedCountry = "Macedonia";

            var expectedHotel = new HotelDto
            {
                Id = 3,
                Code = "03",
                Name = "Diplomat Hotel",
                Description = "Description",
                City = "Ohrid",
                Address = "BB",
                Email = "test@in.com",
                CountryId = 1,
                HotelCategoryId = 1,
                Web = "",
                CountryName = "Macedonia",
                HotelCategory = "4 STARS"
            };
            var hotel = new Hotel { Id = 3, Code = "03", Name = "Diplomat Hotel", Description = "Description", City = "Ohrid", Address = "BB", Email = "test@in.com", CountryId = 1, HotelCategoryId = 1, Web = "sss.mk" };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "4 STARS" };

            mockContryRepository.Setup(x => x.GetCountryName(It.IsAny<int>())).Returns(mockedCountry);
            mockHotelRepository.Setup(x => x.GetHotelCategory(It.IsAny<int>())).Returns(mockedHotelCategory);
            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.MapHotelData(hotel);

            //Assert
            Assert.Null(result.Web);

        }
        
        [Fact]
        public void Search_SearchRequestValid_GetHotelByCountryShouldBeCalledOnce()
        {
            mockHotelRepository.Setup(x => x.GetHotelsByCategory(It.IsAny<int>())).Returns(new List<Hotel>());

            var searhcService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            searhcService.Search(_searchServiceFixtureData.ValidSearchRequest);

            mockHotelRepository.Verify(x => x.GetHotelsByCategory(It.IsAny<int>()), Times.Once);
        }

    }
}
