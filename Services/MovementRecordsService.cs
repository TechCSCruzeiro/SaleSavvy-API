using Azure;
using Microsoft.AspNetCore.Mvc;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.MovementRecords;
using System.Net.Mime;

namespace SaleSavvy_API.Services
{
    public class MovementRecordsService : IMovementRecordsService
    {
        IMovementRecordsRepository _movementRecordsRepository;
        IRecord _record;
        IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MovementRecordsService(IMovementRecordsRepository movementRecordsRepository, IRecord record, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _movementRecordsRepository = movementRecordsRepository;
            _record = record;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Guid> CreateMovementRecordStock(InputRecordStock input)
        {
            var data = await _movementRecordsRepository.GetStockMovementReportInfo(input);

            var fileId = await _record.GenerateMovementRecordFile(data);

            return fileId;
        }

        public async Task<Guid> CreateRecordStock(InputRecordStock input)
        {
            var data = await _movementRecordsRepository.GetStockReportInfo(input);

            var fileId = await _record.GenerateExcelFile(data);

            return fileId;
        }

        public async Task<string> SearchRecord(Guid fileId)
        {
            string relativePath = _configuration["ExcelSettings:DestinationPath"];
            string absolutePath = Path.Combine(_hostingEnvironment.ContentRootPath, relativePath);

            var excelFileName = $"{fileId}.xlsx";
            var excelFilePath =  Path.Combine(absolutePath, excelFileName);

            if (System.IO.File.Exists(excelFilePath))
            {
                return excelFilePath;
            }

            return null;
        }
    }
}
