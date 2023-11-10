using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Sales.Input;

namespace SaleSavvy_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService salesService;

        public SalesController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        /// <summary>
        /// Realizar Transação de Produtos
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateSales([FromBody] InputSales input)
        {
            var output = await salesService.TransactionSales(input);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok();
            }
            else
            {
                return BadRequest(output);
            }
        }
    }
}
