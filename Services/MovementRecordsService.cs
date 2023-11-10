using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.MovementRecords.Input;

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

        public async Task<Guid> CreateMovementRecordStock(InputRecord input)
        {
            var data = await _movementRecordsRepository.GetStockMovementReportInfo(input);

            var fileId = await _record.GenerateMovementRecordFile(data);

            return fileId;
        }

        public async Task<Guid> CreateSallesRecord(InputRecord input)
        {
            var data = await _movementRecordsRepository.GetSallesReportInfo(input);

            var fileId = await _record.GenerateSallesRecordFile(data);

            return fileId;
        }

        public async Task<Guid> CreateRecordStock(InputRecord input)
        {
            var data = await _movementRecordsRepository.GetStockReportInfo(input);

            if(data != null)
            {
                var fileId = await _record.GenerateRecordStockFile(data);

                return fileId;
            }

            return Guid.Empty;

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
