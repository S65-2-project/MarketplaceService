using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class ProductUpdateFailedException : Exception
    {
        public ProductUpdateFailedException()
            : base("Failed to update the product.") { }
    }
}
