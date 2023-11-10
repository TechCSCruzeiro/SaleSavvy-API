using SaleSavvy_API.Models.MovementRecords.Output;
using SaleSavvy_API.Models.Products;

namespace SaleSavvy_API.Interface
{
    public interface IRecord
    {

        Task<Guid> GenerateMovementRecordFile(List<OutputRecordStock> data);
        Task<Guid> GenerateSallesRecordFile(List<OutputRecordSales> data);
        Task<Guid> GenerateRecordStockFile(ProductDto[] data);
    }
}
