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

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateDAppOffer([FromBody] CreateDAppOfferModel createDAppOfferModel, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(await _dAppService.CreateDAppOffer(createDAppOfferModel,jwt));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDAppOffer(Guid id, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                await _dAppService.DeleteDAppOffer(id, jwt);
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
        public async Task<IActionResult> UpdateDAppOffer(Guid id, UpdateDAppOfferModel updateDAppOfferModel, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                return Ok(await _dAppService.UpdateDAppOffer(id, updateDAppOfferModel, jwt));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDAppOfferModel getDAppOfferModel)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}