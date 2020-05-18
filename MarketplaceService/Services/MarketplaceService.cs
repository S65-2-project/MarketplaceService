using System;
using System.Threading.Tasks;
using marketplaceservice.Domain;
using marketplaceservice.Exceptions;
using marketplaceservice.Models;
using marketplaceservice.Repositories;

namespace marketplaceservice.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly IMarketplaceRepository _marketplaceRepository;

        public MarketplaceService(IMarketplaceRepository marketplaceMarketplaceRepository)
        {
            _marketplaceRepository = marketplaceMarketplaceRepository;
        }

        public async Task<Product> CreateProduct(CreateProductModel createProductModel)
        {
            if (string.IsNullOrEmpty(createProductModel.Title) || string.IsNullOrEmpty(createProductModel.Description))
                throw new EmptyFieldException();

            var productIn = new Product
            {
                Id = Guid.NewGuid(),
                Title = createProductModel.Title,
                Description = createProductModel.Description
            };

            return await _marketplaceRepository.CreateProduct(productIn);
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