using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
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

        public async Task<DelegateOffer> GetDelegateOffer(Guid id)
        {
            return await _delegateOffers.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DelegateOffer> UpdateDelegateOffer(Guid id, DelegateOffer productIn)
        {
            await _delegateOffers.ReplaceOneAsync(f => f.Id == id, productIn);
            return productIn;
        }

        public async Task DeleteDelegateOffer(Guid id )
        {
            await _delegateOffers.DeleteManyAsync(f => f.Id == id);
            return;
        }
    }
}