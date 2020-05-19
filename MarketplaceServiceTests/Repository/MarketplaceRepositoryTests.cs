using System;
using System.Threading.Tasks;
using marketplaceservice.DatastoreSettings;
using marketplaceservice.Domain;
using marketplaceservice.Repositories;
using Mongo2Go;
using Xunit;

namespace marketplaceservicetests.Repository
{
    public class MarketplaceRepositoryTests : IDisposable
    {
        
        private readonly MongoDbRunner _mongoDbRunner;
        private readonly IMarketplaceRepository _marketplaceRepository;
        
        public MarketplaceRepositoryTests()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            var settings = new MarketplaceDatabaseSettings
            {
                ConnectionString = _mongoDbRunner.ConnectionString,
                DatabaseName = "IntegrationTests",
                MarketplaceCollectionName = "TestCollection"
            };
            _marketplaceRepository = new MarketplaceRepository(settings);
        }

        public void Dispose()
        {
            _mongoDbRunner?.Dispose();
        }

        [Fact]
        public async Task CreateProduct()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title Text",
                Description = "Description Text"
            };

            var result = await _marketplaceRepository.CreateProduct(product);
            Assert.NotNull(result);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task DeleteProductById()
        {
            //Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _marketplaceRepository.CreateProduct(product1);
            await _marketplaceRepository.CreateProduct(product2);

            //Act
            await _marketplaceRepository.DeleteProduct(product1.Id);
            var result = await _marketplaceRepository.GetProduct(product1.Id);
            var resultAll = await _marketplaceRepository.GetAll();

            //Assert
            Assert.Null(result);
            Assert.NotNull(resultAll);
            Assert.NotEmpty(resultAll);
            Assert.Single(resultAll);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _marketplaceRepository.CreateProduct(product1);
            await _marketplaceRepository.CreateProduct(product2);

            //Act
            var result = await _marketplaceRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(product1.Title, result[0].Title);
            Assert.Equal(product2.Title, result[1].Title);
        }

        [Fact]
        public async Task GetProductById()
        {
            //Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _marketplaceRepository.CreateProduct(product1);
            await _marketplaceRepository.CreateProduct(product2);

            //Act
            var result = await _marketplaceRepository.GetProduct(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
            Assert.NotEqual(product2.Title, result.Title);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            //Arrange
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _marketplaceRepository.CreateProduct(product1);
            await _marketplaceRepository.CreateProduct(product2);

            //Act
            product1.Title = "Title1Edited";

            await _marketplaceRepository.UpdateProduct(product1.Id, product1);

            var result = await _marketplaceRepository.GetProduct(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
        }
    }
}
