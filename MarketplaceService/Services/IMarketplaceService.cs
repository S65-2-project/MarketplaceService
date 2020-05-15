using System;
using System.Threading.Tasks;
using marketplaceservice.Domain;
using marketplaceservice.Models;

namespace marketplaceservice.Services
{
    public interface IMarketplaceService
    {
        Task<Product> CreateProduct(CreateProductModel product);
        Task<Product> GetProduct(Guid id);
        Task<Product> UpdateProduct(Guid id, UpdateProductModel product);
        Task DeleteProduct(Guid id);
    }
}