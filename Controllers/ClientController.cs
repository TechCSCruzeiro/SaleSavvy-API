using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client.Input;

namespace SaleSavvy_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientController : Controller
    {
        IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Registrar Cliente para venda
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Register/Customer")]
        public async Task<IActionResult> RegisterCustomer(InputClient input)
        {
            var output = await _clientService.RegisterClient(input);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output);
            }
            return BadRequest(output.Error.MenssageError);
        }
        
        /// <summary>
        /// Buscar Cliente por Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Search/Client")]
        public async Task<IActionResult> SearchClient(Guid userId)
        {
            var output = await _clientService.GetClient(userId);

            if (output != null)
            {
                return Ok(output);
            }
            return NotFound("Cliente não encontrado");
        }

        /// <summary>
        /// Buscar Lista de Clientes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Search/ListClient")]
        public async Task<IActionResult> SearchListClient([System.Web.Http.FromUri]string userId)
        {
            
            var output =  await _clientService.GetListClient(Guid.Parse(userId));

            if (output != null)
            {
                return Ok(output);
            }
            return BadRequest();
        }
    }
}
