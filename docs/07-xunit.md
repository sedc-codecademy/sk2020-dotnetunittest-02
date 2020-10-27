# Sharing data between tests
  - Constructor and Dispose : in order to have a code that will be run for every test use the constructor and dispose. Constructor and dispose will be called for every test
  because XUnit creates a new instance of the test class for every test.
  - Class Fixture : in order to share single object instance (where you will have a setup/mocked data) that will be used for all test in a test class use Class Fixture
    - Create new class that will be the fixture class and implement IDisposable if needed for cleanup
    ```csharp  
    public class HotelFixtureData
    {
        public Hotel MockedHotel { get; private set; }

        public HotelFixtureData()
        {
            MockedHotel = SetMockedHotel();
            MockedExpectedHotel = SetMockedExpectedHotel();
            HotelList = SetHotelList();
            MockedHotelCategory = SetHotelCategory();
        }

        private Hotel SetMockedHotel()
        {
            var hotel = new Hotel
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
                Web = "http://diplomat.mk"
            };

            return hotel;
        }
    }
    ```
    - Add IClassFixture<> to the test class and in order to access the data add in the constructor
    ```csharp  
    public class SearchServiceTest : IClassFixture<HotelFixtureData>
    {
        HotelFixtureData _hotelFixtureData;

        public SearchServiceTest(HotelFixtureData hotelFixtureData)
        {
            _hotelFixtureData = hotelFixtureData;
        }
    }
    ```
  - Collection Fixture : in order to share a Class Fixture among couple of test classs then use Collection Fixture
    - Create new class that will be the fixture class and implement IDisposable if needed for cleanup
    ```csharp  
    public class HotelFixtureData
    {
        public Hotel MockedHotel { get; private set; }

        public HotelFixtureData()
        {
            MockedHotel = SetMockedHotel();
            MockedExpectedHotel = SetMockedExpectedHotel();
            HotelList = SetHotelList();
            MockedHotelCategory = SetHotelCategory();
        }

        private Hotel SetMockedHotel()
        {
            var hotel = new Hotel
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
                Web = "http://diplomat.mk"
            };

            return hotel;
        }
    }
    ```
    - Create collection definition class and decoratr it with the [CollectionDefinition] attribute, add unique name that will identify the test collection. Add ICollectionFixture<> to the collection definition class.
     ```csharp  
    [CollectionDefinition("HotelCollection")]
    public class HotelCollection : ICollectionFixture<HotelFixtureData>
    {
      // This class has no code, and is never created. Its purpose is simply
      // to be the place to apply [CollectionDefinition] and all the
      // ICollectionFixture<> interfaces.
    }
     ```  
    -Add the [Collection] attribute to all the test classes and in order to access the data add in the constructor
    ```csharp  
    [Collection("HotelCollection")]
    public class AvailabilityServiceTest
    {
        HotelFixtureData _hotelFixtureData;
        public AvailabilityServiceTest(HotelFixtureData hotelFixtureData)
        {
            _hotelFixtureData = hotelFixtureData;
        }
    }
    [Collection("HotelCollection")]
    public class SearchServiceTest
    {
        HotelFixtureData _hotelFixtureData;

        public SearchServiceTest(HotelFixtureData hotelFixtureData)
        {
            _hotelFixtureData = hotelFixtureData;
        }
    }
    
    ```
