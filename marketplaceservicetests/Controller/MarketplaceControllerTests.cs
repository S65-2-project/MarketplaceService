using System;
using System.Threading.Tasks;
using marketplaceservice.Controllers;
using marketplaceservice.Domain;
using marketplaceservice.Models;
using marketplaceservice.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace marketplaceservicetests.Controller
{
    public class MarketplaceControllerTests
    {
        private readonly MarketplaceController _marketplaceController;
        private readonly Mock<IMarketplaceService> _marketplaceService;

        public MarketplaceControllerTests()
        {
            _marketplaceService = new Mock<IMarketplaceService>();
            _marketplaceController = new MarketplaceController(_marketplaceService.Object);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Title = titleText,
                Description = descriptionText
            };
            
            var createProductModel = new CreateProductModel()
            {
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.CreateProduct(createProductModel)).ReturnsAsync(product);

            var result = await _marketplaceController.Create(createProductModel);
            var data = result as ObjectResult;
            
            Assert.NotNull(result);
            Assert.NotNull(data);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product.Id, ((Product) data.Value).Id);
        }
    }
}