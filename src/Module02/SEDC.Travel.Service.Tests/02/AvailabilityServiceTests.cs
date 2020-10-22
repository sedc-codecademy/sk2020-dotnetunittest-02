using SEDC.Travel.Service.Tests.DataFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SEDC.Travel.Service.Tests._02
{
    [Collection("SearchCollectionData")]
    public class AvailabilityServiceTests
    {
        SearchServiceFixtureData _searchServiceFixtureData;
        public AvailabilityServiceTests(SearchServiceFixtureData searchServiceFixtureData)
        {
            _searchServiceFixtureData = searchServiceFixtureData;
        }
    }
}
