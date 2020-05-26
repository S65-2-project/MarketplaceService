using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class OfferNotFoundException: Exception
    {
        public OfferNotFoundException()
            : base("Could not find the offer.")
        {
        }
    }
}