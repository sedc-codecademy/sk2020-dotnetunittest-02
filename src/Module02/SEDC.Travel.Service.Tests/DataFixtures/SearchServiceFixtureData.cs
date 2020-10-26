using System;
using System.Collections.Generic;
using System.Text;

using SEDC.Travel.Domain.Model;
using SEDC.Travel.Service.Model.DTO;
using SEDC.Travel.Service.Model.ThirdParty;


namespace SEDC.Travel.Service.Tests.DataFixtures
{
    public class SearchServiceFixtureData
    {
        public List<Hotel> MockedHotels { get; set; }
        public HotelCategory MockedHotelCategory { get; set; }
        public HotelCategory MockedHotelCategorySecond { get; set; }
        public Hotel MockedHotel { get; set; }
        public HotelDto MockedExpectedHotel { get; set; }

        public HotelAvailabilityResponse MockedHotelAvailabilityResponse { get; set; }

        public SearchServiceFixtureData()
        {
            MockedHotels = SetMockedHotels();
            MockedHotelCategory = SetMockedHotelCategory();
            MockedHotel = SetMockedHotel();
            MockedExpectedHotel = SetMockedExpectedHotel();
            MockedHotelCategorySecond = SetMockedHotelCategorySecond();
            MockedHotelAvailabilityResponse = SetMockedHotelAvailabilityResponse();
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

        private HotelAvailabilityResponse SetMockedHotelAvailabilityResponse()
        {
            var response = new HotelAvailabilityResponse();
            response.Count = 2;
            response.CheckIn = DateTime.Now.AddDays(30);
            response.CheckOut = DateTime.Now.AddDays(35);

            var availableHotels = new List<HotelResponse>();

            var availableHotelFirst = new HotelResponse();
            availableHotelFirst.Code = "01";
            availableHotelFirst.AvailableRooms = new List<HotelAvailableRoom> {
                 new HotelAvailableRoom { Id = 1, Code = "ROM_01", Price = 100},
            };
            availableHotels.Add(availableHotelFirst);


            var availableHotelSecond = new HotelResponse();
            availableHotelSecond.Code = "02";
            availableHotelSecond.AvailableRooms = new List<HotelAvailableRoom> {
                 new HotelAvailableRoom { Id = 3, Code = "ROM_03", Price = 150},
            };
            availableHotels.Add(availableHotelSecond);


            response.AvailableHotels = availableHotels;
            return response;
        }
    }
}
