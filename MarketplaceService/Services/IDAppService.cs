using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Models;

namespace MarketplaceService.Services
{
    public interface IDAppService
    {
        /// <summary>
        /// Creates a DAppOffer 
        /// </summary>
        /// <param name="createDAppOfferModel"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> CreateDAppOffer(CreateDAppOfferModel createDAppOfferModel);
        
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
        /// <param name="updateDAppOfferModel"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> UpdateDAppOffer(Guid id, UpdateDAppOfferModel updateDAppOfferModel);
        
        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userIn"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> RemoveDelegateFromDAppOffer(Guid id, User userIn);
        
        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userOut"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> AddDelegateToDAppOffer(Guid id, User userOut);
        
        /// <summary>
        /// Deletes a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        Task DeleteDAppOffer(Guid id);
    }
}