using Xunit;

namespace SEDC.Travel.Service.Tests.DataFixtures
{
    [CollectionDefinition("SearchCollectionData")]
    public class SearchCollection : ICollectionFixture<SearchServiceFixtureData>
    {
    }
}
