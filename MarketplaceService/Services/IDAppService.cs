using System;
using System.Threading.Tasks;
using MarketplaceService.DataTypes;
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
        Task<DAppOffer> CreateDAppOffer(CreateDAppOfferModel createDAppOfferModel, string jwt);
        
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
        Task<DAppOffer> UpdateDAppOffer(Guid id, UpdateDAppOfferModel updateDAppOfferModel, string jwt);
        
        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userIn"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> RemoveDelegateFromDAppOffer(Guid id, User userIn ,string jwt);
        
        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userOut"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> AddDelegateToDAppOffer(Guid id, User userOut, string jwt);
        
        /// <summary>
        /// Deletes a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        Task DeleteDAppOffer(Guid id, string jwt);

        /// <summary>
        /// Gets offers that comply with filters
        /// </summary>
        /// <param name="getDAppOfferModel">filter values</param>
        /// <returns></returns>
        Task<PagedList<DAppOffer>> GetOffers(GetDAppOfferModel getDAppOfferModel);
    }
}