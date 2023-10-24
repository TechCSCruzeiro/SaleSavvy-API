using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Services;

namespace SaleSavvy_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovementRecordsController : Controller
    {
        IMovementRecordsService _movementRecordsService;
        public MovementRecordsController(IMovementRecordsService movementRecordsService)
        {
            _movementRecordsService = movementRecordsService;
        }

        [HttpPost("record/movement/stock")]
        public async Task<IActionResult> GenerateRecordStock(InputRecordStock input) 
        {
            var output = await _movementRecordsService.CreateRecordStock(input);

            if (output != null)
            {
                return Ok(output);
            }

            return BadRequest();
        }

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
