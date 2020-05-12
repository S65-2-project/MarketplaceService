using System;
using System.Threading.Tasks;
using marketplaceservice.Domain;
using marketplaceservice.Models;
using marketplaceservice.Repositories;
using marketplaceservice.Services;
using Moq;
using Xunit;

namespace marketplaceservicetests.Service
{
    public class MarketplaceServiceTests
    {
        private readonly IMarketplaceService _marketplaceService;
        private readonly Mock<IMarketplaceRepository> _repository;

        public MarketplaceServiceTests()
        {
            _repository = new Mock<IMarketplaceRepository>();
            _marketplaceService = new MarketplaceService(_repository.Object);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new Product()
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateProductModel()
            {
                Title = titleText,
                Description = descriptionText
            };

            _repository.Setup(x => x.GetProduct(createProductModel.Guid)).ReturnsAsync((Product) null);
            _repository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);

            var result = await _marketplaceService.CreateProduct(createProductModel);
            
            Assert.Equal(product.Title, result.Title);
        }
        
        [Fact]
        public async Task CreateProduct_Failed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new Product()
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateProductModel()
            {
                Title = "",
                Description = ""
            };

            _repository.Setup(x => x.GetProduct(createProductModel.Guid)).ReturnsAsync((Product) null);
            _repository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);

            var result = await Assert.ThrowsAsync<ArgumentException>(() =>_marketplaceService.CreateProduct(createProductModel));
            
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
            //Assert.NotEqual(product.Title, result.Title);
            //Assert.NotEqual(product.Description, result.Description);
        }
    }
}