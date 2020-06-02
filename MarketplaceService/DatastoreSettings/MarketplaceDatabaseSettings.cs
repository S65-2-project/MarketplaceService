namespace MarketplaceService.DatastoreSettings
{
    public class MarketplaceDatabaseSettings : IMarketplaceDatabaseSettings
    {
        public string DelegateOfferCollectionName { get; set; }
        public string DAppOfferCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMarketplaceDatabaseSettings
    {
        public string DAppOfferCollectionName { get; set; }
        public string DelegateOfferCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}