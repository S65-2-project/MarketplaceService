using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class ProductDeleteException : Exception
    {
        public ProductDeleteException()
            : base("Couldn't delete the offer.")
        {
        }
    }
}