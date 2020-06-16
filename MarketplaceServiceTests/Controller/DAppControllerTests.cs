using System;
using System.Threading.Tasks;
using marketplaceservice.Helpers;
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
        private readonly Mock<IJwtIdClaimReaderHelper> _jwtIdClaimReaderHelper;

        public DAppControllerTests()
        {
            _dAppService = new Mock<IDAppService>();
            _dAppController = new DAppController(_dAppService.Object);
            _jwtIdClaimReaderHelper = new Mock<IJwtIdClaimReaderHelper>();
        }

        [Fact]
        public async Task CreateDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            const string jwt = "";
            var productModel = new CreateDAppOfferModel() {
                Title = titleText,
                Description = descriptionText
            };
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(guid);


            _dAppService.Setup(x => x.CreateDAppOffer(productModel, jwt))
                .Throws<EmptyFieldException>();

            //Act
            var result = await _dAppController.CreateDAppOffer(productModel, jwt);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateDAppOffer_Success()
        {
            var userid = Guid.NewGuid();
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            const string jwt = "";

            var product = new DAppOffer()
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDAppOfferModel()
            {
                Provider = new User { Id = userid },
                Title = titleText,
                Description = descriptionText
            };
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(userid);


            _dAppService.Setup(x => x.CreateDAppOffer(createProductModel, jwt)).ReturnsAsync(product);

            var result = await _dAppController.CreateDAppOffer(createProductModel, jwt) as ObjectResult;

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product.Title, ((DAppOffer) result.Value).Title);
        }

        [Fact]
        public async Task DeleteDelegateOffer_BadRequest()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string jwt = "";

            _dAppService.Setup(x => x.DeleteDAppOffer(guid, jwt)).Throws<ProductDeleteException>();

            //Act
            var result = await _dAppController.DeleteDAppOffer(guid, jwt);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteDelegateOffer_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();
            const string jwt = "";

            _dAppService.Setup(x => x.DeleteDAppOffer(guid, jwt));

            //Act
            var result = await _dAppController.DeleteDAppOffer(guid, jwt);

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
            const string jwt = "";
            var updateProductModel = new UpdateDAppOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _dAppService.Setup(x => x.UpdateDAppOffer(guid, updateProductModel, jwt)).Throws<OfferUpdateFailedException>();

            //Act
            var result = await _dAppController.UpdateDAppOffer(guid, updateProductModel, jwt);

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
            const string jwt = "";

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

            _dAppService.Setup(x => x.UpdateDAppOffer(guid, updateProductModel, jwt )).ReturnsAsync(product1);

            //Act
            var result = await _dAppController.UpdateDAppOffer(guid, updateProductModel, jwt) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product1, (DAppOffer) result.Value);
        }
    }
}