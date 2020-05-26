using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
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
    }
}