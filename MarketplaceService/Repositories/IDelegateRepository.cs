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
        /// <param name="getOfferModel"> filter parameters </param>
        /// <returns> Pagedlist </returns>
        Task<PagedList<DelegateOffer>> GetAllDelegateOffers(GetOfferModel getOfferModel);
        
        /// <summary>
        /// Will Update Every occurence of the user with the id with the new email parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        Task UpdateUserEmail(Guid id, string newEmail);

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

        /// <summary>
        /// Remove All Offers which has a user with id as provider
        /// </summary>
        /// <param name="id">of a user which is removed from the application</param>
        /// <returns></returns>
        Task RemoveDelegateOffersWithUser(Guid id);

    }
}
