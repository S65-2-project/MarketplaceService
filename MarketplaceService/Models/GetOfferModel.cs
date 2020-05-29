namespace MarketplaceService.Models
{
    public class GetOfferModel : QueryStringParameters
    {
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }

        public string searchQuery { get; set; }

        public string regionQuery { get; set; }
        public int? minAvailableForInMonth { get; set; }
        public int? maxAvailableForInMonth { get; set; }

        // public bool ValidPriceRange => maxPrice > minPrice;
    }
}