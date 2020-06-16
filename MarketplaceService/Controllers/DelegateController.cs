using System;
using System.Threading.Tasks;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDelegateOffer([FromBody] CreateDelegateOfferModel createDelegateOfferModel, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(await _delegateService.CreateDelegateOffer(createDelegateOfferModel, jwt));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDelegateOffer(Guid id, string jwt)
        {
            try
            {
                await _delegateService.DeleteDelegateOffer(id,jwt);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDelegateOffer(Guid id, UpdateDelegateOfferModel updateDelegateOfferModel, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(await _delegateService.UpdateDelegateOffer(id, updateDelegateOfferModel, jwt));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDelegateOfferModel getDelegateOfferModel)
        {
            try
            {
                // gets offers that comply with the filters in the getOfferModel
                var offers = await _delegateService.GetOffers(getDelegateOfferModel);

                // make headerdata for the frontend
                var metadata = new
                {
                    offers.TotalCount,
                    offers.PageSize,
                    offers.CurrentPage,
                    offers.TotalPages,
                    offers.HasNext,
                    offers.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}