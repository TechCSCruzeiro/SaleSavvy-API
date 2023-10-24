using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Services;

namespace SaleSavvy_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientController : Controller
    {
        IClientService _clientService;
        public ClientController(IClientService clientService) 
        {
          _clientService = clientService;
        }

        [HttpPost("Register/Customer")]
        public async Task<IActionResult> RegisterCustomer (InputClient input)
        {
            var output = await _clientService.RegisterClient(input);

            if (output.ReturnCode == ReturnCode.exito)
            {
                return Ok(output);
            }
            return BadRequest(output.Error.MenssageError);
        }
    }
}
