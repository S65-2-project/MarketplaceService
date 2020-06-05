using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;
using MongoDB.Driver;

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

        public async Task<PagedList<DelegateOffer>> GetAllDelegateOffers(GetOfferModel getOfferModel)
        {
            // Retrieve the collection as queryable
            var offers = _delegateOffers.AsQueryable().OrderBy(on => on.LiskPerMonth).AsQueryable();

            // Apply filters
            if (getOfferModel.MinPrice != null)
                offers = offers.Where(o => o.LiskPerMonth >= getOfferModel.MinPrice);

            if (getOfferModel.MaxPrice != null)
                offers = offers.Where(o => o.LiskPerMonth <= getOfferModel.MaxPrice);

            if (getOfferModel.MinAvailableForInMonth != null)
                offers = offers.Where(o => o.AvailableForInMonths >= getOfferModel.MinAvailableForInMonth);

            if (getOfferModel.MaxAvailableForInMonth != null)
                offers = offers.Where(o => o.AvailableForInMonths <= getOfferModel.MaxAvailableForInMonth);

            if (!string.IsNullOrEmpty(getOfferModel.SearchQuery))
                offers = offers.Where(o => o.Title.ToLower().Contains(getOfferModel.SearchQuery.ToLower()));

            if (!string.IsNullOrEmpty(getOfferModel.RegionQuery))
                offers = offers.Where(o => o.Region.ToLower().Contains(getOfferModel.RegionQuery.ToLower()));

            return await PagedList<DelegateOffer>.ToPagedList(offers, getOfferModel.PageNumber, getOfferModel.PageSize);
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
    }
}