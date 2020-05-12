using System;
using System.Threading.Tasks;
using marketplaceservice.Domain;
using marketplaceservice.Models;

namespace marketplaceservice.Repositories
{
    public interface IMarketplaceRepository
    {
        Task<Product> CreateProduct(Product productIn);
        Task<Product> GetProduct(Guid id);
        Task<Product> UpdateProduct(Guid id, Product productIn);
        Task DeleteProduct(Guid id);
    }
}