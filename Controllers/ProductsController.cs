using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Services;
using System;
using System.Threading.Tasks;

namespace SaleSavvy_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct([FromBody] InputSaveProduct input)
        {

            var output = await _productsService.CreateProduct(input);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output.ReturnCode);
            }
            return BadRequest(output.Error.MenssageError);

        }

        [HttpGet("ListProduct")]
        public async Task<IActionResult> GetProduct([System.Web.Http.FromUri] Guid userId)
        {
            try
            {
                var output = await _productsService.SearchProduct(userId);
                return Ok(output);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ModificProduct")]
        public async Task<IActionResult> ModificProduct(InputProduct product)
        {
            var output = await _productsService.UpdateProduct(product);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output);
            }
            return NotFound(output.Error.MenssageError);
        }

        [HttpDelete("DesactiveProduct")]
        public async Task<IActionResult> DeleteProduct([System.Web.Http.FromUri] Guid productId)
        {
            var output = await _productsService.RemoveProduct(productId);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output);
            }
            return NotFound(output.Error.MenssageError);
        }

        [HttpGet("Find/ProductById")]
        public async Task<IActionResult> GetProductById([System.Web.Http.FromUri] Guid productId)
        {
            var output = await _productsService.SearchProductById(productId);
            
            if(output != null)
            {
                return Ok(output);
            }
            return NotFound("Produto não encontrado");
        }


    }
}
