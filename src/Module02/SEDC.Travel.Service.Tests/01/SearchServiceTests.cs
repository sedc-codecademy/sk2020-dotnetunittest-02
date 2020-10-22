using Moq;
using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SEDC.Travel.Service.Tests._01
{
    public class SearchServiceTests
    {
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

        //var expectedHotel = new HotelDto
        //{
        //    Id = 3,
        //    Code = "03",
        //    Name = "Diplomat Hotel",
        //    Description = "Description",
        //    City = "Ohrid",
        //    Address = "BB",
        //    Email = "test@in.com",
        //    CountryId = 1,
        //    HotelCategoryId = 1,
        //    Web = "http://diplomat.mk",
        //    CountryName = "Macedonia",
        //    HotelCategory = "4 STARS"
        //};
        //var hotel = new Hotel { Id = 3, Code = "03", Name = "Diplomat Hotel", Description = "Description", City = "Ohrid", Address = "BB", Email = "test@in.com", CountryId = 1, HotelCategoryId = 1, Web = "http://diplomat.mk" };
        //var hotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "4 STARS" };


        //var expectedHotel = new HotelDto
        //{
        //    Id = 3,
        //    Code = "03",
        //    Name = "Diplomat Hotel",
        //    Description = "Description",
        //    City = "Ohrid",
        //    Address = "BB",
        //    Email = "test@in.com",
        //    CountryId = 1,
        //    HotelCategoryId = 1,
        //    Web = "",
        //    CountryName = "Macedonia",
        //    HotelCategory = "4 STARS"
        //};
        //var hotel = new Hotel { Id = 3, Code = "03", Name = "Diplomat Hotel", Description = "Description", City = "Ohrid", Address = "BB", Email = "test@in.com", CountryId = 1, HotelCategoryId = 1, Web = "https://diplomat" };
        //var hotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "4 STARS" };

    }
}
