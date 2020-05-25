using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class ProductNotFoundException: Exception
    {
        public ProductNotFoundException()
            : base("Product was not found.")
        {
        }
    }
}