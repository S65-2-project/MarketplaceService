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
    public class DAppControllerTests
    {
        private readonly DAppController _dAppController;
        private readonly Mock<IDAppService> _dAppService;

        public DAppControllerTests()
        {
            _dAppService = new Mock<IDAppService>();
            _dAppController = new DAppController(_dAppService.Object);
        }

        [Fact]
        public async Task CreateDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var productModel = new CreateDAppOfferModel() {
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.CreateDAppOffer(productModel))
                .Throws<EmptyFieldException>();

            //Act
            var result = await _dAppController.CreateDAppOffer(productModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateDAppOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new DAppOffer()
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDAppOfferModel()
            {
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.CreateDAppOffer(createProductModel)).ReturnsAsync(product);

            var result = await _dAppController.CreateDAppOffer(createProductModel) as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product.Title, ((DAppOffer) result.Value).Title);
        }

        [Fact]
        public async Task DeleteDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _dAppService.Setup(x => x.DeleteDAppOffer(guid)).Throws<ProductDeleteException>();

            //Act
            var result = await _dAppController.DeleteDAppOffer(guid);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteDelegateOffer_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            _dAppService.Setup(x => x.DeleteDAppOffer(guid));

            //Act
            var result = await _dAppController.DeleteDAppOffer(guid);

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

            _dAppService.Setup(x => x.GetDAppOffer(product1.Id)).Throws<OfferNotFoundException>();

            //Act
            var result = await _dAppController.GetDAppOffer(product1.Id);

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

            var product1 = new DAppOffer
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.GetDAppOffer(guid)).ReturnsAsync(product1);

            //Act
            var result = await _dAppController.GetDAppOffer(product1.Id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1.Id, ((DAppOffer) result.Value).Id);
        }

        [Fact]
        public async Task UpdateProduct_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title1";
            const string descriptionText = "Description1";

            var updateProductModel = new UpdateDAppOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.UpdateDAppOffer(guid, updateProductModel)).Throws<OfferUpdateFailedException>();

            //Act
            var result = await _dAppController.UpdateDAppOffer(guid, updateProductModel);

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

            var updateProductModel = new UpdateDAppOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            var product1 = new DAppOffer
            {
                Id = guid,
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.UpdateDAppOffer(guid, updateProductModel)).ReturnsAsync(product1);

            //Act
            var result = await _dAppController.UpdateDAppOffer(guid, updateProductModel) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1, (DAppOffer) result.Value);
        }
    }
}