﻿using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Repositories;
using MarketplaceService.Services;
using Moq;
using Xunit;

namespace MarketplaceServiceTests.Service
{
    public class MarketplaceServiceTests
    {
        private readonly IDelegateService _marketplaceService;
        private readonly Mock<IDelegateRepository> _marketplaceRepository ;
        
        public MarketplaceServiceTests()
        {
            _marketplaceRepository  = new Mock<IDelegateRepository>();
            _marketplaceService = new MarketplaceService.Services.DelegateService(_marketplaceRepository .Object);
        }

        [Fact]
        public async Task CreateProduct_Failed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

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
                    _marketplaceService.CreateDelegateOffer(createProductModel));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task CreateDelegateOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new DelegateOffer
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateDelegateOfferModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _marketplaceRepository .Setup(x => x.CreateDelegateOffer(It.IsAny<DelegateOffer>())).ReturnsAsync(product);

            var result = await _marketplaceService.CreateDelegateOffer(createProductModel);

            Assert.Equal(product.Title, result.Title);
        }

        [Fact]
        public async Task UpdateProduct_EmptyTitleFailed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

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
                _marketplaceService.UpdateDelegateOffer(product.Id, updateProductModel));

            Assert.NotNull(result);
            Assert.IsType<EmptyFieldException>(result);
        }

        [Fact]
        public async Task UpdateDelegateOffer_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

            var product = new DelegateOffer
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new DelegateOffer
            {
                Id = id,
                Title = "New Title",
                Description = "New Description"
            };

            var updateProductModel = new UpdateDelegateOfferModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };
            _marketplaceRepository .Setup(x => x.GetDelegateOffer(product.Id)).ReturnsAsync(product);
            _marketplaceRepository .Setup(x =>
                x.UpdateDelegateOffer(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await _marketplaceService.UpdateDelegateOffer(product.Id, updateProductModel);

            Assert.NotNull(result);
            Assert.Equal(updatedProduct, result);
            Assert.NotEqual(titleText, result.Description);
            Assert.NotEqual(descriptionText, result.Title);
        }
    }
}