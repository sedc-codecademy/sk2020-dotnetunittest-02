using NSubstitute;
using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SEDC.Travel.Service.CustomTests._01
{
    public class SearchServiceTestsOne
    {
        ICountryRepository mockContryRepository;
        IHotelRepository mockHotelRepository;

        public SearchServiceTestsOne()
        {
            mockContryRepository = Substitute.For<ICountryRepository>();
            mockHotelRepository = Substitute.For<IHotelRepository>();
        }

        [Fact]
        public void GetHotels_NoExistingHotels_ReturnedResultShouldBeEmpty()
        {
            //Arrange

            //Act
            var searchService = new SearchService(mockContryRepository, mockHotelRepository);
            var result = searchService.GetHotels();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetHotels_TwoExistingHotels_ReturnedResultShouldBeTwo()
        {
            //Arrange

            var mockedHotels = new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };
            var categoryId = 1;

            mockHotelRepository.GetHotels().Returns(mockedHotels);
            mockHotelRepository.GetHotelCategory(categoryId).Returns(mockedHotelCategory);

            //Act
            var searchService = new SearchService(mockContryRepository, mockHotelRepository);
            var result = searchService.GetHotels();

            //Assert
            Assert.Equal(mockedHotels.Count, result.Count);
        }

        [Fact]
        public void GetHotels_TwoExistingHotelsWithArg_ReturnedResultShouldBeTwo()
        {
            //Arrange
            var mockContryRepository = Substitute.For<ICountryRepository>();
            var mockHotelRepository = Substitute.For<IHotelRepository>();

            var mockedHotels = new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };

            mockHotelRepository.GetHotels().Returns(mockedHotels);
            mockHotelRepository.GetHotelCategory(Arg.Any<int>()).Returns(mockedHotelCategory);

            //Act
            var searchService = new SearchService(mockContryRepository, mockHotelRepository);
            var result = searchService.GetHotels();

            //Assert
            Assert.Equal(mockedHotels.Count, result.Count);
        }

        [Fact]
        public void GetHotels_TwoExistingHotels_ShouldReturnExceptionBecouseOfTheGetCountryNameMethod()
        {
            //Arrange
            var mockContryRepository = Substitute.For<ICountryRepository>();
            var mockHotelRepository = Substitute.For<IHotelRepository>();

            var mockedHotels = new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
            var mockedHotelCategory = new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };
            var categoryId = 1;
            var expMsg = "Something went wrong.";


            mockHotelRepository.GetHotels().Returns(mockedHotels);
            mockHotelRepository.GetHotelCategory(categoryId).Returns(mockedHotelCategory);
            mockContryRepository.GetCountryName(1).Returns(x => { throw new Exception(expMsg); });

            //Act
            var searchService = new SearchService(mockContryRepository, mockHotelRepository);

            //Assert
            var result = Assert.Throws<Exception>(() => searchService.GetHotels());
            Assert.Equal(expMsg, result.Message);

        }


        [Fact]
        public void Search_SearchRequestValid_GetHotelByCountryShouldBeCalledOnce()
        {
            var mockContryRepository = Substitute.For<ICountryRepository>();
            var mockHotelRepository = Substitute.For<IHotelRepository>();

            var validSearchRequest = new SearchRequest
            {
                FromDate = DateTime.Now.AddDays(10),
                ToDate = DateTime.Now.AddDays(15),
                Adults = 2,
                Children = 4,
                Rooms = 2,
                HotelCategory = 1
            };


            mockHotelRepository.GetHotelsByCategory(Arg.Any<int>()).Returns(new List<Hotel>());

            var searhcService = new SearchService(mockContryRepository, mockHotelRepository);
            searhcService.Search(validSearchRequest);

            //mockHotelRepository.Verify(x => x.GetHotelsByCategory(It.IsAny<int>()), Times.Once);
            mockHotelRepository.Received().GetHotels();
            mockHotelRepository.Received(1).GetHotels();
        }

    }
}
