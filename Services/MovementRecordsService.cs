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

        public MovementRecordsService(IMovementRecordsRepository movementRecordsRepository, IRecord record, IConfiguration configuration)
        {
            _movementRecordsRepository = movementRecordsRepository;
            _record = record;
            _configuration = configuration;
        }

        public async Task<Guid> CreateRecordStock(InputRecordStock input)
        {
            var data = await _movementRecordsRepository.GetStockReportInfo(input);

            var fileId = await _record.GenerateExcelFile(data);

            return fileId;
        }

        public async Task<string> SearchRecord(Guid fileId)
        {
            var destinationPath = _configuration["ExcelSettings:DestinationPath"];
            var excelFileName = $"{fileId}.xlsx";
            var excelFilePath =  Path.Combine(destinationPath, excelFileName);

            if (System.IO.File.Exists(excelFilePath))
            {
                return excelFilePath;
            }

            return null;
        }
    }
}
