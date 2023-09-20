using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models;
using SaleSavvy_API.Services;
using SaleSavvy_API.Models.Login.Output;
using SaleSavvy_API.Models.Login;

namespace SaleSavvy_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TesteController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Teste([FromBody] InputLogin input)
        {
            var login = new Login();
            login.EmployeeLogin = new Employee(input.Email, input.Password, "Teste");
            login.Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479");
            login.ReturnCode = ReturnCode.exito;
            var output = new OutputGetLogin(login);

            if (output.ReturnCode == ReturnCode.exito)
            {
                var token = TokenService.GenerateToken(output);
                return Ok(token);
            }
            return BadRequest(output.Error.MenssageError);

        }
    }
}

