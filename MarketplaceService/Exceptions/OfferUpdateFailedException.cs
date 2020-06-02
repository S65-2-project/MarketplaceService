using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class OfferUpdateFailedException : Exception
    {
        public OfferUpdateFailedException()
            : base("Failed to update the offer.") { }
    }
}
