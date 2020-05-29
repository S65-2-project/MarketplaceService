using System;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
using MarketplaceService.Repositories;
using Mongo2Go;
using Xunit;

namespace MarketplaceServiceTests.Repository
{
    public class DelegateRepositoryTests : IDisposable
    {
        private readonly MongoDbRunner _mongoDbRunner;
        private readonly IDelegateRepository _delegateRepository;
        
        public DelegateRepositoryTests()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            var settings = new MarketplaceDatabaseSettings
            {
                ConnectionString = _mongoDbRunner.ConnectionString,
                DatabaseName = "IntegrationTests",
                DelegateOfferCollectionName = "TestCollection"
            };
            _delegateRepository = new DelegateRepository(settings);
        }

        public void Dispose()
        {
            _mongoDbRunner?.Dispose();
        }

        [Fact]
        public async Task CreateProduct()
        {
            var product = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title Text",
                Description = "Description Text"
            };

            var result = await _delegateRepository.CreateDelegateOffer(product);
            Assert.NotNull(result);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task DeleteDelegateOfferById()
        {
            //Arrange
            var product1 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _delegateRepository.CreateDelegateOffer(product1);
            await _delegateRepository.CreateDelegateOffer(product2);

            //Act
            await _delegateRepository.DeleteDelegateOffer(product1.Id);
            var result = await _delegateRepository.GetDelegateOffer(product1.Id);
            var resultAll = await _delegateRepository.GetAllDelegateOffers();

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
            var product1 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _delegateRepository.CreateDelegateOffer(product1);
            await _delegateRepository.CreateDelegateOffer(product2);

            //Act
            var result = await _delegateRepository.GetAllDelegateOffers();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(product1.Title, result[0].Title);
            Assert.Equal(product2.Title, result[1].Title);
        }

        [Fact]
        public async Task GetDelegateOfferById()
        {
            //Arrange
            var product1 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _delegateRepository.CreateDelegateOffer(product1);
            await _delegateRepository.CreateDelegateOffer(product2);

            //Act
            var result = await _delegateRepository.GetDelegateOffer(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
            Assert.NotEqual(product2.Title, result.Title);
        }

        [Fact]
        public async Task UpdateDelegateOffer()
        {
            //Arrange
            var product1 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _delegateRepository.CreateDelegateOffer(product1);
            await _delegateRepository.CreateDelegateOffer(product2);

            //Act
            product1.Title = "Title1Edited";

            await _delegateRepository.UpdateDelegateOffer(product1.Id, product1);

            var result = await _delegateRepository.GetDelegateOffer(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
        }
    }
}
