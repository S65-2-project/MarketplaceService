﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceService.DatastoreSettings;
using MarketplaceService.Domain;
using MongoDB.Driver;

namespace MarketplaceService.Repositories
{
    public class MarketplaceRepository : IMarketplaceRepository
    {
        private readonly IMongoCollection<Product> _products;

        public MarketplaceRepository(IMarketplaceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.MarketplaceCollectionName);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _products.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> UpdateProduct(Guid id, Product productIn)
        {
            await _products.ReplaceOneAsync(f => f.Id == id, productIn);
            return productIn;
        }

        public async Task DeleteProduct(Guid id)
        {
            await _products.DeleteManyAsync(f => f.Id == id);
        }
    }
}