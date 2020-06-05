using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Exceptions;
using MarketplaceService.DataTypes;
using MarketplaceService.Models;
using MarketplaceService.Repositories;

namespace MarketplaceService.Services
{
    public class DelegateService : IDelegateService
    {
        private readonly IDelegateRepository _delegateRepository;

        public DelegateService(IDelegateRepository delegateRepository)
        {
            _delegateRepository = delegateRepository;
        }

        public async Task<DelegateOffer> CreateDelegateOffer(CreateDelegateOfferModel creatDelegateOfferModel)
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

            return await _delegateRepository.CreateDelegateOffer(delegateOffer);
        }

        public async Task<DelegateOffer> GetDelegateOffer(Guid id)
        {
            return await _delegateRepository.GetDelegateOffer(id);
        }

        public async Task<DelegateOffer> UpdateDelegateOffer(Guid id, UpdateDelegateOfferModel updateDelegateOfferModel)
        {
            if (string.IsNullOrEmpty(updateDelegateOfferModel.Title) || string.IsNullOrEmpty(updateDelegateOfferModel.Description))
                throw new EmptyFieldException();

            var delegateOffer = await GetDelegateOffer(id);

            delegateOffer.Id = id;
            delegateOffer.Title = updateDelegateOfferModel.Title;
            delegateOffer.Description = updateDelegateOfferModel.Description;
            delegateOffer.Region = updateDelegateOfferModel.Region;
            delegateOffer.LiskPerMonth = updateDelegateOfferModel.LiskPerMonth;
            delegateOffer.AvailableForInMonths = updateDelegateOfferModel.AvailableForInMonths;

            return await _delegateRepository.UpdateDelegateOffer(id, delegateOffer);
        }

        public async Task DeleteDelegateOffer(Guid id)
        {
            await _delegateRepository.DeleteDelegateOffer(id);
        }

        public async Task<PagedList<DelegateOffer>> GetOffers(GetOfferModel getOfferModel)
        {
            return await _delegateRepository.GetAllDelegateOffers(getOfferModel);
        }
    }
}