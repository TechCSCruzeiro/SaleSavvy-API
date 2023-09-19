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
    public class ProductsController : ControllerBase // Altere a classe base para ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct([FromBody] InputProduct input)
        {

            var output = _productsService.CreateProduct(input);
            try
            {

                return Ok("Product inserted successfully");
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna uma resposta HTTP com status 500 Internal Server Error
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            // Lógica para buscar produtos aqui

            // Retorna uma resposta HTTP com os produtos encontrados
            return Ok(/* Resultado da busca de produtos */);
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
