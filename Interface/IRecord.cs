using SaleSavvy_API.Models.MovementRecords;

namespace SaleSavvy_API.Interface
{
    public interface IRecord
    {
        Task<Guid> GenerateExcelFile(List<OutputRecordStock> data);
    }
}
