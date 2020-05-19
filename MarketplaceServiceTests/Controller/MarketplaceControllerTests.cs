using System;
using System.Threading.Tasks;
using MarketplaceService.Controllers;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MarketplaceServiceTests.Controller
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
        public async Task CreateProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var productModel = new CreateProductModel
            {
                Guid = guid,
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.CreateProduct(productModel))
                .Throws<EmptyFieldException>();

            //Act
            var result = await _marketplaceController.Create(productModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var guid = Guid.NewGuid();

            var product = new Product
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateProductModel
            {
                Guid = guid,
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.CreateProduct(createProductModel)).ReturnsAsync(product);

            var result = await _marketplaceController.Create(createProductModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product.Id, ((Product) result.Value).Id);
        }

        [Fact]
        public async Task DeleteProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _marketplaceService.Setup(x => x.DeleteProduct(guid)).Throws<ProductDeleteException>();

            //Act
            var result = await _marketplaceController.Delete(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _marketplaceService.Setup(x => x.DeleteProduct(guid));

            //Act
            var result = await _marketplaceController.Delete(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task GetProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product1 = new Product
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.GetProduct(product1.Id)).Throws<ProductNotFoundException>();

            //Act
            var result = await _marketplaceController.Get(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetProduct_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product1 = new Product
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.GetProduct(product1.Id)).ReturnsAsync(product1);

            //Act
            var result = await _marketplaceController.Get(product1.Id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1.Id, ((Product) result.Value).Id);
        }

        [Fact]
        public async Task UpdateProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title1";
            const string descriptionText = "Description1";

            var updateProductModel = new UpdateProductModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.UpdateProduct(guid, updateProductModel)).Throws<ProductUpdateFailedException>();

            //Act
            var result = await _marketplaceController.Update(guid, updateProductModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title1";
            const string descriptionText = "Description1";

            var updateProductModel = new UpdateProductModel
            {
                Title = titleText,
                Description = descriptionText
            };

            var product1 = new Product
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceService.Setup(x => x.UpdateProduct(guid, updateProductModel)).ReturnsAsync(product1);

            //Act
            var result = await _marketplaceController.Update(guid, updateProductModel) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1, (Product) result.Value);
        }
    }
}