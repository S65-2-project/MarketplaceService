using System;
using System.Threading.Tasks;
using MarketplaceService.Domain;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}