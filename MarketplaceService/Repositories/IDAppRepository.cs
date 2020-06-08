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
        /// Will Update Every occurence of the user with the id with the new email parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        Task UpdateUserEmail(Guid id, string newEmail);

        /// <summary>
        /// Updates a DAppOffer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>DAppOffer</returns>
        Task<DAppOffer> UpdateDAppOffer(Guid id, DAppOffer offerIn);

        /// <summary>
        /// adds a delegate from Delegates currently in offer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerIn"></param>
        /// <returns>Product</returns>
        Task<DAppOffer> AddDelegateToDAppOffer(Guid id, DAppOffer offerIn);
        
        /// <summary>
        /// Removes a delegate from Delegates currently in offer
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
        /// Remove All Offers which has a user with id as provider
        /// </summary>
        /// <param name="id">of a user which is removed from the application</param>
        /// <returns></returns>
        Task RemoveDAppOffersWithuser(Guid id);
        /// <summary>
        /// Will check every dapp offer if they have a delegate currently in offer with this id, if so it will remove the delegate from this offer
        /// </summary>
        /// <param name="id">of a user which is removed from the application</param>
        /// <returns></returns>
        Task RemoveDelegateFromAllOffersWithuser(Guid id);

    }
}