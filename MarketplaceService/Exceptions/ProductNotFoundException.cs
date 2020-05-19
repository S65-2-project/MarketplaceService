using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class ProductNotFoundException: Exception
    {
        public ProductNotFoundException()
            : base("All fields have to be filled out.")
        {
        }
    }
}