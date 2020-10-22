using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Travel.Service.Tests.DataFixtures
{
    public class SearchServiceFixtureData
    {
        public List<Hotel> MockedHotels { get; set; }
        public HotelCategory MockedHotelCategory { get; set; }
        public HotelCategory MockedHotelCategorySecond { get; set; }
        public Hotel MockedHotel { get; set; }
        public HotelDto MockedExpectedHotel { get; set; }

        public SearchServiceFixtureData()
        {
            MockedHotels = SetMockedHotels();
            MockedHotelCategory = SetMockedHotelCategory();
            MockedHotel = SetMockedHotel();
            MockedExpectedHotel = SetMockedExpectedHotel();
            MockedHotelCategorySecond = SetMockedHotelCategorySecond();
        }

        private List<Hotel> SetMockedHotels()
        {
            return new List<Hotel>
            {
                new Hotel { Id=1, Code="01", Name="Hotel Royal Skopje", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
                new Hotel { Id=2, Code="02", Name="Hotel Royal", Description="Des", City="Skopje", Address="bb", HotelCategoryId=1, Web="http://rojal.mk" },
            };
        }

        private HotelCategory SetMockedHotelCategory()
        {
            return new HotelCategory { Id = 1, Code = "03", Description = "3 Stars" };
        }

        private HotelCategory SetMockedHotelCategorySecond()
        {
            return new HotelCategory { Id = 1, Code = "04", Description = "4 STARS" };
        }

        private Hotel SetMockedHotel()
        {
            return new Hotel { Id = 3, Code = "03", Name = "Diplomat Hotel", Description = "Description", City = "Ohrid", Address = "BB", Email = "test@in.com", CountryId = 1, HotelCategoryId = 1, Web = "http://diplomat.mk" };
        }
        private HotelDto SetMockedExpectedHotel()
        {
            return new HotelDto
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
        }
    }
}
