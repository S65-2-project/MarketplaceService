using System;
using System.Threading.Tasks;
using marketplaceservice.Helpers;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Repositories;
using MarketplaceService.Services;
using Moq;
using Xunit;

namespace MarketplaceServiceTests.Service
{
    public class DelegateServiceTests
    {
        private readonly IDelegateService _marketplaceService;
        private readonly Mock<IDelegateRepository> _marketplaceRepository ;
        private readonly Mock<IJwtIdClaimReaderHelper> _jwtIdClaimReaderHelper;


        public DelegateServiceTests()
        {
            _marketplaceRepository  = new Mock<IDelegateRepository>();
            _jwtIdClaimReaderHelper = new Mock<IJwtIdClaimReaderHelper>();
            _marketplaceService = new DelegateService(_marketplaceRepository.Object, _jwtIdClaimReaderHelper.Object);
        }

        [Fact]
        public async Task CreateProduct_Failed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            const string jwt = "";

            var product = new DelegateOffer
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDelegateOfferModel {
                Title = "",
                Description = ""
            };

            _marketplaceRepository .Setup(x => x.CreateDelegateOffer(It.IsAny<DelegateOffer>())).ReturnsAsync(product);

            var result = await Assert.ThrowsAsync<EmptyFieldException>(() =>
                    _marketplaceService.CreateDelegateOffer(createProductModel, jwt));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task CreateDelegateOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            const string jwt = "";
            var userid = Guid.NewGuid();

            var product = new DelegateOffer
            {
                Title = titleText,
                Description = descriptionText,
                Provider = new User(){ Id = userid }

            };

            var createProductModel = new CreateDelegateOfferModel
            {
                Title = titleText,
                Description = descriptionText,
                Provider = new User(){ Id = userid }
            };

            _marketplaceRepository.Setup(x => x.CreateDelegateOffer(It.IsAny<DelegateOffer>())).ReturnsAsync(product);
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(userid);

            var result = await _marketplaceService.CreateDelegateOffer(createProductModel, jwt);

            Assert.Equal(product.Title, result.Title);
            Assert.Equal(1, 1);

        }

        [Fact]
        public async Task UpdateProduct_EmptyTitleFailed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();
            const string jwt = "";

            var product = new DelegateOffer
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new DelegateOffer
            {
                Id = id,
                Title = "",
                Description = "New Description"
            };

            var updateProductModel = new UpdateDelegateOfferModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };

            _marketplaceRepository .Setup(x =>
                x.UpdateDelegateOffer(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await Assert.ThrowsAsync<EmptyFieldException>(() =>
                _marketplaceService.UpdateDelegateOffer(product.Id, updateProductModel, jwt));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task UpdateDelegateOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();
            const string jwt = "";
            var userid = Guid.NewGuid();


            var product = new DelegateOffer
            {
                Id = id,
                Title = titleText,
                Description = descriptionText,
                Provider = new User(){ Id = userid }
            };

            var updatedProduct = new DelegateOffer
            {
                Id = id,
                Title = "New Title",
                Description = "New Description",
                Provider = new User(){ Id = userid }

            };

            var updateProductModel = new UpdateDelegateOfferModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };
            _marketplaceRepository.Setup(x => x.GetDelegateOffer(product.Id)).ReturnsAsync(product);
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(userid);
            _marketplaceRepository.Setup(x =>
               x.UpdateDelegateOffer(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await _marketplaceService.UpdateDelegateOffer(product.Id, updateProductModel, jwt);

            Assert.NotNull(result);
            Assert.Equal(updatedProduct, result);
            Assert.NotEqual(titleText, result.Description);
            Assert.NotEqual(descriptionText, result.Title);

        }
    }
}