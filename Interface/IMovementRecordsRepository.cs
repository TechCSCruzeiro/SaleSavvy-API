using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Models.MovementRecords.Input;
using SaleSavvy_API.Models.MovementRecords.Output;
using SaleSavvy_API.Models.Products;
using SaleSavvy_API.Models.Products.Input;
using SaleSavvy_API.Models.Products.Output;

namespace SaleSavvy_API.Interface
{
    public interface IMovementRecordsRepository
    {
        Task<OutputProduct> SaveRecord(StatusMovementRecords status, InputProduct product);
        Task<List<OutputRecordStock>> GetStockMovementReportInfo(InputRecord input);
        Task<List<OutputRecordSales>> GetSallesReportInfo(InputRecord input);
        Task<ProductDto[]> GetStockReportInfo(InputRecord input);

    }
}
