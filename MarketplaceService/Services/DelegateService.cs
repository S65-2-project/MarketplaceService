using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;
using MarketplaceService.Repositories;
using marketplaceservice.Helpers;
using marketplaceservice.Exceptions;

namespace MarketplaceService.Services
{
    public class DelegateService : IDelegateService
    {
        private readonly IDelegateRepository _delegateRepository;
        private readonly IJwtIdClaimReaderHelper _jwtIdClaimReaderHelper;

        public DelegateService(IDelegateRepository delegateRepository, IJwtIdClaimReaderHelper jwtIdClaimReaderHelper)
        {
            _delegateRepository = delegateRepository;
            _jwtIdClaimReaderHelper = jwtIdClaimReaderHelper;
        }

        public async Task<DelegateOffer> CreateDelegateOffer(CreateDelegateOfferModel creatDelegateOfferModel, string jwt)
        {
            if (string.IsNullOrEmpty(creatDelegateOfferModel.Title) || string.IsNullOrEmpty(creatDelegateOfferModel.Description))
                throw new EmptyFieldException();

            var delegateOffer = new DelegateOffer
            {
                Id = Guid.NewGuid(),
                Provider = creatDelegateOfferModel.Provider,
                Title = creatDelegateOfferModel.Title,
                Description = creatDelegateOfferModel.Description,
                Region = creatDelegateOfferModel.Region,
                LiskPerMonth = creatDelegateOfferModel.LiskPerMonth,
                AvailableForInMonths = creatDelegateOfferModel.AvailableForInMonths
            };
            var id = _jwtIdClaimReaderHelper.getUserIdFromToken(jwt);
            if (delegateOffer.Provider.Id != id)
            {
                throw new NotAuthenticatedException();
            }

            return await _delegateRepository.CreateDelegateOffer(delegateOffer);
        }

        public async Task<DelegateOffer> GetDelegateOffer(Guid id)
        {
            return await _delegateRepository.GetDelegateOffer(id);
        }

        public async Task<DelegateOffer> UpdateDelegateOffer(Guid id, UpdateDelegateOfferModel updateDelegateOfferModel, string jwt)
        {
            if (string.IsNullOrEmpty(updateDelegateOfferModel.Title) || string.IsNullOrEmpty(updateDelegateOfferModel.Description))
                throw new EmptyFieldException();

            var delegateOffer = await GetDelegateOffer(id);
            if(delegateOffer.Provider.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))
            {
                throw new NotAuthenticatedException();
            }
            delegateOffer.Id = id;
            delegateOffer.Title = updateDelegateOfferModel.Title;
            delegateOffer.Description = updateDelegateOfferModel.Description;
            delegateOffer.Region = updateDelegateOfferModel.Region;
            delegateOffer.LiskPerMonth = updateDelegateOfferModel.LiskPerMonth;
            delegateOffer.AvailableForInMonths = updateDelegateOfferModel.AvailableForInMonths;

            return await _delegateRepository.UpdateDelegateOffer(id, delegateOffer);
        }

        public async Task DeleteDelegateOffer(Guid id, string jwt)
        {
            var offer = await _delegateRepository.GetDelegateOffer(id);
            if(offer.Id != _jwtIdClaimReaderHelper.getUserIdFromToken(jwt))
            {
                throw new NotAuthenticatedException();
            }
            await _delegateRepository.DeleteDelegateOffer(id);
        }

        public async Task<PagedList<DelegateOffer>> GetOffers(GetOfferModel getOfferModel)
        {
            return await _delegateRepository.GetAllDelegateOffers(getOfferModel);
        }
    }
}