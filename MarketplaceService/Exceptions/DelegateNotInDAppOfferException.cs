using System;

namespace MarketplaceService.Exceptions
{
    [Serializable]
    public class DelegateNotInDAppOfferException : Exception
    {
        public DelegateNotInDAppOfferException()
            : base("This delegate is currently not part of this offer.")
        {
        }
    }
}