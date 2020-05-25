using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Repositories;

namespace MarketplaceService.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly IMarketplaceRepository _marketplaceRepository;

        public MarketplaceService(IMarketplaceRepository marketplaceRepository)
        {
            _marketplaceRepository = marketplaceRepository;
        }

        public async Task<Product> CreateProduct(CreateProductModel createProductModel)
        {
            if (string.IsNullOrEmpty(createProductModel.Title) || string.IsNullOrEmpty(createProductModel.Description))
                throw new EmptyFieldException();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = createProductModel.Title,
                Description = createProductModel.Description
            };

            return await _marketplaceRepository.CreateProduct(product);
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _marketplaceRepository.GetProduct(id);
        }

        public async Task<Product> UpdateProduct(Guid id, UpdateProductModel updateProductModel)
        {
            if (string.IsNullOrEmpty(updateProductModel.Title) || string.IsNullOrEmpty(updateProductModel.Description))
                throw new EmptyFieldException();

            var product = await GetProduct(id);

            product.Id = id;
            product.Title = updateProductModel.Title;
            product.Description = updateProductModel.Description;

            return await _marketplaceRepository.UpdateProduct(id, product);
        }

        public async Task DeleteProduct(Guid id)
        {
            await _marketplaceRepository.DeleteProduct(id);
        }
    }
}