namespace MarketplaceService.Models
{
    public class GetOfferModel : QueryStringParameters
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string SearchQuery { get; set; }
        public string RegionQuery { get; set; }
        public int? MinAvailableForInMonth { get; set; }
        public int? MaxAvailableForInMonth { get; set; }
    }
}