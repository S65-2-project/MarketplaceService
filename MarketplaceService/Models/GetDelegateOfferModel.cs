namespace MarketplaceService.Models
{
    public class GetDelegateOfferModel : QueryStringParameters
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string SearchQuery { get; set; }
        public string RegionQuery { get; set; }
        public int? MinMonth { get; set; }
        public int? MaxMonth { get; set; }
    }
}