using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;

namespace SaleSavvy_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController:  Controller
    {
        IUserService _userService;
        public UserController(IUserService autenticationService)
        {
            _userService = autenticationService;
        }

        [HttpGet("listUser")]
        public async Task<IActionResult> GetUser()
        {
            var output = await _userService.GetListUser();

            if(output != null) 
            { 
                return Ok(output);
            }
            return BadRequest("Não foi encontrado nenhum usuario");
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var output = await _userService.DeleteUser(id.ToString());

            if (output == ReturnCode.exito)
            {
                return Ok(output);
            }
            return NotFound("Usuário não encontrado");
        }

    }
}
