using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Models.Register.Output;

namespace SaleSavvy_API.Controllers
{
    [Route("api/autentication")]
    [ApiController]
    public class AutenticationController : ControllerBase
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
        [HttpPost("/login")]
        public async Task<OutputGetLogin> InsertLogin([FromBody] InputLogin input)
        {
            return await _autenticationService.Validatelogin(input);
        }

        /// <summary>
        /// Inserir Login de acesso ao controle de estoque
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/register")]
        public async Task<OutputRegister> RegisterUser([FromBody] InputRegister input)
        {
            return await _autenticationService.ValidateRegister(input);
        }
    }
}