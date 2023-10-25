using SaleSavvy_API.Models;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IMovementRecordsRepository
    {
        Task<OutputProduct> SaveRecord(StatusMovementRecords status, InputProduct product);
        Task<List<OutputRecordStock>> GetStockReportInfo(InputRecordStock input);
        Task<List<OutputRecordStock>> GetStockMovementReportInfo(InputRecordStock input);
    }
}
