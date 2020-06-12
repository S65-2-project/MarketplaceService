namespace MarketplaceService.Models
{
    public class GetDAppOfferModel : QueryStringParameters
    {
        public int? MinReward { get; set; }
        public int? MaxReward { get; set; }
        public string SearchQuery { get; set; }
        public string RegionQuery { get; set; }
    }
}