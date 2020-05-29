using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Repositories;
using MarketplaceService.Services;
using Moq;
using Xunit;

namespace MarketServiceTests.Service
{
public class DAppServiceTests
    {
        private readonly IDAppService _dAppService;
        private readonly Mock<IDAppRepository> _dAppRepository ;
        
        public DAppServiceTests()
        {
            _dAppRepository  = new Mock<IDAppRepository>();
            _dAppService = new DAppService(_dAppRepository .Object);
        }

        [Fact]
        public async Task CreateProduct_Failed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new DAppOffer
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDAppOfferModel {
                Title = "",
                Description = ""
            };

            _dAppRepository .Setup(x => x.CreateDAppOffer(It.IsAny<DAppOffer>())).ReturnsAsync(product);

            var result = await Assert.ThrowsAsync<EmptyFieldException>(() =>
                    _dAppService.CreateDAppOffer(createProductModel));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task CreateDAppOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new DAppOffer
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDAppOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _dAppRepository .Setup(x => x.CreateDAppOffer(It.IsAny<DAppOffer>())).ReturnsAsync(product);

            var result = await _dAppService.CreateDAppOffer(createProductModel);

            Assert.Equal(product.Title, result.Title);
        }

        [Fact]
        public async Task UpdateProduct_EmptyTitleFailed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

            var product = new DAppOffer
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new DAppOffer
            {
                Id = id,
                Title = "",
                Description = "New Description"
            };

            var updateProductModel = new UpdateDAppOfferModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };

            _dAppRepository .Setup(x =>
                x.UpdateDAppOffer(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await Assert.ThrowsAsync<EmptyFieldException>(() =>
                _dAppService.UpdateDAppOffer(product.Id, updateProductModel));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task UpdateDAppOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

            var product = new DAppOffer
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new DAppOffer
            {
                Id = id,
                Title = "New Title",
                Description = "New Description"
            };

            var updateProductModel = new UpdateDAppOfferModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };
            _dAppRepository .Setup(x => x.GetDAppOffer(product.Id)).ReturnsAsync(product);
            _dAppRepository .Setup(x =>
                x.UpdateDAppOffer(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await _dAppService.UpdateDAppOffer(product.Id, updateProductModel);

            Assert.NotNull(result);
            Assert.Equal(updatedProduct, result);
            Assert.NotEqual(titleText, result.Description);
            Assert.NotEqual(descriptionText, result.Title);
        }
    }
}