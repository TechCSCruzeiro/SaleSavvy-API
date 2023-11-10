using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.Client;
using SaleSavvy_API.Models.MovementRecords.Input;

namespace SaleSavvy_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class MovementRecordsController : Controller
    {
        IMovementRecordsService _movementRecordsService;
        public MovementRecordsController(IMovementRecordsService movementRecordsService)
        {
            _movementRecordsService = movementRecordsService;
        }

        /// <summary>
        /// Gerar Relatorio de Movimentação de Estoque
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("record/movement/stock")]
        public async Task<IActionResult> GenerateRecordMovementStock(InputRecord input) 
        {
            var output = await _movementRecordsService.CreateMovementRecordStock(input);

            if (output != null)
            {
                return Ok(output);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gerar Relatório de Estoque
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("record/stock")]
        public async Task<IActionResult> GenerateRecordStock(InputRecord input)
        {
            var output = await _movementRecordsService.CreateRecordStock(input);

            if (output != null)
            {
                return Ok(output);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gerar Relatório de Vendas
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("record/salles")]
        public async Task<IActionResult> GenareteRecordStock(InputRecord input)
        {
            var output = await _movementRecordsService.CreateSallesRecord(input);

            if (output != null)
            {
                return Ok(output);
            }

            return BadRequest();
        }

        /// <summary>
        /// Baixar Relatórios Gerados
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet("api/excel/download")]
        public async Task<IActionResult> DownloadRecord([System.Web.Http.FromUri] Guid fileId)
        {
            var data = await _movementRecordsService.SearchRecord(fileId);

            if (data == null)
            {
                return NotFound();
            }
            // Ler o arquivo e retorná-lo como resultado
            var fileStream = System.IO.File.OpenRead(data);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(fileStream, contentType, Path.GetFileName(data));
        }
    }
}
