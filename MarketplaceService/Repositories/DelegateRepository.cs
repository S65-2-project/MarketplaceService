using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;
using MongoDB.Driver;
using Newtonsoft.Json.Schema;

namespace MarketplaceService.Repositories
{
    public class DelegateRepository : IDelegateRepository
    {
        private readonly IMongoCollection<DelegateOffer> _delegateOffers;

        public DelegateRepository(IMarketplaceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _delegateOffers = database.GetCollection<DelegateOffer>(settings.DelegateOfferCollectionName);
        }

        public async Task<DelegateOffer> CreateDelegateOffer(DelegateOffer product)
        {
            await _delegateOffers.InsertOneAsync(product);
            return product;
        }

        public async Task<List<DelegateOffer>> GetAllDelegateOffers()
        {
            return await _delegateOffers.Find(_ => true).ToListAsync();
        }

        public async Task<PagedList<DelegateOffer>> GetAllDelegateOffers(GetDelegateOfferModel getDelegateOfferModel)
        {
            // Retrieve the collection as queryable
            var offers = _delegateOffers.AsQueryable().OrderBy(on => on.LiskPerMonth).AsQueryable();

            // Apply filters
            if (getDelegateOfferModel.MinPrice != null)
                offers = offers.Where(o => o.LiskPerMonth >= getDelegateOfferModel.MinPrice);

            if (getDelegateOfferModel.MaxPrice != null)
                offers = offers.Where(o => o.LiskPerMonth <= getDelegateOfferModel.MaxPrice);

            if (getDelegateOfferModel.MinMonth != null)
                offers = offers.Where(o => o.AvailableForInMonths >= getDelegateOfferModel.MinMonth);

            if (getDelegateOfferModel.MaxMonth != null)
                offers = offers.Where(o => o.AvailableForInMonths <= getDelegateOfferModel.MaxMonth);

            if (!string.IsNullOrEmpty(getDelegateOfferModel.SearchQuery))
                offers = offers.Where(o => o.Title.ToLower().Contains(getDelegateOfferModel.SearchQuery.ToLower()));

            if (!string.IsNullOrEmpty(getDelegateOfferModel.RegionQuery))
                offers = offers.Where(o => o.Region.ToLower().Contains(getDelegateOfferModel.RegionQuery.ToLower()));

            return await PagedList<DelegateOffer>.ToPagedList(offers, getDelegateOfferModel.PageNumber, getDelegateOfferModel.PageSize);
        }

        public async Task<DelegateOffer> GetDelegateOffer(Guid id)
        {
            return await _delegateOffers.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DelegateOffer> UpdateDelegateOffer(Guid id, DelegateOffer productIn)
        {
            await _delegateOffers.ReplaceOneAsync(f => f.Id == id, productIn);
            return productIn;
        }

        public async Task DeleteDelegateOffer(Guid id)
        {
            await _delegateOffers.DeleteManyAsync(f => f.Id == id);
            return;
        }

        public async Task UpdateUserEmail(Guid id, string newEmail)
        {
            var listProvidersWithId = await _delegateOffers.Find(offer => offer.Provider.Id == id).ToListAsync();
            foreach (var _delegate in listProvidersWithId)
            {
                _delegate.Provider.Email = newEmail;
                await UpdateDelegateOffer(_delegate.Id, _delegate);
            }
        }

        public async Task RemoveDelegateOffersWithUser(Guid id)
        {
            var listProvidersWithId = await _delegateOffers.Find(offer => offer.Provider.Id == id).ToListAsync();
            foreach(var _delegate in listProvidersWithId)
            {
                await DeleteDelegateOffer(_delegate.Id);
            }

        }
    }
}