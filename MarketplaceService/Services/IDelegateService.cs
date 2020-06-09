using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;

namespace MarketplaceService.Services
{
    public interface IDelegateService
    {
        /// <summary>
        /// Creates a DelegateOffer 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>DelegateOffer</returns>
        Task<DelegateOffer> CreateDelegateOffer(CreateDelegateOfferModel creatDelegateOfferModel);
        /// <summary>
        /// Gets an DelegateOffer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DelegateOffer</returns>
        Task<DelegateOffer> GetDelegateOffer(Guid id);
        /// <summary>
        /// Updates a DelegateOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDelegateOfferModel"></param>
        /// <returns>DelegateOffer</returns>
        Task<DelegateOffer> UpdateDelegateOffer(Guid id, UpdateDelegateOfferModel updateDelegateOfferModel);
        /// <summary>
        /// Deletes a DelegateOffer
        /// </summary>
        /// <param name="id"></param>
        Task DeleteDelegateOffer(Guid id);

        /// <summary>
        /// Gets filtered list
        /// </summary>
        /// <param name="getDelegateOfferModel"> filter options</param>
        /// <returns>filtered pagedlist</returns>
        Task<PagedList<DelegateOffer>> GetOffers(GetDelegateOfferModel getDelegateOfferModel);
    }
}