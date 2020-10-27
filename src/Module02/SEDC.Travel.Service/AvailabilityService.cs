﻿using System.Collections.Generic;

using SEDC.Travel.Domain.Contract;
using SEDC.Travel.Service.Contract;
using SEDC.Travel.Service.Model;
using SEDC.Travel.Service.ThirdParty;
using SEDC.Travel.Service.Model.ThirdParty;
using System;

namespace SEDC.Travel.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelAvailability _hotelAvailability;
        private readonly IPricingService _pricingService;

        public AvailabilityService(IHotelRepository hotelRepository, IHotelAvailability hotelAvailability, IPricingService pricingService)
        {
            _hotelRepository = hotelRepository;
            _hotelAvailability = hotelAvailability;
            _pricingService = pricingService;
        }

        public AvailabilityResponse CheckAvailability(SearchRequest request, List<string> hotelCodes)
        {
            ValidatedSearchRequest(request);

            var hoteSearchRequest = Request(request);

            var response = _hotelAvailability.SearchHotelAvailability(hoteSearchRequest);

            return ProcessAvailability(response);

        }

        public AvailabilityResponse ProcessAvailability(HotelAvailabilityResponse response)
        {
            var result = new AvailabilityResponse();

            result.CheckIn = response.CheckIn;
            result.CheckOut = response.CheckOut;
            result.Count = response.Count;
            var availableHotels = new List<AvailableHotel>();
            foreach (var hotels in response.AvailableHotels)
            {
                var hotel = new AvailableHotel();

                hotel.Code = hotels.Code;

                var rooms = new List<AvailableRoom>();
                foreach (var room in hotels.AvailableRooms)
                {
                    var data = new AvailableRoom();
                    data.Id = room.Id;
                    data.Code = room.Code;
                    data.Price = room.Price;
                    data.NewPrice = _pricingService.CalculatePrice(response.CheckIn, response.CheckOut, room.Price);
                    rooms.Add(data);
                }
                hotel.AvailableRooms = rooms;

                availableHotels.Add(hotel);

            }

            result.AvailableHotels = availableHotels;


            return result;
        }

        public void ValidatedSearchRequest(SearchRequest searchRequest)
        {
            if (searchRequest.FromDate == null)
            {
                throw new Exception("FromDate must have value");
            }
            if (searchRequest.ToDate == null)
            {
                throw new Exception("ToDate must have value");
            }

            if (searchRequest.ToDate < searchRequest.FromDate)
            {
                throw new Exception("ToDate must be bigger than FromDate");
            }

            if (searchRequest.Adults < 1)
            {
                throw new Exception("Must have at least one Adult");
            }

            if (searchRequest.Adults < searchRequest.Rooms)
            {
                throw new Exception("Must have at least one Adult per room");
            }
        }

        public HotelAvailabilityRequest Request(SearchRequest searchRequest)
        {
            var request = new HotelAvailabilityRequest();
            request.CheckIn = searchRequest.FromDate.Value;
            request.CheckOut = searchRequest.ToDate.Value;
            request.Nights = (searchRequest.ToDate.Value - searchRequest.FromDate.Value).Days;

            request.Adults = searchRequest.Adults;
            request.Children = searchRequest.Children;
            request.Infants = searchRequest.Infants;
            request.Rooms = searchRequest.Rooms;
            request.HotelCategory = searchRequest.HotelCategory;

            return request;
        }

    }
}
