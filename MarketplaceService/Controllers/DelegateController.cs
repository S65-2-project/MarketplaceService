using System;
using System.Threading.Tasks;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DelegateController : ControllerBase
    {
        private readonly IDelegateService _delegateService;

        public DelegateController(IDelegateService delegateService)
        {
            _delegateService = delegateService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDelegateOffer(Guid id)
        {
            try
            {
                return Ok(await _delegateService.GetDelegateOffer(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDelegateOffer([FromBody] CreateDelegateOfferModel createDelegateOfferModel)
        {
            try
            {
                return Ok(await _delegateService.CreateDelegateOffer(createDelegateOfferModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDelegateOffer(Guid id)
        {
            try
            {
                await _delegateService.DeleteDelegateOffer(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDelegateOffer(Guid id, UpdateDelegateOfferModel updateDelegateOfferModel)
        {
            try
            {
                return Ok(await _delegateService.UpdateDelegateOffer(id, updateDelegateOfferModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}