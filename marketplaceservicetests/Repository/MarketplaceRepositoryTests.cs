using System;
using marketplaceservice.DatastoreSettings;
using marketplaceservice.Domain;
using marketplaceservice.Repositories;
using Mongo2Go;
using Xunit;

namespace marketplaceservicetests.Repository
{
    public class MarketplaceRepositoryTests
    {
        private readonly MongoDbRunner _mongoDbRunner;
        private readonly IMarketplaceRepository _marketplaceRepository;

        public MarketplaceRepositoryTests()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            var settings = new MarketplaceDatabaseSettings()
            {
                ConnectionString = _mongoDbRunner.ConnectionString,
                DatabaseName = "IntegrationTests",
                MarketplaceCollectionName = "TestCollection"
            };
            _marketplaceRepository = new MarketplaceRepository(settings);
            
        }

        [Fact]
        public async void CreateProductTest()
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Title = "Title Text",
                Description = "Description Text",
            };

            var result = await _marketplaceRepository.CreateProduct(product);
            Assert.NotNull(result);
            Assert.Equal(product, result);
        }
    }
}