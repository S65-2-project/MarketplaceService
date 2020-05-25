using System;
using System.Threading.Tasks;
using MarketplaceService.Models;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarketplaceController : ControllerBase
    {
        private readonly IMarketplaceService _marketplaceService;

        public MarketplaceController(IMarketplaceService marketplaceService)
        {
            _marketplaceService = marketplaceService;
        }

        [HttpGet]
        [Route("delegate")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _marketplaceService.GetProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("delegate")]
        public async Task<IActionResult> Create([FromBody] CreateProductModel createProductModel)
        {
            try
            {
                return Ok(await _marketplaceService.CreateProduct(createProductModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delegate")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _marketplaceService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("delegate")]
        public async Task<IActionResult> Update(Guid id, UpdateProductModel updateProductModel)
        {
            try
            {
                return Ok(await _marketplaceService.UpdateProduct(id, updateProductModel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}