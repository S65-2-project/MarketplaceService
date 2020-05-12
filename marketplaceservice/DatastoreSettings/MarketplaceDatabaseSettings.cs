﻿namespace marketplaceservice.DatastoreSettings
{
    public class MarketplaceDatabaseSettings : IMarketplaceDatabaseSettings
    {
        public string MarketplaceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMarketplaceDatabaseSettings
    {
        string MarketplaceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}