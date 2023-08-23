using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;

namespace SaleSavvy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        IAutenticationService _autenticationService;
        public AutenticationController(IAutenticationService autenticationService)
        {
            _autenticationService = autenticationService;
        }

        [HttpPost]
        public IActionResult InsertLogin([FromBody]InputLogin input)
        {
            _autenticationService.Validatelogin(input);
            return Ok(input);
        }
    }
}
