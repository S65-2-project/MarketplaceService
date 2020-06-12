using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.DataTypes;
using MarketplaceService.Domain;
using MarketplaceService.Models;
using MongoDB.Driver;

namespace MarketplaceService.Repositories
{
    public class DAppRepository : IDAppRepository
    {
        private readonly IMongoCollection<DAppOffer> _dAppOffers;

        public DAppRepository(IMarketplaceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _dAppOffers = database.GetCollection<DAppOffer>(settings.DAppOfferCollectionName);
        }

        public async Task<DAppOffer> CreateDAppOffer(DAppOffer product)
        {
            await _dAppOffers.InsertOneAsync(product);
            return product;
        }

        public async Task<List<DAppOffer>> GetAllDAppOffers()
        {
            return await _dAppOffers.Find(_ => true).ToListAsync();
        }

        public async Task<DAppOffer> GetDAppOffer(Guid id)
        {
            return await _dAppOffers.Find(f => f.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<DAppOffer> UpdateDAppOffer(Guid id, DAppOffer productIn)
        {
            await _dAppOffers.ReplaceOneAsync(f => f.Id == id, productIn);
            return productIn;
        }

        public async Task DeleteDAppOffer(Guid id)
        {
            await _dAppOffers.DeleteManyAsync(f => f.Id == id);
        }

        public async Task<PagedList<DAppOffer>> GetAllDAppOffers(GetDAppOfferModel getDAppOfferModel)
        {
            // Retrieve the collection as queryable
            var offers = _dAppOffers.AsQueryable().OrderBy(on => on.LiskPerMonth).AsQueryable();

            // Apply filters
            if (getDAppOfferModel.MinReward != null)
                offers = offers.Where(o => o.LiskPerMonth >= getDAppOfferModel.MinReward);

            if (getDAppOfferModel.MaxReward != null)
                offers = offers.Where(o => o.LiskPerMonth <= getDAppOfferModel.MaxReward);

            if (!string.IsNullOrEmpty(getDAppOfferModel.SearchQuery))
                offers = offers.Where(o => o.Title.ToLower().Contains(getDAppOfferModel.SearchQuery.ToLower()));

            if (!string.IsNullOrEmpty(getDAppOfferModel.RegionQuery))
                offers = offers.Where(o => o.Region.ToLower().Contains(getDAppOfferModel.RegionQuery.ToLower()));

            return await PagedList<DAppOffer>.ToPagedList(offers, getDAppOfferModel.PageNumber, getDAppOfferModel.PageSize);
        }

        public async Task<DAppOffer> AddDelegateToDAppOffer(Guid id, DAppOffer offerIn)
        {
            await _dAppOffers.ReplaceOneAsync(f => f.Id == id, offerIn);
            return offerIn;
        }

        public async Task<DAppOffer> RemoveDelegateFromDAppOffer(Guid id, DAppOffer offerIn)
        {
            await _dAppOffers.ReplaceOneAsync(f => f.Id == id, offerIn);
            return offerIn;
        }

        public async Task RemoveDAppOffersWithUser(Guid id)
        {
            await _dAppOffers.DeleteManyAsync(offer => offer.Provider.Id == id);
        }

        public async Task RemoveDelegateFromAllOffersWithUser(Guid id)
        {
            var listDAppsWithUserThatIsGonnaBeRemoved = await _dAppOffers.Find(offer => offer.DelegatesCurrentlyInOffer.Exists(user => user.Id == id)).ToListAsync(); //get all offer which has the user with id
            foreach(var dapp in listDAppsWithUserThatIsGonnaBeRemoved)
            {
                dapp.DelegatesCurrentlyInOffer.Remove(dapp.DelegatesCurrentlyInOffer.Find(user => user.Id == id)); //remove the user from list.
                await UpdateDAppOffer(dapp.Id, dapp);//update dapp with list without user
            }
        }

        public async Task UpdateUserEmail(Guid id, string newEmail)
        {
            var listWithDelegatesInOfferWithID = await _dAppOffers.Find(offer => offer.DelegatesCurrentlyInOffer.Exists(user => user.Id == id)).ToListAsync();
            foreach(var dapp in listWithDelegatesInOfferWithID)
            {
                dapp.DelegatesCurrentlyInOffer.Find(user => user.Id == id).Name = newEmail;
                await UpdateDAppOffer(dapp.Id, dapp);
            }

            var listProvidersWithId = await _dAppOffers.Find(offer => offer.Provider.Id == id).ToListAsync();
            foreach(var dapp in listProvidersWithId)
            {
                dapp.Provider.Name = newEmail;
                await UpdateDAppOffer(dapp.Id, dapp);
            }
        }
    }
}