using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.DataTypes;
using MarketplaceService.Domain;
using MarketplaceService.Models;

namespace MarketplaceService.Repositories
{
    public interface IDAppRepository
    {
        /// <summary>
        /// Creates a DAppOffer 
        /// </summary>
        /// <param name="offerIn"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> CreateDAppOffer(DAppOffer offerIn);
        
        /// <summary>
        /// Get all DAppOffer
        /// </summary>
        /// <returns>DAppOffer</returns>
        Task<List<DAppOffer>> GetAllDAppOffers();
        
        /// <summary>
        /// Gets a DAppOffer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> GetDAppOffer(Guid id);
        
        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> UpdateDAppOffer(Guid id, DAppOffer offerIn);

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>Product</returns>
        Task<DAppOffer> AddDelegateToDAppOffer(Guid id, DAppOffer offerIn);
        
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>Product</returns>
        Task<DAppOffer> RemoveDelegateFromDAppOffer(Guid id, DAppOffer offerIn);
        
        /// <summary>
        /// Deletes a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        Task DeleteDAppOffer(Guid id);
        /// <summary>
        /// Gets offers that comply with filters
        /// </summary>
        /// <param name="getDAppOfferModel">filter values</param>
        /// <returns></returns>
        Task<PagedList<DAppOffer>> GetAllDAppOffers(GetDAppOfferModel getDAppOfferModel);
    }
}