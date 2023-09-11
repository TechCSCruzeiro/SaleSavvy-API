using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;

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
    }
}