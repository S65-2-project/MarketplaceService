using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;

namespace MarketplaceService.Repositories
{
    public interface IDelegateRepository
    {
        /// <summary>
        /// Creates a product 
        /// </summary>
        /// <param name="productIn"></param>
        /// <returns>Product</returns>
        Task<DelegateOffer> CreateDelegateOffer(DelegateOffer productIn);
        
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        Task<List<DelegateOffer>> GetAllDelegateOffers();

        /// <summary>
        /// Get filtered list of products
        /// </summary>
        /// <param name="getDelegateOfferModel"> filter parameters </param>
        /// <returns> Pagedlist </returns>
        Task<PagedList<DelegateOffer>> GetAllDelegateOffers(GetDelegateOfferModel getDelegateOfferModel);

        /// <summary>
        /// Gets an product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        Task<DelegateOffer> GetDelegateOffer(Guid id);
        
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>Product</returns>
        Task<DelegateOffer> UpdateDelegateOffer(Guid id, DelegateOffer offerIn);

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id"></param>
        Task DeleteDelegateOffer(Guid id);
    }
}
