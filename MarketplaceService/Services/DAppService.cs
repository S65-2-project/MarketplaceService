using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using marketplaceservice.Exceptions;
using marketplaceservice.Helpers;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.Models;
using MarketplaceService.Repositories;

namespace MarketplaceService.Services
{
    public class DAppService : IDAppService
    {
        private readonly IDAppRepository _dAppRepository;
        private readonly IJwtIdClaimReaderHelper _jwtIdClaimReaderHelper;

        public DAppService(IDAppRepository dAppRepository, IJwtIdClaimReaderHelper jwtIdClaimReaderHelper)
        {
            _dAppRepository = dAppRepository;
            _jwtIdClaimReaderHelper = jwtIdClaimReaderHelper;
        }
        
        public async Task<DAppOffer> CreateDAppOffer(CreateDAppOfferModel createDAppOfferModel, string jwt)
        {
            if (string.IsNullOrEmpty(createDAppOfferModel.Title) || string.IsNullOrEmpty(createDAppOfferModel.Description))
                throw new EmptyFieldException();

            var offer = new DAppOffer
            {
                Id = Guid.NewGuid(),
                Title = createDAppOfferModel.Title,
                Provider = createDAppOfferModel.Provider,
                Description = createDAppOfferModel.Description,
                OfferLengthInMonths = createDAppOfferModel.OfferLengthInMonths,
                LiskPerMonth = createDAppOfferModel.LiskPerMonth, 
                DelegatesNeededForOffer = createDAppOfferModel.DelegatesNeededForOffer,
                DelegatesCurrentlyInOffer = new List<User>(),
                Region = createDAppOfferModel.Region,
                DateEnd = createDAppOfferModel.DateEnd,
                DateStart = createDAppOfferModel.DateStart
            };
            if(offer.Provider.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))//authorization
            {
                throw new NotAuthorisedException();
            }

            return await _dAppRepository.CreateDAppOffer(offer);
        }
        
        public async Task<DAppOffer> GetDAppOffer(Guid id)
        {
            return await _dAppRepository.GetDAppOffer(id);
        }

        public async Task<DAppOffer> UpdateDAppOffer(Guid id, UpdateDAppOfferModel updateDAppOfferModel, string jwt)
        {
            if (string.IsNullOrEmpty(updateDAppOfferModel.Title) || string.IsNullOrEmpty(updateDAppOfferModel.Description))
                throw new EmptyFieldException();

            var offer = await GetDAppOffer(id);

            offer.Id = id;
            offer.Title = updateDAppOfferModel.Title;
            offer.Description = updateDAppOfferModel.Description;
            offer.OfferLengthInMonths = updateDAppOfferModel.OfferLengthInMonths;
            offer.LiskPerMonth = updateDAppOfferModel.LiskPerMonth;
            offer.DelegatesNeededForOffer = updateDAppOfferModel.DelegatesNeededForOffer;
            offer.Region = updateDAppOfferModel.Region;
            offer.DateStart = updateDAppOfferModel.DateStart;
            offer.DateEnd = updateDAppOfferModel.DateEnd;

            if (offer.Provider.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))//authorization
            {
                throw new NotAuthorisedException();
            }

            return await _dAppRepository.UpdateDAppOffer(id, offer);
        }

        public async Task<DAppOffer> AddDelegateToDAppOffer(Guid id, User user, string jwt)
        {
            if(user.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))//authorization
            {
                throw new NotAuthorisedException();
            }
            var offerIn = await GetDAppOffer(id);
            offerIn.DelegatesCurrentlyInOffer.Add(user);

            return await _dAppRepository.AddDelegateToDAppOffer(id, offerIn);
        }

        public async Task<DAppOffer> RemoveDelegateFromDAppOffer(Guid id, User user, string jwt)
        {
            if (user.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))//authorization
            {
                throw new NotAuthorisedException();
            }

            var offerIn = await GetDAppOffer(id);
            try
            {
                offerIn.DelegatesCurrentlyInOffer.Remove(user);
            }
            catch (Exception e)
            {
                throw new DelegateNotInDAppOfferException();
            }

            return await _dAppRepository.RemoveDelegateFromDAppOffer(id, offerIn);
        }

        public async Task DeleteDAppOffer(Guid id, string jwt)
        {
            var offer = await _dAppRepository.GetDAppOffer(id);
            if(offer.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))//authorization
            {
                throw new NotAuthorisedException();
            }
            await _dAppRepository.DeleteDAppOffer(id);
        }
    }
}