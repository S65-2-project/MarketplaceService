﻿using System;
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
        public MarketplaceServiceTests()
        {
            _repository = new Mock<IMarketplaceRepository>();
            _marketplaceService = new MarketplaceService(_repository.Object);
        }

        private readonly IMarketplaceService _marketplaceService;
        private readonly Mock<IMarketplaceRepository> _repository;

        [Fact]
        public async Task CreateProduct_Failed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new Product
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateProductModel
            {
                Title = "",
                Description = ""
            };

            _repository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);

            var result =
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    _marketplaceService.CreateProduct(createProductModel));

            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";

            var product = new Product
            {
                Title = titleText,
                Description = descriptionText
            };

            var createProductModel = new CreateProductModel
            {
                Title = titleText,
                Description = descriptionText
            };

            _repository.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);

            var result = await _marketplaceService.CreateProduct(createProductModel);

            Assert.Equal(product.Title, result.Title);
        }

        [Fact]
        public async Task UpdateProduct_EmptyTitleFailed()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

            var product = new Product
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new Product
            {
                Id = id,
                Title = "",
                Description = "New Description"
            };

            var updateProductModel = new UpdateProductModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };

            _repository.Setup(x =>
                x.UpdateProduct(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await Assert.ThrowsAsync<ArgumentException>(() =>
                _marketplaceService.UpdateProduct(product.Id, updateProductModel));

            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            const string titleText = "Title Text";
            const string descriptionText = "Description Text";
            var id = Guid.NewGuid();

            var product = new Product
            {
                Id = id,
                Title = titleText,
                Description = descriptionText
            };

            var updatedProduct = new Product
            {
                Id = id,
                Title = "New Title",
                Description = "New Description"
            };

            var updateProductModel = new UpdateProductModel
            {
                Title = updatedProduct.Title,
                Description = updatedProduct.Description
            };
            _repository.Setup(x => x.GetProduct(product.Id)).ReturnsAsync(product);
            _repository.Setup(x =>
                x.UpdateProduct(product.Id, product)).ReturnsAsync(updatedProduct);

            var result = await _marketplaceService.UpdateProduct(product.Id, updateProductModel);

            Assert.NotNull(result);
            Assert.Equal(updatedProduct, result);
            Assert.NotEqual(titleText, result.Description);
            Assert.NotEqual(descriptionText, result.Title);
        }
    }
}