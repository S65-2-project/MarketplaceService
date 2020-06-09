using System;
using System.Threading.Tasks;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketplaceService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DAppController : ControllerBase
    {
        private readonly IDAppService _dAppService;

        public DAppController(IDAppService dAppService)
        {
            _dAppService = dAppService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDAppOffer(Guid id)
        {
            try
            {
                return Ok(await _dAppService.GetDAppOffer(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDAppOffer([FromBody] CreateDAppOfferModel createDAppOfferModel)
        {
            try
            {
                return Ok(await _dAppService.CreateDAppOffer(createDAppOfferModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDAppOffer(Guid id)
        {
            try
            {
                await _dAppService.DeleteDAppOffer(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDAppOffer(Guid id, UpdateDAppOfferModel updateDAppOfferModel)
        {
            try
            {
                return Ok(await _dAppService.UpdateDAppOffer(id, updateDAppOfferModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDAppOfferModel getDAppOfferModel)
        {
            // gets offers that comply with the filters in the getOfferModel
            var offers = await _dAppService.GetOffers(getDAppOfferModel);

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
    }
}