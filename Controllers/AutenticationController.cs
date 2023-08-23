using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;

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

        [HttpPost("/login")]
        public async Task<OutputLogin> InsertLogin([FromBody]InputLogin input)
        {
            return await _autenticationService.Validatelogin(input);
        }
    }
}
