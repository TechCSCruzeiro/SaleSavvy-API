﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Login.Input;
using SaleSavvy_API.Models.Register.Input;
using SaleSavvy_API.Services;

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

            if (output.ReturnCode == ReturnCode.exito)
            {
                var token = TokenService.GenerateToken(output);
                return Ok(token);
            }
            return BadRequest(output.Error.MenssageError);


        }
    }
}