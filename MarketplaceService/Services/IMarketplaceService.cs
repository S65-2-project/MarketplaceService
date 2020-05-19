using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Models;

namespace MarketplaceService.Services
{
    public interface IMarketplaceService
    {
        /// <summary>
        /// Creates a product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        Task<Product> CreateProduct(CreateProductModel product);
        /// <summary>
        /// Gets an product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        Task<Product> GetProduct(Guid id);
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        Task<Product> UpdateProduct(Guid id, UpdateProductModel product);
        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id"></param>
        Task DeleteProduct(Guid id);
    }
}