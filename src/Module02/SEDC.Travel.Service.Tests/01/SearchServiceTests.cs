using System;
using System.Collections.Generic;

using Moq;
using Xunit;
using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model.DTO;

namespace SEDC.Travel.Service.Tests._01
{
    public class SearchServiceTests
    {
        #region GetHotels
        [Fact]
        public void GetHotels_NoExistingHotels_ReturnedResultShouldBeEmpty()
        {
            //Arrange
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

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
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

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
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();
            var expected= typeof(List<HotelDto>);

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
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

            var mockedHotels = new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };
            var categoryId = 1;


            mockHotelRepository.Setup(x => x.GetHotels()).Returns(mockedHotels);
            mockHotelRepository.Setup(x => x.GetHotelCategory(categoryId)).Returns(mockedHotelCategory);

            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.GetHotels();

            //Assert
            Assert.Equal(mockedHotels.Count, result.Count);
        }

        [Fact]
        public void GetHotels_TwoExistingHotels_ShouldReturnExceptionBecouseOfTheGetCountryNameMethod()
        {
            //Arrange
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

            var mockedHotels = new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };
            var categoryId = 1;
            var expMsg = "Something went wrong.";


            mockHotelRepository.Setup(x => x.GetHotels()).Returns(mockedHotels);
            mockHotelRepository.Setup(x => x.GetHotelCategory(categoryId)).Returns(mockedHotelCategory);
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
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();
            

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
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

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
                Web = "http://diplomat.mk",
                CountryName = "Macedonia",
                HotelCategory = "4 STARS"
            };
            var hotel = new Hotel { Id = 3, Code = "03", Name = "Diplomat Hotel", Description = "Description", City = "Ohrid", Address = "BB", Email = "test@in.com", CountryId = 1, HotelCategoryId = 1, Web = "http://diplomat.mk" };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "4 STARS" };

            mockContryRepository.Setup(x => x.GetCountryName(It.IsAny<int>())).Returns(mockedCountry);
            mockHotelRepository.Setup(x => x.GetHotelCategory(It.IsAny<int>())).Returns(mockedHotelCategory);
            //Act
            var searchService = new SearchService(mockContryRepository.Object, mockHotelRepository.Object);
            var result = searchService.MapHotelData(hotel);

            //Assert
            Assert.Equal(expectedHotel.Id, result.Id);
            Assert.Equal(expectedHotel.Code, result.Code);
            Assert.Equal(expectedHotel.Name, result.Name);
            Assert.Equal(expectedHotel.Description, result.Description);
            Assert.Equal(expectedHotel.City, result.City);
            Assert.Equal(expectedHotel.Address, result.Address);
            Assert.Equal(expectedHotel.Email, result.Email);
            Assert.Equal(expectedHotel.CountryId, result.CountryId);
            Assert.Equal(expectedHotel.HotelCategoryId, result.HotelCategoryId);
            Assert.Equal(expectedHotel.CountryName, result.CountryName);
            Assert.Equal(expectedHotel.HotelCategory, result.HotelCategory);
            Assert.Equal(expectedHotel.Web, result.Web);
        }

        [Fact]
        public void MapHotelData_HotelExistWithBadWebAddress_TheWebAddressShouldBeEmpty()
        {
            //Arrange
            var mockContryRepository = new Mock<ICountryRepository>();
            var mockHotelRepository = new Mock<IHotelRepository>();

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

    }
}
