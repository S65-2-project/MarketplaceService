using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using marketplaceservice.Domain;

namespace marketplaceservice.Repositories
{
    public interface IMarketplaceRepository
    {
        /// <summary>
        /// Creates a product 
        /// </summary>
        /// <param name="productIn"></param>
        /// <returns>Product</returns>
        Task<Product> CreateProduct(Product productIn);
        
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        Task<List<Product>> GetAll();
        
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
        /// <param name="productIn"></param>
        /// <returns>Product</returns>
        Task<Product> UpdateProduct(Guid id, Product productIn);
        
        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id"></param>
        Task DeleteProduct(Guid id);
    }
}
