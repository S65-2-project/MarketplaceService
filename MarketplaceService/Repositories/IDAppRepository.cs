using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.Domain;

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
    }
}