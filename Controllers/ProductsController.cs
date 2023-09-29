using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Products;
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
        public async Task<IActionResult> InsertProduct([FromBody] InputProduct input)
        {

            var output = await _productsService.CreateProduct(input);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output.ReturnCode);
            }
            return BadRequest(output.Error.MenssageError);

        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Id do Usuario não pode ser vazio");
            }

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

        [HttpPut]
        public async Task<IActionResult> ModificProduct()
        {
            // Lógica para atualizar produtos aqui

            // Retorna uma resposta HTTP de sucesso com status 200 OK
            return Ok("Product updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct()
        {
            // Lógica para excluir produtos aqui

            // Retorna uma resposta HTTP de sucesso com status 200 OK
            return Ok("Product deleted successfully");
        }
    }
}
