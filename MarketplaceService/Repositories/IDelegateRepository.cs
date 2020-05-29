using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Helpers;
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

        Task<PagedList<DelegateOffer>> GetAllDelegateOffers(GetOfferModel getOfferModel);
        
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

        IEnumerable<DelegateOffer> GetAllDelegateOffersEnum();
    }
}
