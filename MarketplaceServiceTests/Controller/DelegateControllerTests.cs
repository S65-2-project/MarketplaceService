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
    public class DelegateControllerTests
    {
        private readonly DelegateController _delegateController;
        private readonly Mock<IDelegateService> _delegateService;

        public DelegateControllerTests()
        {
            _delegateService = new Mock<IDelegateService>();
            _delegateController = new DelegateController(_delegateService.Object);
        }

        [Fact]
        public async Task CreateDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var productModel = new CreateDelegateOfferModel() {
                Guid = guid,
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.CreateDelegateOffer(productModel))
                .Throws<EmptyFieldException>();

            //Act
            var result = await _delegateController.CreateDelegateOffer(productModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateDelegateOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var guid = Guid.NewGuid();

            var product = new DelegateOffer()
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDelegateOfferModel()
            {
                Guid = guid,
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.CreateDelegateOffer(createProductModel)).ReturnsAsync(product);

            var result = await _delegateController.CreateDelegateOffer(createProductModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product.Id, ((DelegateOffer) result.Value).Id);
        }

        [Fact]
        public async Task DeleteDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _delegateService.Setup(x => x.DeleteDelegateOffer(guid)).Throws<ProductDeleteException>();

            //Act
            var result = await _delegateController.DeleteDelegateOffer(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteDelegateOffer_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _delegateService.Setup(x => x.DeleteDelegateOffer(guid));

            //Act
            var result = await _delegateController.DeleteDelegateOffer(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task GetDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product1 = new DelegateOffer()
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.GetDelegateOffer(product1.Id)).Throws<OfferNotFoundException>();

            //Act
            var result = await _delegateController.GetDelegateOffer(product1.Id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetDelegateOffer_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product1 = new DelegateOffer
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.GetDelegateOffer(product1.Id)).ReturnsAsync(product1);

            //Act
            var result = await _delegateController.GetDelegateOffer(product1.Id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1.Id, ((DelegateOffer) result.Value).Id);
        }

        [Fact]
        public async Task UpdateProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title1";
            const string descriptionText = "Description1";

            var updateProductModel = new UpdateDelegateOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.UpdateDelegateOffer(guid, updateProductModel)).Throws<OfferUpdateFailedException>();

            //Act
            var result = await _delegateController.UpdateDelegateOffer(guid, updateProductModel);

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

            var updateProductModel = new UpdateDelegateOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            var product1 = new DelegateOffer
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _delegateService.Setup(x => x.UpdateDelegateOffer(guid, updateProductModel)).ReturnsAsync(product1);

            //Act
            var result = await _delegateController.UpdateDelegateOffer(guid, updateProductModel) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1, (DelegateOffer) result.Value);
        }
    }
}