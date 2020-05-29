using System;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
using MarketplaceService.Repositories;
using Mongo2Go;
using Xunit;

namespace MarketplaceServiceTests.Repository
{
public class DAppRepositoryTests : IDisposable
    {
        private readonly MongoDbRunner _mongoDbRunner;
        private readonly IDAppRepository _dAppRepository;
        
        public DAppRepositoryTests()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            var settings = new MarketplaceDatabaseSettings
            {
                ConnectionString = _mongoDbRunner.ConnectionString,
                DatabaseName = "IntegrationTests",
                DAppOfferCollectionName = "TestCollection"
            };
            _dAppRepository = new DAppRepository(settings);
        }

        public void Dispose()
        {
            _mongoDbRunner?.Dispose();
        }

        [Fact]
        public async Task CreateProduct()
        {
            var product = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title Text",
                Description = "Description Text"
            };

            var result = await _dAppRepository.CreateDAppOffer(product);
            Assert.NotNull(result);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task DeleteDAppOfferById()
        {
            //Arrange
            var product1 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _dAppRepository.CreateDAppOffer(product1);
            await _dAppRepository.CreateDAppOffer(product2);

            //Act
            await _dAppRepository.DeleteDAppOffer(product1.Id);
            var result = await _dAppRepository.GetDAppOffer(product1.Id);
            var resultAll = await _dAppRepository.GetAllDAppOffers();

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
            var product1 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _dAppRepository.CreateDAppOffer(product1);
            await _dAppRepository.CreateDAppOffer(product2);

            //Act
            var result = await _dAppRepository.GetAllDAppOffers();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(product1.Title, result[0].Title);
            Assert.Equal(product2.Title, result[1].Title);
        }

        [Fact]
        public async Task GetDAppOfferById()
        {
            //Arrange
            var product1 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description Text"
            };

            var product2 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description Text"
            };

            await _dAppRepository.CreateDAppOffer(product1);
            await _dAppRepository.CreateDAppOffer(product2);

            //Act
            var result = await _dAppRepository.GetDAppOffer(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
            Assert.NotEqual(product2.Title, result.Title);
        }

        [Fact]
        public async Task UpdateDAppOffer()
        {
            //Arrange
            var product1 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title1",
                Description = "Description1"
            };

            var product2 = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = "Title2",
                Description = "Description2"
            };

            await _dAppRepository.CreateDAppOffer(product1);
            await _dAppRepository.CreateDAppOffer(product2);

            //Act
            product1.Title = "Title1Edited";

            await _dAppRepository.UpdateDAppOffer(product1.Id, product1);

            var result = await _dAppRepository.GetDAppOffer(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(product1.Title, result.Title);
        }
    }
}