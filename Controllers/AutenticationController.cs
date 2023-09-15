using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Register.Input;

namespace SaleSavvy_API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AutenticationController : Controller
    {
        IAutenticationService _autenticationService;
        public AutenticationController(IAutenticationService autenticationService)
        {
            _autenticationService = autenticationService;
        }

        /// <summary>
        /// Inserir Login de acesso ao controle de estoque
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> InsertLogin([FromBody] InputLogin input)
        {
            var output = await _autenticationService.Validatelogin(input);

            if (output.ReturnCode != ReturnCode.exito)
            {
                return BadRequest(output.Error.MenssageError);
            }
            return Ok(output);

        }

        /// <summary>
        /// Cadastrar novo usuario
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] InputRegister input)
        {
            var output = await _autenticationService.ValidateRegister(input);

            if (output.ReturnCode != ReturnCode.exito)
            {
                return BadRequest(output.Error.MenssageError);
            }
            return Ok(output);
        }
    }
}